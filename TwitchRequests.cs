using LiveSplit.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace LiveSplit.TwitchPredictions
{
	public static class TwitchRequests
	{
		private const string TwitchAPIURI = "https://api.twitch.tv/helix/";
		static KeyValuePair<string, string> clientIDHeader = new KeyValuePair<string, string>("Client-ID", "sz9g0b3arar4db1l4is6dk95wj9sfo");
		static string Channel = "";
		static string BroadcasterID = "";
		static KeyValuePair<string, string> BearerToken;
		static DateTime lastRequest = DateTime.MinValue;

		internal static void ProvideBearerToken(string Token, string ChannelP)
		{
			BearerToken = new KeyValuePair<string, string>("Authorization", "Bearer " + Token.Trim());
			Channel = ChannelP;
		}

		internal static Uri BuildURI(string[] endpoint, Tuple<string, string>[] parameters)
		{
			var uri = TwitchAPIURI + string.Join("/", endpoint);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i == 0)
					sb.Append("?");
				else
					sb.Append("&");

				sb.Append(JSON.Escape(parameters[i].Item1));
				sb.Append("=");
				sb.Append(JSON.Escape(parameters[i].Item2));
			}
			return new Uri(uri + sb.ToString());
		}



		internal static async System.Threading.Tasks.Task GetUserIDAsync()
		{
			DebugLogging.Log("Getting user ID");
			var userTokenResult = await PerformGetRequestAsync(
				BuildURI(new string[] { "users" }, new Tuple<string, string>[] { new Tuple<string, string>("login", Channel) }),
				new Dictionary<string, string>() { }
				);

			if (userTokenResult["data"] != null)
			{
				var dataNode = ((IEnumerable<dynamic>)userTokenResult["data"]).First();
				if (dataNode["id"] != null)
				{
					DebugLogging.Log("Request returned user ID!");
					BroadcasterID = dataNode["id"];
					return;
				}
			}
			DebugLogging.Log("Bad get user ID request!");
		}

		internal static async System.Threading.Tasks.Task<StreamPrediction> GetCurrentPredictionAsync()
		{
			DebugLogging.Log("Getting current prediction");

			if (BroadcasterID == "")
				await GetUserIDAsync();

			if (BroadcasterID == "")
			{
				DebugLogging.Log("[ERROR] Broadcaster ID wasn't set despite multiple requests!");
				return null;
			}

			var requestResult = await PerformGetRequestAsync(
				BuildURI(new string[] { "predictions" }, new Tuple<string, string>[] { new Tuple<string, string>("broadcaster_id", BroadcasterID) }),
				new Dictionary<string, string>() { }
				);

			if (requestResult["data"] != null)
			{
				var dataNode = ((IEnumerable<dynamic>)requestResult["data"]).First();

				if (dataNode["status"] != null)
				{
					DebugLogging.Log("Converting response to Stream Prediction object.");
					StreamPrediction prediction = StreamPrediction.ConvertNode(dataNode);
					DebugLogging.Log("Current prediction is: '" + prediction.Title + "' and the status is " + prediction.Status);
					return prediction;
				}
			}
			DebugLogging.Log("Failed to get current (or last) prediction.");
			return null;
		}

		#region Get/Post/Patch Request wrappers
		internal static async System.Threading.Tasks.Task<dynamic> PerformGetRequestAsync(Uri uri, Dictionary<string, string> headers)
		{
			var timeToWait = ((lastRequest + TimeSpan.FromSeconds(2)) - DateTime.UtcNow);
			if (timeToWait > TimeSpan.Zero)
				System.Threading.Thread.Sleep((int)(timeToWait.TotalMilliseconds));
			var request = WebRequest.Create(uri);
			request.Headers.Add(clientIDHeader.Key, clientIDHeader.Value);
			request.Headers.Add(BearerToken.Key, BearerToken.Value);
			foreach (var header in headers)
				request.Headers.Add(header.Key, header.Value);

			lastRequest = DateTime.UtcNow;
			using (var response = await request.GetResponseAsync())
			{
				return JSON.FromResponse(response);
			}
		}

		internal static async System.Threading.Tasks.Task<dynamic> PerformPostRequestAsync(Uri uri, Dictionary<string, string> headers, string bodyContent)
		{
			var timeToWait = ((lastRequest + TimeSpan.FromSeconds(2)) - DateTime.UtcNow);
			if (timeToWait > TimeSpan.Zero)
				System.Threading.Thread.Sleep((int)(timeToWait.TotalMilliseconds));
			var request = WebRequest.Create(uri);
			request.ContentType = "application/json";
			request.Method = "POST";

			request.Headers.Add(clientIDHeader.Key, clientIDHeader.Value);
			request.Headers.Add(BearerToken.Key, BearerToken.Value);
			foreach (var header in headers)
				request.Headers.Add(header.Key, header.Value);

			using (var streamWriter = new StreamWriter(request.GetRequestStream()))
			{
				streamWriter.Write(bodyContent);
			}

			lastRequest = DateTime.UtcNow;
			using (var response = await request.GetResponseAsync())
			{
				return JSON.FromResponse(response);
			}
		}

		internal static async System.Threading.Tasks.Task<dynamic> PerformPatchRequestAsync(Uri uri, Dictionary<string, string> headers, string bodyContent)
		{
			var timeToWait = ((lastRequest + TimeSpan.FromSeconds(2)) - DateTime.UtcNow);
			if (timeToWait > TimeSpan.Zero)
				System.Threading.Thread.Sleep((int)(timeToWait.TotalMilliseconds));
			var request = WebRequest.Create(uri);
			request.ContentType = "application/json";
			request.Method = "PATCH";

			request.Headers.Add(clientIDHeader.Key, clientIDHeader.Value);
			request.Headers.Add(BearerToken.Key, BearerToken.Value);
			foreach (var header in headers)
				request.Headers.Add(header.Key, header.Value);

			using (var streamWriter = new StreamWriter(request.GetRequestStream()))
			{
				streamWriter.Write(bodyContent);
			}

			lastRequest = DateTime.UtcNow;
			using (var response = await request.GetResponseAsync())
			{
				return JSON.FromResponse(response);
			}
		}
		#endregion

		internal static async System.Threading.Tasks.Task<StreamPrediction> StartPredictionAsync(string header, string option1, string option2, uint lenght)
		{
			StreamPrediction newPrediction;
			DebugLogging.Log("Trying to start a new prediction.");

			newPrediction = null;
			if (lenght > 1800)
				lenght = 1779;
			if (lenght < 1)
				lenght = 1;

			var parameters = new StringBuilder();
			parameters.AppendLine("{");
			parameters.AppendLine("\"broadcaster_id\": \"" + BroadcasterID + "\",");
			parameters.AppendLine("\"title\": \"" + JSON.Escape(header) + "\",");

			parameters.AppendLine("\"outcomes\": [");
			parameters.AppendLine("{");
			parameters.AppendLine("\"title\": \"" + JSON.Escape(option1) + "\"");
			parameters.AppendLine("},");
			parameters.AppendLine("{");
			parameters.AppendLine("\"title\": \"" + JSON.Escape(option2) + "\"");
			parameters.AppendLine("}");
			parameters.AppendLine("],");

			parameters.AppendLine("\"prediction_window\": " + lenght.ToString() + "");
			parameters.AppendLine("}");

			try
			{
				var response = await PerformPostRequestAsync(
					BuildURI(new string[] { "predictions" }, new Tuple<string, string>[] { }),
					new Dictionary<string, string>() { }, parameters.ToString()
				);

				if (response["data"] != null)
				{
					var dataNode = ((IEnumerable<dynamic>)response["data"]).First();
					if (dataNode["id"] != null)
					{
						if (dataNode["status"] != null)
						{
							newPrediction = StreamPrediction.ConvertNode(dataNode);
							DebugLogging.Log("Successfully created a new prediction!");
							return newPrediction;
						}
						DebugLogging.Log("[ERROR] Incorrect response?");
						return newPrediction;
					}
				}
				DebugLogging.Log("[ERROR] Incorrect response?");
				return newPrediction;
			}
			catch (WebException e)
			{
				if (e.Status == WebExceptionStatus.ProtocolError)
				{
					if (((HttpWebResponse)e.Response).StatusCode == HttpStatusCode.BadRequest)
					{
						var rawResponse = string.Empty;

						var alreadyClosedStream = e.Response.GetResponseStream() as MemoryStream;
						using (var brandNewStream = new MemoryStream(alreadyClosedStream.ToArray()))
						using (var reader = new StreamReader(brandNewStream))
							rawResponse = reader.ReadToEnd();

						if (rawResponse.Contains("prediction event already active"))
						{
							DebugLogging.Log("[ERROR] Prediction is already running!");
							return newPrediction;
						}
					}
					DebugLogging.Log("[ERROR] " + e);
					return newPrediction;

				}
				DebugLogging.Log("[ERROR] " + e);
				return newPrediction;
			}
		}

		internal static async void CompleteWithOptionAsync(int winningOption)
		{
			if (TwitchConnection.GetInstance().BroadcasterID == "")
				await TwitchRequests.GetUserIDAsync();

			if (TwitchConnection.GetInstance().BroadcasterID != "")
			{
				StreamPrediction prediction = TwitchConnection.GetInstance().CurrentPrediction;
				if (prediction == null)
					prediction = await GetCurrentPredictionAsync();

				if (prediction != null && (prediction.Status == StreamPrediction.PredictionStatus.ACTIVE || prediction.Status == StreamPrediction.PredictionStatus.LOCKED))
				{
					DebugLogging.Log("Trying to close a prediction (" + prediction.ID + ") with outcome #" + (winningOption + 1).ToString());

					var parameters = new StringBuilder();
					parameters.AppendLine("{");
					parameters.AppendLine("\"broadcaster_id\": \"" + BroadcasterID + "\",");
					parameters.AppendLine("\"id\": \"" + prediction.ID + "\",");

					parameters.AppendLine("\"status\": \"" + StreamPrediction.PredictionStatus.RESOLVED + "\",");
					parameters.AppendLine("\"winning_outcome_id\": \"" + (winningOption == 0 ? prediction.FirstOutcome.ID : prediction.SecondOutcome.ID) + "\"");
					parameters.AppendLine("}");

					var response = await PerformPatchRequestAsync(BuildURI(new string[] { "predictions" }, new Tuple<string, string>[] { }),
						new Dictionary<string, string>() { }, parameters.ToString()
					);

					if (response["data"] != null)
					{
						var dataNode = ((IEnumerable<dynamic>)response["data"]).First();
						if (dataNode["id"] != null)
						{
							if (dataNode["status"] != null)
							{
								StreamPrediction newPredictionState = StreamPrediction.ConvertNode(dataNode);
								if (newPredictionState.Status == StreamPrediction.PredictionStatus.CANCELED)
								{
									DebugLogging.Log("Successfully closed a prediction!");
									return;
								}
								else
								{
									DebugLogging.Log("Failed to close a new prediction!");
									return;
								}
							}
						}
					}
				}
			}
		}

		internal static async void CancelPredictionAsync()
		{
			if (TwitchConnection.GetInstance().BroadcasterID == "")
				await TwitchRequests.GetUserIDAsync();

			if (TwitchConnection.GetInstance().BroadcasterID != "")
			{
				StreamPrediction prediction = TwitchConnection.GetInstance().CurrentPrediction;
				if (prediction == null)
					prediction = await GetCurrentPredictionAsync();

				if (prediction != null && (prediction.Status == StreamPrediction.PredictionStatus.ACTIVE || prediction.Status == StreamPrediction.PredictionStatus.LOCKED))
				{
					DebugLogging.Log("Trying to cancel a cancel prediction (" + prediction.ID + ")");

					var parameters = new StringBuilder();
					parameters.AppendLine("{");
					parameters.AppendLine("\"broadcaster_id\": \"" + BroadcasterID + "\",");
					parameters.AppendLine("\"id\": \"" + prediction.ID + "\",");

					parameters.AppendLine("\"status\": \"" + StreamPrediction.PredictionStatus.CANCELED + "\"");
					parameters.AppendLine("}");

					var response = await PerformPatchRequestAsync(BuildURI(new string[] { "predictions" }, new Tuple<string, string>[] { }),
						new Dictionary<string, string>() { }, parameters.ToString()
					);

					if (response["data"] != null)
					{
						var dataNode = ((IEnumerable<dynamic>)response["data"]).First();
						if (dataNode["id"] != null)
						{
							if (dataNode["status"] != null)
							{
								StreamPrediction newPredictionState = StreamPrediction.ConvertNode(dataNode);
								if (newPredictionState.Status == StreamPrediction.PredictionStatus.CANCELED)
								{
									DebugLogging.Log("Successfully cancelled a new prediction!");
									return;
								}
								else
								{
									DebugLogging.Log("Failed to cancelled a new prediction!");
									return;
								}
							}
						}
					}
					DebugLogging.Log("[ERROR] Incorrect response?");
					return;
				}
			}
			else
				DebugLogging.Log("Can not cancel prediction. Broadcaster ID is null!");
		}
	}
}

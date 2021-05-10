using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.Web;

namespace LiveSplit.TwitchPredictions
{
	public static class TwitchRequests
	{
		private const string TwitchAPIURI = "https://api.twitch.tv/helix/";
		static KeyValuePair<string, string> clientIDHeader = new KeyValuePair<string, string>("Client-ID", "sz9g0b3arar4db1l4is6dk95wj9sfo");
		static KeyValuePair<string, string> bearerToken;
		static DateTime lastRequest = DateTime.UtcNow;

		public static void ProvideBearerToken(string Token)
		{
			bearerToken = new KeyValuePair<string, string>("Authorization", "Bearer " + Token.Trim());
		}

		public static Uri BuildURI(string[] endpoint, Tuple<string, string>[] parameters)
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

		internal static object PerformGetRequest(Uri uri, Dictionary<string, string> headers)
		{

			var request = WebRequest.Create(uri);
			request.Headers.Add(clientIDHeader.Key, clientIDHeader.Value);
			request.Headers.Add(bearerToken.Key, bearerToken.Value);
			foreach (var header in headers)
				request.Headers.Add(header.Key, header.Value);

			using (var response = request.GetResponse())
			{
				return JSON.FromResponse(response);
			}
		}
	}
}

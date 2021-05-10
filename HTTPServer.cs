using System;
using System.Linq;
using System.Net;
using System.Threading;

namespace LiveSplit.TwitchPredictions
{
	public class HTTPServer
	{
		const string REDIRECT_URI = "http://localhost:43628/auth/twitch/callback/";
		HttpListener listener;
		Thread t;
		public bool Terminate;
		System.Timers.Timer scheduledTermination;
		IAsyncResult result;

		public delegate void OnReceivedResultDelegate(string token, string scope, string tokentype);
		public event OnReceivedResultDelegate OnReceivedResultEvent;

		public static bool IsSupported() => HttpListener.IsSupported;

		public HTTPServer()
		{
			System.Diagnostics.Process.Start("https://id.twitch.tv/oauth2/authorize" +
				"?response_type=token" +
				"&client_id=" + TwitchConnection.ClientID +
				"&redirect_uri=" + REDIRECT_URI +
				"&force_verify=true" +
				"&scope=" +
				string.Join(" ", new string[] {
					"chat:edit",
					"chat:read",
					"channel:read:predictions",
					"channel:manage:predictions"
				}));
			t = new Thread(ServerThread);
			t.Start();
		}

		private void ServerThread()
		{
			listener = new HttpListener();
			listener.Prefixes.Add("http://localhost:43628/");
			listener.Start();
			scheduledTermination = new System.Timers.Timer(1000 * 60 * 2);
			scheduledTermination.Elapsed += (o, e) => { CloseHttpListener(); };
			scheduledTermination.Start();
			result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
		}

		public void ListenerCallback(IAsyncResult result)
		{
			var listenerAsync = (HttpListener)result.AsyncState;
			HttpListenerContext context = listenerAsync.EndGetContext(result);
			HttpListenerRequest request = context.Request;
			HttpListenerResponse response = context.Response;
			string pageContentRedirect = string.Join("\n", "<html>",
					"<head>",
					"<script>",
					"window.onload = function() {",
					"if(location.hash != null && location.hash.startsWith(\"#\"))",
					"{",
					"window.location.replace(\"http://localhost:43628/auth/twitch/callback/\" +  location.hash.replace(\'#\', \'&\'));",
					"}",
					"}",
					"</script>",
					"<title>Close it</title>",
					"</head>",
					"<body>You can probably close this page</body>",
					"</html>"); byte[] buffer = System.Text.Encoding.UTF8.GetBytes(pageContentRedirect);
			response.ContentLength64 = buffer.Length;
			System.IO.Stream output = response.OutputStream;
			output.Write(buffer, 0, buffer.Length);

			var requestUrl = request.Url.ToString();
			if (!requestUrl.StartsWith(REDIRECT_URI, StringComparison.InvariantCultureIgnoreCase))
				throw new FailedToGetProperResponseException("Response URL was: " + requestUrl);
			var anchor = requestUrl.Remove(0, REDIRECT_URI.Length);
			var elements = anchor.Split(new char[] { '?', '&' }, StringSplitOptions.RemoveEmptyEntries);
			var keyvaluePair = elements.Select(x => x.Split(new char[] { '=' }, 2)).ToDictionary(split => split[0], split => split[1]);
			if (keyvaluePair.ContainsKey("access_token"))
			{
				CloseHttpListener();
				OnReceivedResultEvent?.Invoke(keyvaluePair["access_token"],
					keyvaluePair.ContainsKey("scope") ? keyvaluePair["scope"] : "",
					keyvaluePair.ContainsKey("token_type") ? keyvaluePair["token_type"] : ""
					);
				return;
			}

			result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
		}

		public void CloseHttpListener()
		{
			if (listener != null && listener.IsListening)
				listener.Close();

			if (scheduledTermination != null)
			{
				scheduledTermination.Stop();
				scheduledTermination.Dispose();
			}
		}
	}

	public class FailedToGetProperResponseException : Exception
	{
		public FailedToGetProperResponseException() : base() { }

		public FailedToGetProperResponseException(string message) : base(message) { }
		public FailedToGetProperResponseException(string message, Exception innerException) : base(message, innerException) { }
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveSplit.TwitchPredictions
{
	public class HTTPServer
	{

		const string REDIRECT_URI = "http://localhost:43628/auth/twitch/callback/";
		HttpListener listener;
		Thread t;
		public bool Terminate;
		System.Timers.Timer scheduledTermination;

		public static bool IsSupported() => HttpListener.IsSupported;

		public HTTPServer()
		{
			System.Diagnostics.Process.Start("https://id.twitch.tv/oauth2/authorize" +
				"?response_type=code" +
				"&client_id=" + TwitchConnection.ClientID +
				"&redirect_uri=" + REDIRECT_URI +
				"&force_verify=true" +
				"&scope=chat:read");
			t = new Thread(ServerThread);
			t.Start();
		}

		private void ServerThread()
		{
			listener = new HttpListener();
			listener.Prefixes.Add("http://localhost:43628/");
			listener.Start();
			IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);
			scheduledTermination = new System.Timers.Timer(1000 * 60 * 2);
			scheduledTermination.Elapsed += (o, e) => { CloseHttpListener(); };
			scheduledTermination.Start();
		}

		public void ListenerCallback(IAsyncResult result)
		{
			listener = (HttpListener)result.AsyncState;
			HttpListenerContext context = listener.EndGetContext(result);
			HttpListenerRequest request = context.Request;
			HttpListenerResponse response = context.Response;
			string responseString = "Received response.... kurwa.";
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
			response.ContentLength64 = buffer.Length;
			System.IO.Stream output = response.OutputStream;
			output.Write(buffer, 0, buffer.Length);

			var requestUrl = request.Url.ToString();
			CloseHttpListener();
			if (!requestUrl.StartsWith(REDIRECT_URI, StringComparison.InvariantCultureIgnoreCase))
				throw new FailedToGetProperResponseException("Response URL was: " + requestUrl);

			var anchor = requestUrl.Remove(0, REDIRECT_URI.Length);
			var elements = anchor.Split(new char[] { '?', '&' }, StringSplitOptions.RemoveEmptyEntries);
			var keyvaluePair = elements.Select(x => x.Split(new char[] { '=' }, 2)).ToDictionary(split => split[0], split => split[1]);
			if (keyvaluePair["code"] == null)
				throw new AnchorDoesntContainCodeException();


		}

		private void CloseHttpListener()
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
		public FailedToGetProperResponseException() : base()	{}

		public FailedToGetProperResponseException(string message) : base(message) { }
		public FailedToGetProperResponseException(string message, Exception innerException) : base(message, innerException) { }
	}

	public class AnchorDoesntContainCodeException : Exception
	{
		public AnchorDoesntContainCodeException() : base() { }

		public AnchorDoesntContainCodeException(string message) : base(message) { }
		public AnchorDoesntContainCodeException(string message, Exception innerException) : base(message, innerException) { }
	}
}

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
		string REDIRECT_URI = "http://localhost:43628/auth/twitch/callback";
		HttpListener listener;
		Thread t;
		public bool Terminate;

		public static bool IsSupported() => HttpListener.IsSupported;

		public HTTPServer()
		{
			System.Diagnostics.Process.Start("https://id.twitch.tv/oauth2/authorize" +
				"?response_type=token" +
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
			System.Timers.Timer scheduledTermination = new System.Timers.Timer(1000 * 60 * 2);
			scheduledTermination.Elapsed += (o, e) => { CloseHttpListener(); };
			scheduledTermination.Start();
		}

		private void ScheduledTermination_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			throw new NotImplementedException();
		}

		public static void ListenerCallback(IAsyncResult result)
		{
			HttpListener listener = (HttpListener)result.AsyncState;
			HttpListenerContext context = listener.EndGetContext(result);
			HttpListenerRequest request = context.Request;
			HttpListenerResponse response = context.Response;
			string responseString = "Received response.... kurwa.";
			byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
			response.ContentLength64 = buffer.Length;
			System.IO.Stream output = response.OutputStream;
			output.Write(buffer, 0, buffer.Length);
			output.Close();
		}

		private void CloseHttpListener()
		{
			if (listener != null && listener.IsListening)
				listener.Close();
		}

	}
}

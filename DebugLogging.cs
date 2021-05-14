using System;
using System.Diagnostics;

namespace LiveSplit.TwitchPredictions
{
	public static class DebugLogging
	{
		public delegate void InvokeLogDelegade(string text);
		public static TwitchPredictionsSettings _settings;

		public static void Log(string text)
		{
			if (_settings.RB_DebugView.InvokeRequired)
			{
				_settings.RB_DebugView.Invoke(new InvokeLogDelegade(Log), text);
			}
			else
			{
				string textToPost = DateTime.Now.ToShortTimeString() + ": " + text + "\n";
				Debug.WriteLine("[PredictionsPlugin] " + textToPost);
				_settings.RB_DebugView.AppendText(textToPost);
			}
		}
	}
}

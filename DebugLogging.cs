using System;
using System.Diagnostics;

namespace LiveSplit.TwitchPredictions
{
	public static class DebugLogging
	{
		public delegate void InvokeLogDelegade(string text, bool dontPostInChat);
		public static TwitchPredictionsSettings _settings;

		public static void Log(string text, bool dontPostInChat = false)
		{
			if (_settings.RB_DebugView.InvokeRequired)
			{
				_settings.RB_DebugView.Invoke(new InvokeLogDelegade(Log), text, dontPostInChat);
			}
			else
			{
				string textToPost = DateTime.Now.ToShortTimeString() + ": " + text;
				Debug.WriteLine("[PredictionsPlugin] " + textToPost);
				_settings.RB_DebugView.AppendText(textToPost + "\n");
				if (_settings.SplitsToEventsInstance.NotifyOfErrorsInChat && !dontPostInChat)
					TwitchConnection.GetInstance().WriteInChat("[Prediction Plugin] " + text);
			}
		}
	}
}

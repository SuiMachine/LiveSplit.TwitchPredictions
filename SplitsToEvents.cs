using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace LiveSplit.TwitchPredictions
{
	public enum SplitEventType
	{
		[Description("None")] None,
		[Description("Start Prediction")] StartPredictionOnSplitStart,
		[Description("Finish Prediction with first option")] FinishPredictionWithOption1,
		[Description("Finish Prediction with second option")] FinishPredictionWithOption2,
	}

	public enum OnResetEventType
	{
		[Description("Cancel prediction")] Cancel,
		[Description("Complete with first outcome")] CompleteWithOptionOne,
		[Description("Complete with second outcome")] CompleteWithOptionTwo,
		[Description("Do nothing")] Nothing
	}

	[Serializable]
	public class SplitsToEvents : ICloneable
	{
		#region Properties And Sub-class definitions
		[XmlElement] public bool UsePBPrediction { get; set; }
		[XmlArrayItem] public List<SplitEvent> EventList { get; set; }
		[XmlElement] public OnResetEventType OnTimerResetBehaviour { get; set; }
		[XmlElement] public OnResetEventType OnRunCompletion { get; set; }
		[XmlElement] public TimeSpan OnRunCompletionDelay { get; set; }

		[XmlIgnore] public string Filename { get; set; }
		[XmlIgnore] public bool RequiresManualFixing { get; set; }
		[XmlElement] public bool NotifyOfErrorsInChat { get; set; }


		#region Subclasses
		[Serializable]
		public class SplitEvent : ISplitEvent
		{
			[XmlIgnore] public string SegmentName { get; set; }
			[XmlAttribute] public SplitEventType EventType { get; set; }
			[XmlElement] public TimeSpan Delay { get; set; }
			[XmlElement] public SplitAction Action { get; set; }

			[XmlIgnore] public bool WasUsed { get; set; }

			public SplitEvent()
			{
				SegmentName = "";
				EventType = SplitEventType.None;
				Delay = TimeSpan.Zero;
				Action = new SplitAction();
				WasUsed = false;
			}

			public object Clone()
			{
				var clone = new SplitEvent();
				clone.SegmentName = this.SegmentName;
				clone.EventType = this.EventType;
				clone.Delay = this.Delay;
				clone.Action = (SplitAction)this.Action.Clone();
				clone.WasUsed = this.WasUsed;
				return clone;
			}
		}

		[Serializable]
		public class SplitAction : ICloneable
		{
			[XmlAttribute] public bool isUsed { get; set; }
			[XmlAttribute] public string Header { get; set; }
			[XmlAttribute] public string Answer1 { get; set; }
			[XmlAttribute] public string Answer2 { get; set; }
			[XmlAttribute] public uint Lenght { get; set; }

			public SplitAction()
			{
				isUsed = false;
				Header = "";
				Answer1 = "";
				Answer2 = "";
				Lenght = 30;
			}

			public object Clone()
			{
				var clone = new SplitAction();
				clone.isUsed = isUsed;
				clone.Header = Header;
				clone.Answer1 = Answer1;
				clone.Answer2 = Answer2;
				clone.Lenght = Lenght;
				return clone;
			}
		}
		#endregion
		#endregion

		#region Constructor and Clone
		public SplitsToEvents()
		{
			EventList = new List<SplitEvent>();
			OnTimerResetBehaviour = OnResetEventType.Cancel;
			OnRunCompletion = OnResetEventType.Nothing;
			OnRunCompletionDelay = TimeSpan.Zero;
			Filename = "";
			UsePBPrediction = true;
			RequiresManualFixing = false;
			NotifyOfErrorsInChat = false;
		}

		public object Clone()
		{
			var clone = new SplitsToEvents();
			clone.UsePBPrediction = UsePBPrediction;
			clone.NotifyOfErrorsInChat = NotifyOfErrorsInChat;
			clone.EventList = EventList.Select(x => (SplitEvent)x.Clone()).ToList();
			clone.OnTimerResetBehaviour = OnTimerResetBehaviour;
			clone.OnRunCompletion = OnRunCompletion;
			clone.OnRunCompletionDelay = OnRunCompletionDelay;
			clone.Filename = Filename;
			clone.RequiresManualFixing = RequiresManualFixing;
			return clone;
		}
		#endregion

		#region Verify / Save / Enum Descriptor
		public string Verify(Model.LiveSplitState splitStates)
		{
			StringBuilder sbIssues = new StringBuilder();

			var segments = splitStates.Run.ToList();
			if (segments.Count < EventList.Count)
			{
				EventList.RemoveRange(segments.Count, EventList.Count - segments.Count);  //TO DO: Test this!
				sbIssues.AppendLine("- Event list had more positions than splits (the list was trimmed!)");
			}
			else if (segments.Count > EventList.Count)
			{
				var splitsToAdd = segments.Where((x, i) => i >= EventList.Count).Select(x => new SplitsToEvents.SplitEvent());
				EventList.AddRange(splitsToAdd);
				sbIssues.AppendLine("- Event list has less positions than splits (new positions were added!)");
			}

			for (int i = 0; i < EventList.Count; i++)
			{
				EventList[i].SegmentName = segments[i].Name;
			}

			RequiresManualFixing = false;
			bool predictionRunning = false;
			for (int i = 0; i < EventList.Count; i++)
			{
				if (EventList[i].EventType == SplitEventType.None)
					continue;
				else if (EventList[i].EventType == SplitEventType.StartPredictionOnSplitStart && !predictionRunning)
					predictionRunning = true;
				else if (EventList[i].EventType == SplitEventType.StartPredictionOnSplitStart && predictionRunning)
				{
					sbIssues.AppendLine($"- Event list tries to start a prediction before the other prediction is closed (Split #{i + 1}.)");
					RequiresManualFixing = true;
					break;
				}
				else if (EventList[i].EventType == SplitEventType.FinishPredictionWithOption1 || EventList[i].EventType == SplitEventType.FinishPredictionWithOption2)
					predictionRunning = false;
			}

			return sbIssues.ToString();
		}

		public static string GetEnumDescription<T>(T value) where T : Enum
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

			if (attributes != null && attributes.Any())
			{
				return attributes.First().Description;
			}

			return value.ToString();
		}

		internal void Save(string filePath = "")
		{
			if (filePath == "")
				filePath = Filename;
			XmlSerialiationDeserilation.SaveObjectToXML<SplitsToEvents>(this, filePath);
		}
		#endregion

		#region Split Events
		internal void ClearWasUsedFlags()
		{
			foreach (var element in EventList)
			{
				element.WasUsed = false;
			}
		}

		internal void DoResetEvent()
		{
			if (TwitchConnection.GetInstance().CurrentPrediction != null)
			{
				switch (OnTimerResetBehaviour)
				{
					case OnResetEventType.Cancel:
						TwitchConnection.GetInstance().CancelPrediction(TimeSpan.Zero);
						return;
					case OnResetEventType.CompleteWithOptionOne:
						TwitchConnection.GetInstance().CompletePrediction(0, TimeSpan.Zero);
						return;
					case OnResetEventType.CompleteWithOptionTwo:
						TwitchConnection.GetInstance().CompletePrediction(1, TimeSpan.Zero);
						return;
					case OnResetEventType.Nothing:
						return;
				}
			}
		}

		internal void DoSplitEvent(int split)
		{
			if (split < EventList.Count)
			{
				if (EventList[split].WasUsed)
					return;

				EventList[split].WasUsed = true;
				var actionToPerform = EventList[split];

				switch (actionToPerform.EventType)
				{
					case SplitEventType.None:
						return;
					case SplitEventType.FinishPredictionWithOption1:
						TwitchConnection.GetInstance().CompletePrediction(0, actionToPerform.Delay);
						return;
					case SplitEventType.FinishPredictionWithOption2:
						TwitchConnection.GetInstance().CompletePrediction(1, actionToPerform.Delay);
						return;
					case SplitEventType.StartPredictionOnSplitStart:
						if (actionToPerform.Action.isUsed)
							TwitchConnection.GetInstance().StartNewPrediction(actionToPerform.Action.Header, actionToPerform.Action.Answer1, actionToPerform.Action.Answer2, actionToPerform.Action.Lenght);
						return;
				}
			}
		}

		internal void DoCompleteRunEvent(bool isPB)
		{
			if (UsePBPrediction)
			{
				if (isPB)
					TwitchConnection.GetInstance().CompletePrediction(0, OnRunCompletionDelay);
				else
					TwitchConnection.GetInstance().CompletePrediction(1, OnRunCompletionDelay);
			}
			else
			{
				switch (OnRunCompletion)
				{
					case OnResetEventType.Nothing:
						return;
					case OnResetEventType.Cancel:
						TwitchConnection.GetInstance().CancelPrediction(OnRunCompletionDelay);
						return;
					case OnResetEventType.CompleteWithOptionOne:
						TwitchConnection.GetInstance().CompletePrediction(0, OnRunCompletionDelay);
						return;
					case OnResetEventType.CompleteWithOptionTwo:
						TwitchConnection.GetInstance().CompletePrediction(1, OnRunCompletionDelay);
						return;
				}
			}
		}
		#endregion
	}
}

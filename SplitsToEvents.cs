using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
		[XmlElement] public bool UseMessageBoxes { get; set; }
		[XmlArrayItem] public List<SplitEvent> EventList { get; set; }
		[XmlElement] public OnResetEventType OnTimerResetBehaviour { get; set; }
		[XmlElement] public OnResetEventType OnRunCompletion { get; set; }

		[XmlIgnore] public string Filename { get; set; }
		[XmlIgnore] public bool RequiresManualFixing { get; set; }

		#region Subclasses
		[Serializable]
		public class SplitEvent : ISplitEvent
		{
			[XmlIgnore] public string SegmentName { get; set; }
			[XmlAttribute] public SplitEventType EventType { get; set; }
			[XmlElement] public TimeSpan Delay { get; set; }
			public SplitAction Action { get; set; }

			public SplitEvent()
			{
				SegmentName = "";
				EventType = SplitEventType.None;
				Delay = TimeSpan.Zero;
				Action = new SplitAction();
			}

			public object Clone()
			{
				var clone = new SplitEvent();
				clone.SegmentName = this.SegmentName;
				clone.EventType = this.EventType;
				clone.Delay = this.Delay;
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

			public SplitAction()
			{
				isUsed = false;
				Header = "";
				Answer1 = "";
				Answer2 = "";
			}

			public object Clone()
			{
				var clone = new SplitAction();
				clone.isUsed = isUsed;
				clone.Header = Header;
				clone.Answer1 = Answer1;
				clone.Answer2 = Answer2;
				return clone;
			}
		}
		#endregion
		#endregion

		#region Constructor and Clone
		public SplitsToEvents()
		{
			UseMessageBoxes = false;
			EventList = new List<SplitEvent>();
			OnTimerResetBehaviour = OnResetEventType.Cancel;
			OnRunCompletion = OnResetEventType.Nothing;
			Filename = "";
			UsePBPrediction = true;
			RequiresManualFixing = false;
		}

		public object Clone()
		{
			var clone = new SplitsToEvents();
			clone.UsePBPrediction = UsePBPrediction;
			clone.UseMessageBoxes = UseMessageBoxes;
			clone.EventList = EventList.Select(x => (SplitEvent)x.Clone()).ToList();
			clone.OnTimerResetBehaviour = OnTimerResetBehaviour;
			clone.OnRunCompletion = OnRunCompletion;
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
		internal void DoResetEvent()
		{
			if(TwitchConnection.GetInstance().CurrentPrediction != null)
			{
				switch (OnTimerResetBehaviour)
				{
					case OnResetEventType.Cancel:
						TwitchRequests.CancelPredictionAsync();
						return;
					case OnResetEventType.CompleteWithOptionOne:
						TwitchRequests.CompleteWithOptionAsync(1);
						return;
					case OnResetEventType.CompleteWithOptionTwo:
						TwitchRequests.CompleteWithOptionAsync(2);
						return;
					case OnResetEventType.Nothing:
						return;
				}
			}
		}
		#endregion


	}
}

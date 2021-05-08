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
		[Description("On Split Start")] OnSplitStart,
		[Description("On Split End")] OnSplitEnd,
	}

	public enum OnResetEventType
	{
		[Description("Cancel prediction")] Cancel,
		[Description("Complete with Option 1")] CompleteWithOptionOne,
		[Description("Complete with Option 2")] CompleteWithOptionTwo
	}

	[Serializable]
	public class SplitsToEvents
	{
		[XmlAttribute] public bool UseMessageBoxes { get; set; }
		[XmlArrayItem] public List<SplitEvent> EventList { get; set; }
		[XmlElement] public OnResetEventType OnTimerResetBehaviour { get; set; }
		[XmlIgnore] public string Filename { get; set; }

		[Serializable]
		public class SplitEvent : ISplitEvent
		{
			[XmlIgnore] public string SegmentName { get; set; }
			public SplitEventType EventType { get; set; }

			public TimeSpan Delay { get; set; }
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
			public bool isUsed { get; set; }
			public string Header { get; set; }
			public string Answer1 { get; set; }
			public string Answer2 { get; set; }

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
				clone.isUsed = false;
				clone.Header = "";
				clone.Answer1 = "";
				clone.Answer2 = "";
				return clone;
			}
		}

		public SplitsToEvents()
		{
			UseMessageBoxes = false;
			EventList = new List<SplitEvent>();
			OnTimerResetBehaviour = OnResetEventType.Cancel;
			Filename = "";
		}

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
			XmlSerialiationDeserilation.SaveObjectToXML<SplitsToEvents>(this, Filename);
		}
	}
}

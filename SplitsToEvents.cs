using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LiveSplit.TwitchPredictions
{
	public class SplitsToEvents
	{
		[XmlAttribute] public bool UseMessageBoxes;
		[XmlArrayItem] public List<SplitEvent> EventList;
		[XmlElement] public OnResetEventType OnTimerResetBehaviour;
		[XmlIgnore] public string Filename;

		public enum SplitEventType
		{
			OnSpit,
			OnFinish,
			OnPB
		}

		public enum OnResetEventType
		{
			Cancel,
			CompleteWithOptionOne,
			CompleteWithOptionTwo
		}

		[Serializable]
		public class SplitEvent
		{
			[XmlIgnore] public string SegmentName;
			public int SplitIterator;
			public SplitEventType EventType;
			public int Delay;

			public SplitEvent()
			{
				SegmentName = "";
				SplitIterator = 0;
				EventType = SplitEventType.OnSpit;
				Delay = 0;
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
	}
}

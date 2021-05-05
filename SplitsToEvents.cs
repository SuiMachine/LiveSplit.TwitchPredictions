using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LiveSplit.TwitchPredictions
{
	public class SplitsToEvents
	{
		[XmlAttribute] public bool UseMessageBoxes;
		[XmlArrayItem] public List<SplitEvent> EventList;
		[XmlElement] public OnResetEventType OnTimerResetBehaviour;

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
			public int SplitIterator;
			public SplitEventType EventType;
			public int Delay;

			public SplitEvent()
			{
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
		}

	}
}

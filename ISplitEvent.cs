using System;

namespace LiveSplit.TwitchPredictions
{
	public interface ISplitEvent : ICloneable
	{
		string SegmentName { get; set; }
		SplitEventType EventType { get; set; }
		TimeSpan Delay { get; set; }
		SplitsToEvents.SplitAction Action { get; set; }
	}
}
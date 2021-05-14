using LiveSplit.Web;
using System;

namespace LiveSplit.TwitchPredictions
{
	internal class StreamPrediction
	{
		public enum PredictionStatus
		{
			RESOLVED,
			ACTIVE,
			CANCELED,
			LOCKED
		}

		internal class StreamPredictionOutcome
		{
			string ID;
			string Title;

			internal StreamPredictionOutcome()
			{
				ID = "";
				Title = "";
			}

			public static StreamPredictionOutcome[] Convert(object nodeRef)
			{
				var node = (dynamic)nodeRef;
				var returnNode1 = new StreamPredictionOutcome();
				var returnNode2 = new StreamPredictionOutcome();

				var subNode1 = node[0];
				var subNode2 = node[1];

				returnNode1.ID = subNode1["id"];
				returnNode1.Title = subNode1["title"];

				returnNode2.ID = subNode2["id"];
				returnNode2.Title = subNode2["title"];


				return new StreamPredictionOutcome[] { returnNode1, returnNode2 };
			}
		}

		public string ID { get; private set; }
		internal PredictionStatus Status;
		internal string Title;
		internal StreamPredictionOutcome FirstOutcome;
		internal StreamPredictionOutcome SecondOutcome;
		internal DateTime CreateAt;
		internal DateTime? LockedAt;
		internal DateTime? EndedAt;
		internal int PredictionWindow;

		internal StreamPrediction()
		{
			ID = "";
			Status = PredictionStatus.RESOLVED;
			Title = "";
			FirstOutcome = new StreamPredictionOutcome();
			SecondOutcome = new StreamPredictionOutcome();
			CreateAt = DateTime.MinValue;
			LockedAt = null;
			EndedAt = null;
			PredictionWindow = 120;
		}

		public static StreamPrediction ConvertNode(object nodeRef)
		{
			var node = (dynamic)nodeRef;
			var obj = new StreamPrediction();

			obj.ID = node["id"];
			obj.Status = Enum.Parse(typeof(PredictionStatus), node["status"]);
			obj.Title = node["title"];

			if (DateTime.TryParse(node["created_at"], out DateTime output1))
				obj.CreateAt = output1;

			if (DateTime.TryParse(node["ended_at"], out DateTime output2))
				obj.EndedAt = output2;

			if (DateTime.TryParse(node["locked_at"], out DateTime output3))
				obj.LockedAt = output3;

			obj.PredictionWindow = node["prediction_window"];
			var predictionResults = StreamPredictionOutcome.Convert(node["outcomes"]);
			obj.FirstOutcome = predictionResults[0];
			obj.SecondOutcome = predictionResults[1];

			return obj;
		}
	}
}
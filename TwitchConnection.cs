using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.TwitchPredictions
{
	class TwitchConnection
	{
		private TwitchPredictionsSettings settings;

		public TwitchConnection(TwitchPredictionsSettings settings)
		{
			this.settings = settings;
		}

		internal void Disconnect()
		{
			//throw new NotImplementedException();
		}
	}
}

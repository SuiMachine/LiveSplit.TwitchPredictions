using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveSplit.Web;

namespace LiveSplit.TwitchPredictions
{
	public static class TwitchRequests
	{
		private const string TwitchAPIURI = "https://api.twitch.tv/helix/";


		public static Uri BuildURI(string[] endpoint, Tuple<string, string>[] parameters)
		{
			var uri = TwitchAPIURI + string.Join("/", endpoint);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i == 0)
					sb.Append("?");
				else
					sb.Append("&");

				sb.Append(JSON.Escape(parameters[i].Item1));
				sb.Append("=");
				sb.Append(JSON.Escape(parameters[i].Item2));
			}
			return new Uri(uri + sb.ToString());
		}
	}
}

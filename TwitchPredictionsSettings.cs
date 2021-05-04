using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.TwitchPredictions
{
	public partial class TwitchPredictionsSettings : UserControl
	{
		public TwitchPredictionsSettings()
		{
			InitializeComponent();
		}

		internal XmlNode GetSettings(XmlDocument document)
		{
			throw new NotImplementedException();
		}

		internal void SetSettings(XmlNode settings)
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

		internal XmlNode GetSettings(XmlDocument doc)
		{
			XmlElement settingsNode = doc.CreateElement("Settings");

			settingsNode.AppendChild(ToElement(doc, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

			return settingsNode;
		}

		static XmlElement ToElement<T>(XmlDocument document, string name, T value)
		{
			XmlElement str = document.CreateElement(name);
			str.InnerText = value.ToString();
			return str;
		}

		internal void SetSettings(XmlNode settings)
		{
			//this.UseNonSafeMemoryReading = ParseBool(settings, "NonSafeMemoryReader", DEFAULT_UNSAFEREADER);
		}
	}
}

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
		private TwitchConnection _twitchConnection;
		public string Address { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Oauth { get; set; }
		public string Channel { get; set; }

		private string Filename;

		Model.LiveSplitState splitStates;

		public TwitchPredictionsSettings(Model.LiveSplitState splitStates)
		{
			InitializeComponent();
			this.splitStates = splitStates;
			_twitchConnection = TwitchConnection.GetInstance();
			TB_ServerAdress.DataBindings.Add("Text", this, "Address", false, DataSourceUpdateMode.OnPropertyChanged);
			NumB_Port.DataBindings.Add("Value", this, "Port", false, DataSourceUpdateMode.OnPropertyChanged);
			TB_UserName.DataBindings.Add("Text", this, "Username", false, DataSourceUpdateMode.OnPropertyChanged);
			TB_Oauth.DataBindings.Add("Text", this, "Oauth", false, DataSourceUpdateMode.OnPropertyChanged);
			TB_Channel.DataBindings.Add("Text", this, "Channel", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		internal void _state_RunManuallyModified(object sender, EventArgs e)
		{
			GetNewSplits();
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
			Address = _twitchConnection._connectionData.Address;
			Port = _twitchConnection._connectionData.Port;
			Username = _twitchConnection._connectionData.Username;
			Oauth = _twitchConnection._connectionData.Oauth;
			Channel = _twitchConnection._connectionData.Channel;
			GetNewSplits();
		}

		internal void GetNewSplits()
		{
			Filename = splitStates != null && splitStates.Run != null ? splitStates.Run.GameName + "##" + splitStates.Run.CategoryName : "Unknown";
		}
	}
}

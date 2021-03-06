using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.TwitchPredictions
{
	public partial class TwitchPredictionsSettings : UserControl
	{
		private string SubDir => Path.Combine("Components", "TwitchPredictions");

		private TwitchConnection _twitchConnection;
		public string Address { get; set; }
		public int Port { get; set; }
		public string Username { get; set; }
		public string Oauth { get; set; }
		public string Channel { get; set; }
		public bool ConnectOnLaunch { get; set; }

		private HTTPServer server;
		Model.LiveSplitState splitStates;
		public SplitsToEvents SplitsToEventsInstance { get; set; }
		public delegate void ResponseReceivedDelagate(string text, string type, string c);

		public TwitchPredictionsSettings(Model.LiveSplitState splitStates)
		{
			InitializeComponent();
			DebugLogging._settings = this;
			DebugLogging.Log("Created settings and set reference", true);
			this.splitStates = splitStates;
			_twitchConnection = TwitchConnection.GetInstance();

			//Set bindings
			TB_ServerAdress.DataBindings.Add("Text", this, "Address", false, DataSourceUpdateMode.OnPropertyChanged);
			NumB_Port.DataBindings.Add("Value", this, "Port", false, DataSourceUpdateMode.OnPropertyChanged);
			TB_UserName.DataBindings.Add("Text", this, "Username", false, DataSourceUpdateMode.OnPropertyChanged);
			TB_Oauth.DataBindings.Add("Text", this, "Oauth", false, DataSourceUpdateMode.OnPropertyChanged);
			TB_Channel.DataBindings.Add("Text", this, "Channel", false, DataSourceUpdateMode.OnPropertyChanged);
			CB_ConnectOnComponentLaunch.DataBindings.Add("Checked", this, "ConnectOnLaunch", false, DataSourceUpdateMode.OnPropertyChanged);
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
			ConnectOnLaunch = _twitchConnection._connectionData.ConnectOnLaunch;
			GetNewSplits();
		}

		internal void GetNewSplits()
		{
			if (!Directory.Exists(SubDir))
				Directory.CreateDirectory(SubDir);

			var Filename = ReplaceIncorrectCharacters(splitStates != null && splitStates.Run != null ? splitStates.Run.GameName + "##" + splitStates.Run.CategoryName : "Unknown");

			var fullPath = Path.Combine(SubDir, Filename + ".xml");
			if (File.Exists(fullPath))
				SplitsToEventsInstance = XmlSerialiationDeserilation.ReadFromXMLFile<SplitsToEvents>(fullPath);
			else
				SplitsToEventsInstance = new SplitsToEvents();

			SplitsToEventsInstance.Filename = fullPath;
		}

		private string ReplaceIncorrectCharacters(string filename)
		{
			var invalidChars = Path.GetInvalidFileNameChars();
			for (int i = 0; i < invalidChars.Length; i++)
			{
				filename.Replace(invalidChars[i], '_');
			}
			return filename;
		}

		private void B_SaveSettings_Click(object sender, EventArgs e)
		{
			_twitchConnection._connectionData.Address = Address;
			_twitchConnection._connectionData.Port = Port;
			_twitchConnection._connectionData.Username = Username;
			_twitchConnection._connectionData.Oauth = Oauth;
			_twitchConnection._connectionData.Channel = Channel;
			_twitchConnection._connectionData.ConnectOnLaunch = ConnectOnLaunch;
			_twitchConnection.SaveConfig();
		}

		private void B_GenerateAouth_Click(object sender, EventArgs e)
		{
			if (HTTPServer.IsSupported())
			{
				if (server != null)
				{
					server.OnReceivedResultEvent -= Server_OnReceivedResultEvent;
					server.CloseHttpListener();
				}
				server = new HTTPServer();
				server.OnReceivedResultEvent += Server_OnReceivedResultEvent;
			}
			else
				MessageBox.Show("HTTPServer is not supported. Maybe try running LiveSplit as administrator?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private void Server_OnReceivedResultEvent(string token, string scope, string tokentype)
		{
			if (this.InvokeRequired)
			{
				ResponseReceivedDelagate d = new ResponseReceivedDelagate(Server_OnReceivedResultEvent);
				this.Invoke(d, new object[] { token, scope, tokentype });
			}
			else if (token != "")
			{
				TB_Oauth.Text = token;
				Oauth = token;

				if (MessageBox.Show("Received Authorization Token. Do you want to save it now?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
					B_SaveSettings_Click(null, null);
			}
		}

		private void B_EditSplitEvents_Click(object sender, EventArgs e)
		{
			SplitEventsEditorForm splitEventsEditor = new SplitEventsEditorForm(splitStates, (SplitsToEvents)SplitsToEventsInstance.Clone());
			if (splitEventsEditor.ShowDialog() == DialogResult.OK)
			{
				SplitsToEventsInstance = splitEventsEditor.splitToEvents;
			}
		}

		private void B_Connect_Click(object sender, EventArgs e)
		{
			TwitchConnection.GetInstance().Connect();
		}
	}
}

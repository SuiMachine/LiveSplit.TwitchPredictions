using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LiveSplit.TwitchPredictions
{
	public class TwitchConnection
	{
		private static TwitchConnection _Instance;

		internal static TwitchConnection GetInstance() { return _Instance != null ? _Instance : (_Instance = new TwitchConnection()); }

		string USER_DIRECTORY => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LiveSplit.TwitchPredictions");
		string USER_FILE => "Config.xml";

		internal TwitchConnectionData _connectionData;
		private IrcDotNet.IrcClient _irc;

		[Serializable]
		public class TwitchConnectionData
		{
			public string Address { get; set; }
			public int Port { get; set; }

			public string Username { get; set; }
			public string Oauth { get; set; }
			public string Channel { get; set; }

			public TwitchConnectionData()
			{
				Address = "irc.chat.twitch.tv";
				Port = 6667;
				Username = "";
				Oauth = "";
				Channel = "";
			}
		}

		public TwitchConnection()
		{
			_connectionData = XmlSerialiationDeserilation.ReadFromXMLFile<TwitchConnectionData>(Path.Combine(USER_DIRECTORY, USER_FILE));
		}

		//https://dev.twitch.tv/docs/api/reference#create-prediction

		internal void Connect()
		{
			if (_irc == null)
				_irc = new IrcDotNet.IrcClient();

			_irc.Connect(_connectionData.Address, _connectionData.Port, new IrcDotNet.IrcUserRegistrationInfo() { UserName = _connectionData.Username, Password = _connectionData.Oauth });
		}

		internal void Disconnect()
		{
			if (_irc != null && _irc.IsConnected)
				_irc.Disconnect();
		}

		internal void SaveConfig()
		{
			try
			{
				XmlSerialiationDeserilation.SaveObjectToXML(_connectionData, Path.Combine(USER_DIRECTORY, USER_FILE));
			}
			catch(Exception e)
			{
				MessageBox.Show("Failed to store config to a file: " + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}

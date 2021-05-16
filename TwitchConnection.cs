using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit.TwitchPredictions
{
	public class TwitchConnection
	{
		private static TwitchConnection _Instance;
		private TwitchRequests twitchRequests;
		public const string ClientID = "sz9g0b3arar4db1l4is6dk95wj9sfo";

		internal static TwitchConnection GetInstance() { return _Instance != null ? _Instance : (_Instance = new TwitchConnection()); }

		string USER_DIRECTORY => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LiveSplit.TwitchPredictions");
		string USER_FILE => "Config.xml";

		internal TwitchConnectionData _connectionData;
		private IrcDotNet.IrcClient _irc;
		internal StreamPrediction CurrentPrediction { get; private set; }

		[Serializable]
		public class TwitchConnectionData
		{
			public string Address { get; set; }
			public int Port { get; set; }

			public string Username { get; set; }
			public string Oauth { get; set; }
			public string Channel { get; set; }
			public bool ConnectOnLaunch { get; set; }

			public TwitchConnectionData()
			{
				Address = "irc.chat.twitch.tv";
				Port = 6667;
				Username = "";
				Oauth = "";
				Channel = "";
				ConnectOnLaunch = false;
			}
		}

		public TwitchConnection()
		{
			_connectionData = XmlSerialiationDeserilation.ReadFromXMLFile<TwitchConnectionData>(Path.Combine(USER_DIRECTORY, USER_FILE));
			twitchRequests = new TwitchRequests(_connectionData.Channel, _connectionData.Oauth);
		}

		//https://dev.twitch.tv/docs/api/reference#create-prediction

		internal void Connect()
		{
			DebugLogging.Log("Connecting");

			if (_irc == null)
				_irc = new IrcDotNet.IrcClient();
			else
			{
				_irc.Disconnect();
				_irc.ErrorMessageReceived -= _irc_ErrorMessageReceived;
				_irc.Connected -= _irc_Connected;
				_irc.Disconnected -= _irc_Disconnected;
				_irc.ClientInfoReceived -= _irc_ClientInfoReceived;
				_irc.RawMessageReceived -= _irc_RawMessageReceived;
				_irc.Registered -= _irc_Registered;
			}

			_irc.Connect(_connectionData.Address, _connectionData.Port, new IrcDotNet.IrcUserRegistrationInfo() { NickName = _connectionData.Username, UserName = _connectionData.Username, RealName = _connectionData.Username, Password = "oauth:" + _connectionData.Oauth });
			_irc.ErrorMessageReceived += _irc_ErrorMessageReceived;
			_irc.Connected += _irc_Connected;
			_irc.Disconnected += _irc_Disconnected;
			_irc.ClientInfoReceived += _irc_ClientInfoReceived;
			_irc.RawMessageReceived += _irc_RawMessageReceived;
			_irc.Registered += _irc_Registered;
		}

		public async void StartNewPrediction(string Header, string Option1, string Option2, uint Lenght)
		{
			var newPrediction = await twitchRequests.StartPredictionAsync(Header, Option1, Option2, Lenght);
			if (newPrediction != null)
				CurrentPrediction = newPrediction;
		}

		public async void CompletePrediction(int winningOutcome)
		{
			var result = await twitchRequests.CompleteWithOptionAsync(winningOutcome);
			CurrentPrediction = result;
		}

		public async void CancelPrediction()
		{
			var result = await twitchRequests.CancelPredictionAsync();
			CurrentPrediction = result;
		}

		private void _irc_Registered(object sender, EventArgs e)
		{
			DebugLogging.Log("[IRC] Registered");
			JoinChannel(_connectionData.Channel);
		}

		private void JoinChannel(string channel)
		{
			_irc.Channels.Join(new string[] { "#" + channel.ToLower() });
		}

		private void LocalUser_JoinedChannel(object sender, IrcDotNet.IrcChannelEventArgs e)
		{

			DebugLogging.Log("[IRC] Joined channel: " + e.Channel.Name);
		}

		private void _irc_RawMessageReceived(object sender, IrcDotNet.IrcRawMessageEventArgs e)
		{
			DebugLogging.Log("[IRC] MSG: " + (e.Message.Source != null ? e.Message.Source + ": " : "") + string.Join(" ", e.Message.Parameters.Where(x => x != null).ToArray()));
		}

		private void _irc_ErrorMessageReceived(object sender, IrcDotNet.IrcErrorMessageEventArgs e)
		{
			DebugLogging.Log("[IRC] Error Received: " + e.Message);
		}

		private void _irc_ClientInfoReceived(object sender, EventArgs e)
		{
			DebugLogging.Log("[IRC] Client Info Received.");
		}

		private void _irc_Connected(object sender, EventArgs e)
		{
			DebugLogging.Log("[IRC] Connected!");
		}

		private void _irc_Disconnected(object sender, EventArgs e)
		{
			DebugLogging.Log("[IRC] Disconnected!");
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
				MessageBox.Show("Setting were stored in: %APPDATA%\\LiveSplit.TwitchPredictions to prevent accidently sharing login information in case of sharing layouts.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception e)
			{
				MessageBox.Show("Failed to store config to a file: " + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}

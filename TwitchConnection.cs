using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace LiveSplit.TwitchPredictions
{
	public class TwitchConnection
	{
		#region ExternalEvents
		public event Events.TwitchConnectionEvents.OnMessageReceived OnMessageReceived;
		#endregion

		private static TwitchConnection _Instance;
		private IrcDotNet.IrcUser userData;
		public const string ClientID = "sz9g0b3arar4db1l4is6dk95wj9sfo";
		public string BroadcasterID = "";

		internal static TwitchConnection GetInstance() { return _Instance != null ? _Instance : (_Instance = new TwitchConnection()); }

		string USER_DIRECTORY => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LiveSplit.TwitchPredictions");
		string USER_FILE => "Config.xml";

		internal TwitchConnectionData _connectionData;
		private IrcDotNet.IrcClient _irc;
		private StreamPrediction currentPrediction;

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
		}

		//https://dev.twitch.tv/docs/api/reference#create-prediction

		internal void Connect()
		{
			DebugLogging.Log("Connecting");
			//Move that after connection and events are set!
			TwitchRequests.ProvideBearerToken (_connectionData.Oauth, _connectionData.Channel);
			TwitchRequests.GetUserID();

			//currentPrediction = TwitchRequests.GetCurrentPrediction();

			if (_irc == null)
				_irc = new IrcDotNet.IrcClient();
			else
			{
				_irc.Disconnect();
			}

			_irc.Connect(_connectionData.Address, _connectionData.Port, new IrcDotNet.IrcUserRegistrationInfo() { NickName = _connectionData.Username, UserName = _connectionData.Username, RealName = _connectionData.Username, Password = "oauth:" +_connectionData.Oauth });
			_irc.ErrorMessageReceived += _irc_ErrorMessageReceived;
			_irc.Connected += _irc_Connected;
			_irc.Disconnected += _irc_Disconnected;
			_irc.ClientInfoReceived += _irc_ClientInfoReceived;
			_irc.RawMessageReceived += _irc_RawMessageReceived;
			_irc.ChannelListReceived += _irc_ChannelListReceived;
			_irc.Registered += _irc_Registered;
			_irc.ChannelListReceived += _irc_ChannelListReceived1;
		}

		public void StartNewPrediction(string Header, string Option1, string Option2, uint Lenght)
		{
			var result = TwitchRequests.StartPrediction(Header, Option1, Option2, Lenght, out StreamPrediction newPrediction);
			if (result == TwitchRequests.StartPredictionResult.Successful)
				currentPrediction = newPrediction;
		}

		private void _irc_ChannelListReceived1(object sender, IrcDotNet.IrcChannelListReceivedEventArgs e)
		{
			DebugLogging.Log("hhh");
		}

		private void _irc_Registered(object sender, EventArgs e)
		{
			DebugLogging.Log("[IRC] Registered");

			JoinChannel("suicidemachinebot");
		}

		private void JoinChannel(string channel)
		{
			_irc.Channels.Join(new string[] { "#" + channel.ToLower() });
		}

		private void _irc_ChannelListReceived(object sender, IrcDotNet.IrcChannelListReceivedEventArgs e)
		{
			if(!e.Channels.Any(x => x.Name == _connectionData.Channel))
			{
				DebugLogging.Log("F");
			}
			else
				DebugLogging.Log("E");

		}

		private void LocalUser_LeftChannel(object sender, IrcDotNet.IrcChannelEventArgs e)
		{
			DebugLogging.Log("[IRC] Left channel: " + e.Channel.Name);
		}

		private void LocalUser_JoinedChannel(object sender, IrcDotNet.IrcChannelEventArgs e)
		{

			DebugLogging.Log("[IRC] Joined channel: " + e.Channel.Name);
		}

		private void _irc_RawMessageReceived(object sender, IrcDotNet.IrcRawMessageEventArgs e)
		{
			DebugLogging.Log("[IRC] Raw Message Received: " + e.ToString());
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
			catch(Exception e)
			{
				MessageBox.Show("Failed to store config to a file: " + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}

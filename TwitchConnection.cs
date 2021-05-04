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
		internal static TwitchConnection _Instance; 

		string USER_DIRECTORY => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LiveSplit.TwitchPredictions");
		string USER_FILE => "Config.xml";

		private TwitchPredictionsSettings settings;
		private TwitchConnectionData _connectionData;
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

		public TwitchConnection(TwitchPredictionsSettings settings)
		{
			_connectionData = ReadFromXMLFile<TwitchConnectionData>(Path.Combine(USER_DIRECTORY, USER_FILE));
			this.settings = settings;
		}

		//https://dev.twitch.tv/docs/api/reference#create-prediction

		internal void Connect()
		{
			if (_irc == null)
				_irc = new IrcDotNet.IrcClient();
		}

		internal void Disconnect()
		{
			if (_irc != null && _irc.IsConnected)
				_irc.Disconnect();
		}

		public static T ReadFromXMLFile<T>(string filePath) where T : new()
		{
			StreamReader reader = null;
			try
			{
				if (!File.Exists(filePath))
					return new T();

				XmlSerializer serializer = new XmlSerializer(typeof(T));
				reader = new StreamReader(filePath);
				return (T)serializer.Deserialize(reader);
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		internal void SaveConfig()
		{
			SaveObjectToXML(_connectionData, Path.Combine(USER_DIRECTORY, USER_FILE));
		}

		public static void SaveObjectToXML<T>(T objectToStore, string filePath) where T : new()
		{
			if (!Directory.Exists(Directory.GetParent(filePath).FullName))
				Directory.CreateDirectory(Directory.GetParent(filePath).FullName);

			StreamWriter writter = null;

			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				writter = new StreamWriter(filePath);
				serializer.Serialize(writter, objectToStore);
			}
			catch (Exception e)
			{
				MessageBox.Show("Failed to store config data to a file:\n" + e, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			finally
			{
				if (writter != null)
					writter.Close();
			}
		}
	}
}

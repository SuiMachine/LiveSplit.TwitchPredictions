using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.TwitchPredictions
{
	public class TwitchPredictionsComponent : LogicComponent
	{
		public override string ComponentName { get { return "Twitch Predictions"; }	}

		public TwitchPredictionsSettings Settings { get; set; }
		public bool Disposed { get; private set; }
		public bool IsLayoutComponent { get; private set; }

		private TimerModel _timer;
		private TwitchConnection _twitchConnection;
		private LiveSplitState _state;

		public TwitchPredictionsComponent(LiveSplitState state, bool isLayoutComponent)
		{
			_state = state;
			this.IsLayoutComponent = isLayoutComponent;
			this.Settings = new TwitchPredictionsSettings(state);
			_state.RunManuallyModified += Settings._state_RunManuallyModified;

			_timer = new TimerModel { CurrentState = state };
			_twitchConnection = TwitchConnection.GetInstance();
			state.OnStart += State_OnStart;
			if (_twitchConnection._connectionData.ConnectOnLaunch)
				_twitchConnection.Connect();

			_timer.OnReset += _timer_OnReset;

		}

		private void _timer_OnReset(object sender, TimerPhase value)
		{
			Settings.SplitsToEventsInstance.DoResetEvent();
		}

		public override void Dispose()
		{
			this.Disposed = true;

			_state.OnStart -= State_OnStart;
			_state.RunManuallyModified -= Settings._state_RunManuallyModified;

			if (_twitchConnection != null)
			{
				_twitchConnection.Disconnect();
			}
		}

		//May be removed?
		private void State_OnStart(object sender, EventArgs e)
		{
		}


		public override XmlNode GetSettings(XmlDocument document)
		{
			return this.Settings.GetSettings(document);
		}

		public override Control GetSettingsControl(LayoutMode mode)
		{
			return this.Settings;
		}

		public override void SetSettings(XmlNode settings)
		{
			this.Settings.SetSettings(settings);
		}

		public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
	}
}

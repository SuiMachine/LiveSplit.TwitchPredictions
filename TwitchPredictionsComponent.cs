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
		public override string ComponentName { get { return "Twitch Predictions"; } }

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
			if (_twitchConnection._connectionData.ConnectOnLaunch)
				_twitchConnection.Connect();

			//Set required timer events
			_timer.CurrentState.OnStart += CurrentState_OnStart;
			_timer.CurrentState.OnSkipSplit -= CurrentState_OnSplit; //essentially identical on split
			_timer.CurrentState.OnSplit += CurrentState_OnSplit;
			_timer.CurrentState.OnReset += CurrentState_OnReset;
		}

		private void CurrentState_OnStart(object sender, EventArgs e)
		{
			Settings.SplitsToEventsInstance.ClearWasUsedFlags();
			Settings.SplitsToEventsInstance.DoSplitEvent(0);
		}

		private void CurrentState_OnSplit(object sender, EventArgs e)
		{
			var cast = (LiveSplitState)sender;
			var runEnded = cast.CurrentSplit == null;
			if (!runEnded)
				Settings.SplitsToEventsInstance.DoSplitEvent(cast.CurrentSplitIndex);
			else
			{
				var currentTime = cast.CurrentTime;
				var pbTime = cast.Run[cast.CurrentSplitIndex - 1].PersonalBestSplitTime;
				bool isPB;
				if (cast.CurrentTimingMethod == TimingMethod.RealTime)
					isPB = cast.CurrentTime.RealTime < pbTime.RealTime;
				else
					isPB = currentTime.GameTime != null && pbTime.GameTime != null ? currentTime.GameTime < pbTime.GameTime : cast.CurrentTime.RealTime < pbTime.RealTime;

				Settings.SplitsToEventsInstance.DoCompleteRunEvent(isPB);
			}
		}

		private void CurrentState_OnReset(object sender, TimerPhase value)
		{
			Settings.SplitsToEventsInstance.DoResetEvent();
		}

		public override void Dispose()
		{
			this.Disposed = true;

			_state.RunManuallyModified -= Settings._state_RunManuallyModified;
			_timer.CurrentState.OnStart -= CurrentState_OnStart;
			_timer.CurrentState.OnSkipSplit -= CurrentState_OnStart;
			_timer.CurrentState.OnSplit -= CurrentState_OnSplit;
			_timer.CurrentState.OnReset -= CurrentState_OnReset;

			if (_twitchConnection != null)
			{
				_twitchConnection.Disconnect();
			}
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

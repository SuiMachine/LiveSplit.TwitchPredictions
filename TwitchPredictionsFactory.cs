using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.TwitchPredictions
{
	public class TwitchPredictionsFactory : IComponentFactory
	{

		private TwitchPredictionsComponent _instance;

		public string ComponentName
		{
			get { return "Twitch Predictions"; }
		}
		public ComponentCategory Category
		{
			get { return ComponentCategory.Control; }
		}
		public string Description
		{
			get { return "Allows to run Twitch predictions based on splits."; }
		}
		public IComponent Create(Model.LiveSplitState state)
		{
			// workaround for livesplit 1.4 oversight where components can be loaded from two places at once
			// remove all this junk when they fix it
			string caller = new StackFrame(1).GetMethod().Name;
			string callercaller = new StackFrame(2).GetMethod().Name;
			bool createAsLayoutComponent = (caller == "LoadLayoutComponent" || caller == "AddComponent");

			// if component is already loaded somewhere else
			if (_instance != null && !_instance.Disposed)
			{
				// "autosplit components" can't throw exceptions for some reason, so return a dummy component
				if (callercaller == "CreateAutoSplitter")
				{
					return new DummyComponent();
				}

				MessageBox.Show(
					"LiveSplit.TwitchPredictions is already loaded in the " +
						(_instance.IsLayoutComponent ? "Layout Editor" : "Splits Editor") + "!",
					"Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);

				throw new Exception("Component already loaded.");
			}

			return (_instance = new TwitchPredictionsComponent(state, createAsLayoutComponent));
		}
		public string UpdateName
		{
			get { return ComponentName; }
		}
		public string UpdateURL
		{
			get { return "https://raw.githubusercontent.com/SuiMachine/LiveSplit.TwitchPredictions/master/"; }
		}
		public Version Version
		{
			get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version; }
		}
		public string XMLURL
		{
			get { return UpdateURL + "Components/update.LiveSplit.TwitchPredictions.xml"; }
		}
	}

	class DummyComponent : LogicComponent
	{
		public override string ComponentName { get { return "Dummy Component"; } }
		public override void Dispose() { }
		public override XmlNode GetSettings(XmlDocument document) { return document.CreateElement("Settings"); }
		public override Control GetSettingsControl(LayoutMode mode) { return null; }
		//public override void RenameComparison(string oldName, string newName) { }
		public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
		public override void SetSettings(XmlNode settings) { }
	}
}
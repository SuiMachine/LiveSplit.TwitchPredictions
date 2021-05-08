using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.TwitchPredictions
{
	public partial class SplitEventsEditorForm : Form
	{
		Model.LiveSplitState splitStates;
		SplitsToEvents splitToEvents;
		bool wasChanged = false;

		public SplitEventsEditorForm(Model.LiveSplitState splitStates, SplitsToEvents splitToEvents)
		{
			this.splitStates = splitStates;
			this.splitToEvents = splitToEvents;
			InitializeComponent();
			var verficationResult = splitToEvents.Verify(splitStates);
			if (verficationResult != "")
			{
				MessageBox.Show("Verification found following issues:\n" + verficationResult, "Notification", MessageBoxButtons.OK);
			}

			grid_SplitSettings.DataSource = this.splitToEvents.EventList;
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			if (wasChanged && MessageBox.Show("Are you sure you want to close it and abandon changes?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Close();
			}
		}

		#region Getters/Setters
		//Partially coppied from Run Edit Dialog
		public string GameName
		{
			get { return splitStates.Run.GameName; }
			set
			{
				if (splitStates.Run.GameName != value)
				{
					splitStates.Run.GameName = value;
				}
			}
		}

		public string CategoryName
		{
			get { return splitStates.Run.CategoryName; }
			set
			{
				if (splitStates.Run.CategoryName != value)
				{
					splitStates.Run.CategoryName = value;
				}
			}
		}
		#endregion
	}
}

using LiveSplit.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

		#region Grid_Constants
		int COLUMNINDEX_SEGMENTNAME = 0;
		int COLUMNINDEX_EVENT = 1;
		int COLUMNINDEX_DELAY = 2;
		int COLUMNINDEX_ACTION = 3;
		#endregion

		protected BindingList<ISplitEvent> splitToEventList { get; set; }


		public SplitEventsEditorForm(Model.LiveSplitState splitStates, SplitsToEvents splitToEvents)
		{
			this.splitStates = splitStates;
			this.splitToEvents = splitToEvents;
			InitializeComponent();
			var verficationResult = splitToEvents.Verify(splitStates);
			if (verficationResult != "")
			{
				//MessageBox.Show("Verification found following issues:\n" + verficationResult, "Notification", MessageBoxButtons.OK);
			}

			splitToEventList = new BindingList<ISplitEvent>(splitToEvents.EventList) { AllowNew = false, AllowRemove = false };
			grid_SplitSettings.AutoGenerateColumns = false;
			grid_SplitSettings.AutoSize = true;
			grid_SplitSettings.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
			grid_SplitSettings.DataSource = splitToEventList;

			var segmentNameColumn = new DataGridViewTextBoxColumn();
			segmentNameColumn.Name = "SegmentName";
			segmentNameColumn.Width = 100;
			segmentNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			segmentNameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			grid_SplitSettings.Columns.Add(segmentNameColumn);

			var eventTypeColumn = new DataGridViewComboBoxColumn();
			eventTypeColumn.Name = "Event Type";
			eventTypeColumn.Width = 100;
			eventTypeColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			eventTypeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			eventTypeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			grid_SplitSettings.Columns.Add(eventTypeColumn);

			var eventDelayColumn = new DataGridViewTextBoxColumn();
			eventDelayColumn.Name = "Event delay";
			eventDelayColumn.Width = 100;
			eventDelayColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			eventDelayColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			eventDelayColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			grid_SplitSettings.Columns.Add(eventDelayColumn);

			var eventActionColumn = new DataGridViewButtonColumn();
			eventActionColumn.Name = "Action";
			eventActionColumn.Width = 100;
			eventActionColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			eventActionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			eventActionColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			grid_SplitSettings.Columns.Add(eventActionColumn);
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			if (wasChanged && MessageBox.Show("Are you sure you want to close it and abandon changes?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.Close();
			}
			else
			{
				this.Close();
			}
		}

		private void grid_SplitSettings_KeyDown(object sender, KeyEventArgs e)
		{
/*			if (e.KeyCode == Keys.Delete)
			{
				foreach (var selectedObject in grid_SplitSettings.SelectedCells.OfType<DataGridViewCell>().Reverse())
				{
					var selectedCell = selectedObject;

					if (selectedCell.ColumnIndex == COLUMNINDEX_ACTION)
					{
						splitToEvents.EventList[selectedCell.RowIndex].Action.isUsed = false;
						splitToEvents.EventList[selectedCell.RowIndex].Action.Header = "";
						splitToEvents.EventList[selectedCell.RowIndex].Action.Answer1 = "";
						splitToEvents.EventList[selectedCell.RowIndex].Action.Answer2 = "";
						SetEdited();
					}
				}

				grid_SplitSettings.Invalidate();
			}*/
		}

		private void SetEdited()
		{
			wasChanged = true;
		}


		public void SetGraphicsAndHints()
		{
			return;
			for (int i = 0; i < grid_SplitSettings.Rows.Count; i++)
			{
				var actionButton = (Button)grid_SplitSettings[COLUMNINDEX_ACTION, i].Value;

			}
		}
	}
}

using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit.TwitchPredictions
{
	public partial class SplitEventsEditorForm : Form
	{
		Model.LiveSplitState splitStates;
		public SplitsToEvents splitToEvents;
		bool wasChanged = false;
		protected ITimeFormatter TimeFormatter { get; set; }

		#region Grid_Constants
		int COLUMNINDEX_SEGMENTNAME = 0;
		int COLUMNINDEX_EVENT = 1;
		int COLUMNINDEX_DELAY = 2;
		int COLUMNINDEX_ACTION = 3;
		#endregion

		protected BindingList<ISplitEvent> splitToEventList { get; set; }
		private Control eCtl;

		#region Stuff responsible for intiation and display
		public SplitEventsEditorForm(Model.LiveSplitState splitStates, SplitsToEvents splitToEvents)
		{
			this.splitStates = splitStates;
			this.splitToEvents = splitToEvents;
			InitializeComponent();
			this.DialogResult = DialogResult.Cancel;
			var verficationResult = splitToEvents.Verify(splitStates);
			if (verficationResult != "")
			{
				//MessageBox.Show("Verification found following issues:\n" + verficationResult, "Notification", MessageBoxButtons.OK);
			}

			#region Setting up grid
			TimeFormatter = new ShortTimeFormatter();

			splitToEventList = new BindingList<ISplitEvent>(splitToEvents.EventList.Cast<ISplitEvent>().ToList()) { AllowNew = false, AllowRemove = false };
			grid_SplitSettings.AutoGenerateColumns = false;
			grid_SplitSettings.AutoSize = true;
			grid_SplitSettings.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
			grid_SplitSettings.DataSource = splitToEventList;
			grid_SplitSettings.CellClick += Grid_SplitSettings_CellClick;
			grid_SplitSettings.CellFormatting += Grid_SplitSettings_CellFormatting;
			grid_SplitSettings.CellParsing += Grid_SplitSettings_CellParsing;
			grid_SplitSettings.CellValidating += Grid_SplitSettings_CellValidating;
			grid_SplitSettings.CellEndEdit += Grid_SplitSettings_CellEndEdit;
			grid_SplitSettings.SelectionChanged += Grid_SplitSettings_SelectionChanged;

			var segmentNameColumn = new DataGridViewTextBoxColumn();
			segmentNameColumn.Name = "Segment Name";
			segmentNameColumn.Width = 350;
			segmentNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			segmentNameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			grid_SplitSettings.Columns.Add(segmentNameColumn);

			var eventTypeColumn = new DataGridViewComboBoxColumn();

			//Hide your children
			eventTypeColumn.Items.AddRange(Enum.GetValues(typeof(SplitEventType)).Cast<Enum>().Select(value => new
			{
				(Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description
			}.Description).ToArray());
			eventTypeColumn.Name = "Event Type";
			eventTypeColumn.Width = 200;
			eventTypeColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			eventTypeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			eventTypeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			grid_SplitSettings.Columns.Add(eventTypeColumn);

			var eventDelayColumn = new DataGridViewTextBoxColumn();
			eventDelayColumn.Name = "Event delay";
			eventDelayColumn.Width = 80;
			eventDelayColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			eventDelayColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			eventDelayColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			grid_SplitSettings.Columns.Add(eventDelayColumn);

			var eventActionColumn = new DataGridViewButtonColumn();
			eventActionColumn.Name = "Action";
			eventActionColumn.Width = 50;
			eventActionColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			eventActionColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			eventActionColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			grid_SplitSettings.Columns.Add(eventActionColumn);

			grid_SplitSettings.EditingControlShowing += Grid_SplitSettings_EditingControlShowing;
			#endregion

			AddComboboxDataSources();
			CBox_OnRunReset.DataBindings.Add("SelectedValue", this.splitToEvents, "OnTimerResetBehaviour", false, DataSourceUpdateMode.OnPropertyChanged);
			CBox_RunCompletion.DataBindings.Add("SelectedValue", this.splitToEvents, "OnRunCompletion", false, DataSourceUpdateMode.OnPropertyChanged);
			CB_UsePBPrediction.DataBindings.Add("Checked", this.splitToEvents, "UsePBPrediction", false, DataSourceUpdateMode.OnPropertyChanged);
			CB_NotifyOfErrorsInChat.DataBindings.Add("Checked", this.splitStates, "NotifyOfErrorsInChat", false, DataSourceUpdateMode.OnPropertyChanged);
		}

		private void Grid_SplitSettings_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.RowIndex < splitToEventList.Count)
			{
				if (e.ColumnIndex == COLUMNINDEX_ACTION)
				{
					var action = splitToEventList[e.RowIndex].Action;
					Forms.ActionEditor form = new Forms.ActionEditor(action);
					if (form.ShowDialog() == DialogResult.OK)
					{
						splitToEventList[e.RowIndex].Action = form.ReturnedAction;
						SetDirty();
					}
				}
			}
		}

		private void AddComboboxDataSources()
		{
			CBox_OnRunReset.DisplayMember = "Description";
			CBox_OnRunReset.ValueMember = "value";
			CBox_OnRunReset.DataSource = Enum.GetValues(typeof(OnResetEventType)).Cast<Enum>().Select(value =>
			new
			{
				(Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()),
				typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
				value
			}).ToList();

			CBox_RunCompletion.DisplayMember = "Description";
			CBox_RunCompletion.ValueMember = "value";
			CBox_RunCompletion.DataSource = Enum.GetValues(typeof(OnResetEventType)).Cast<Enum>().Select(value =>
			new
			{
				(Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()),
				typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
				value
			}).ToList();
		}
		#endregion

		private void Grid_SplitSettings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < splitToEventList.Count)
			{
				if (e.ColumnIndex == COLUMNINDEX_SEGMENTNAME)
				{
					e.Value = splitToEventList[e.RowIndex].SegmentName;
				}
				else if (e.ColumnIndex == COLUMNINDEX_EVENT)
				{
					e.Value = SplitsToEvents.GetEnumDescription(splitToEventList[e.RowIndex].EventType);
				}
				else if (e.ColumnIndex == COLUMNINDEX_DELAY)
				{
					var comparisonValue = splitToEventList[e.RowIndex].Delay;
					if (comparisonValue == null)
					{
						e.Value = "";
						e.FormattingApplied = false;
					}
					else
					{
						e.Value = TimeFormatter.Format(comparisonValue);
						e.FormattingApplied = true;
					}
				}
				else if (e.ColumnIndex == COLUMNINDEX_ACTION)
				{
					bool eventUsed = splitToEventList[e.RowIndex].Action.isUsed && splitToEventList[e.RowIndex].EventType == SplitEventType.StartPredictionOnSplitStart;
					e.Value = eventUsed ? "!" : "";
					e.CellStyle.BackColor = eventUsed ? Color.Red : Color.Transparent;
				}
			}
		}

		#region Validation of Numeric data
		private void Grid_SplitSettings_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (e.ColumnIndex == COLUMNINDEX_DELAY)
			{
				if (string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
					return;

				try
				{
					TimeSpanParser.Parse(e.FormattedValue.ToString());
				}
				catch
				{
					e.Cancel = true;
					grid_SplitSettings.Rows[e.RowIndex].ErrorText = "Invalid Time";
				}
			}
		}

		private void Grid_SplitSettings_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			grid_SplitSettings.Rows[e.RowIndex].ErrorText = "";
		}
		#endregion

		private void Grid_SplitSettings_SelectionChanged(object sender, EventArgs e)
		{
			UpdateButtonsStatus();
		}

		private void UpdateButtonsStatus()
		{
			B_MoveUp.Enabled = splitToEventList.Count > 1;
			List<DataGridViewCell> selectedCells = grid_SplitSettings.SelectedCells.Cast<DataGridViewCell>().OrderBy(o => o.RowIndex).ToList();

			if (selectedCells.FirstOrDefault() != null)
			{
				B_MoveUp.Enabled = selectedCells.First().RowIndex > 0;
				B_MoveDown.Enabled = selectedCells.Last().RowIndex < splitToEventList.Count - 1;
			}
			else
			{
				B_MoveUp.Enabled = false;
				B_MoveDown.Enabled = false;
			}
		}

		private class ParsingResults
		{
			public bool Parsed { get; set; }
			public object Value { get; set; }

			public ParsingResults(bool parsed, object value)
			{
				Parsed = parsed;
				Value = value;
			}
		}

		private void Grid_SplitSettings_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
		{
			//Do not parse segment names, events and actions
			if (e.ColumnIndex == COLUMNINDEX_SEGMENTNAME || e.ColumnIndex == COLUMNINDEX_ACTION)
				return;

			//Parse timespans
			var parsingResults = ParseCell(e.Value, e.RowIndex, e.ColumnIndex, true);
			if (parsingResults.Parsed)
			{
				e.ParsingApplied = true;
				e.Value = parsingResults.Value;
			}
			else
				e.ParsingApplied = false;
		}

		private ParsingResults ParseCell(object value, int rowIndex, int columnIndex, bool v)
		{
			if (string.IsNullOrWhiteSpace(value.ToString()))
			{
				value = null;
				if (columnIndex == COLUMNINDEX_DELAY)
				{
					splitToEventList[rowIndex].Delay = TimeSpan.Zero;
					SetDirty();
				}

				return new ParsingResults(true, value);
			}

			if (columnIndex == COLUMNINDEX_EVENT)
			{
				var values = Enum.GetValues(typeof(SplitEventType)).Cast<Enum>().Select(enumValues => new
				{
					(Attribute.GetCustomAttribute(enumValues.GetType().GetField(enumValues.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
					enumValues
				}).ToList();

				var enumVal = values.FindIndex(x => x.Description == value.ToString());
				splitToEventList[rowIndex].EventType = (SplitEventType)enumVal;
				SetDirty();
			}

			try
			{
				value = TimeSpanParser.Parse(value.ToString());
				if (columnIndex == COLUMNINDEX_DELAY)
				{
					splitToEventList[rowIndex].Delay = (TimeSpan)value;
					SetDirty();
				}

				return new ParsingResults(true, value);
			}
			catch { }

			return new ParsingResults(false, null);
		}

		private void Grid_SplitSettings_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			eCtl = e.Control;
			/*			eCtl.TextChanged -= new EventHandler(eCtl_TextChanged);
						eCtl.KeyPress -= new KeyPressEventHandler(eCtl_KeyPress);
						eCtl.TextChanged += new EventHandler(eCtl_TextChanged);
						eCtl.KeyPress += new KeyPressEventHandler(eCtl_KeyPress);*/
		}

		private void grid_SplitSettings_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
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
						SetDirty();
					}
				}

				grid_SplitSettings.Invalidate();
			}
		}

		private void SetDirty()
		{
			wasChanged = true;
		}

		#region Button Events
		#region Left bar
		private void B_MoveUp_Click(object sender, EventArgs e)
		{
			List<DataGridViewCell> selectedCells = grid_SplitSettings.SelectedCells.Cast<DataGridViewCell>().OrderBy(o => o.RowIndex).ToList();

			var selectedInd = selectedCells.First().RowIndex;
			bool currCell = false;

			if (selectedCells != null)
			{
				foreach (DataGridViewCell selectedCell in selectedCells)
				{
					selectedInd = selectedCell.RowIndex;
					if (selectedInd > 0)
					{
						SwitchElements(selectedInd - 1);

						if (!currCell)
						{
							grid_SplitSettings.CurrentCell = grid_SplitSettings.Rows[selectedInd - 1].Cells[grid_SplitSettings.CurrentCell.ColumnIndex];
							currCell = true;
						}

						grid_SplitSettings.Rows[selectedInd - 1].Cells[grid_SplitSettings.CurrentCell.ColumnIndex].Selected = true;
					}
				}
			}
			SetDirty();
			grid_SplitSettings.Invalidate();
		}

		private void SwitchElements(int elementIndex)
		{
			var tmpNormalList = splitToEventList.ToList();
			var firstSegment = tmpNormalList.ElementAt(elementIndex);
			var firstName = firstSegment.SegmentName;
			var secondSegment = tmpNormalList.ElementAt(elementIndex + 1);
			var secondName = secondSegment.SegmentName;


			tmpNormalList.RemoveAt(elementIndex + 1);
			tmpNormalList.Insert(elementIndex, secondSegment);
			tmpNormalList[elementIndex].SegmentName = firstName;
			tmpNormalList[elementIndex + 1].SegmentName = secondName;
			splitToEventList = new BindingList<ISplitEvent>(tmpNormalList);

		}

		private void B_MoveDown_Click(object sender, EventArgs e)
		{
			List<DataGridViewCell> selectedCells = grid_SplitSettings.SelectedCells.Cast<DataGridViewCell>().OrderByDescending(o => o.RowIndex).ToList();

			var selectedInd = selectedCells.First().RowIndex;
			bool currCell = false;

			if (selectedCells != null)
			{
				SwitchElements(selectedInd);

				foreach (DataGridViewCell selectedCell in selectedCells)
				{
					selectedInd = selectedCell.RowIndex;
					if (selectedInd < splitToEventList.Count - 1)
					{
						if (!currCell)
						{
							grid_SplitSettings.CurrentCell = grid_SplitSettings.Rows[selectedInd + 1].Cells[grid_SplitSettings.CurrentCell.ColumnIndex];
							currCell = true;
						}

						grid_SplitSettings.Rows[selectedInd + 1].Cells[grid_SplitSettings.CurrentCell.ColumnIndex].Selected = true;
					}
				}
			}
			SetDirty();
			grid_SplitSettings.Invalidate();
		}
		#endregion

		#region Bottom bar
		private void B_Verify_Click(object sender, EventArgs e)
		{

			splitToEvents.EventList = splitToEventList.Cast<SplitsToEvents.SplitEvent>().ToList();
			var result = splitToEvents.Verify(splitStates);
			if (result != "")
				MessageBox.Show("Following issues were found:\n" + result, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				MessageBox.Show("Everything seems fine", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void B_Save_Click(object sender, EventArgs e)
		{
			splitToEvents.EventList = splitToEventList.Cast<SplitsToEvents.SplitEvent>().ToList();
			var result = splitToEvents.Verify(splitStates);
			if (result != "")
			{
				MessageBox.Show("Following issues were found:\n" + result, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			splitToEvents.EventList = splitToEventList.Cast<SplitsToEvents.SplitEvent>().ToList();
			try
			{
				splitToEvents.Save();
				MessageBox.Show("Saved successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Failed to export to XML:\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			if (wasChanged && MessageBox.Show("Are you sure you want to close it and abandon changes?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			}
			else
			{
				this.DialogResult = DialogResult.Cancel;
				this.Close();
			}
		}
		#endregion
		#endregion

		private void CB_UsePBPrediction_CheckedChanged(object sender, EventArgs e)
		{
			CBox_RunCompletion.Enabled = !CB_UsePBPrediction.Checked;
			SetDirty();
		}

		private void CB_NotifyOfErrorsInChat_CheckedChanged(object sender, EventArgs e)
		{
			SetDirty();
		}

		private void CBox_OnRunReset_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetDirty();
		}

		private void CBox_RunCompletion_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetDirty();
		}
	}
}

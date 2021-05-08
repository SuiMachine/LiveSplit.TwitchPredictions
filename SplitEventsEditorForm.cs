using LiveSplit.Model;
using LiveSplit.TimeFormatters;
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
			var verficationResult = splitToEvents.Verify(splitStates);
			if (verficationResult != "")
			{
				//MessageBox.Show("Verification found following issues:\n" + verficationResult, "Notification", MessageBoxButtons.OK);
			}

			#region Setting up grid
			TimeFormatter = new ShortTimeFormatter();
			splitToEventList = new BindingList<ISplitEvent>(splitToEvents.EventList) { AllowNew = false, AllowRemove = false };
			grid_SplitSettings.AutoGenerateColumns = false;
			grid_SplitSettings.AutoSize = true;
			grid_SplitSettings.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
			grid_SplitSettings.DataSource = splitToEventList;
			grid_SplitSettings.CellDoubleClick += Grid_SplitSettings_CellDoubleClick;
			grid_SplitSettings.CellFormatting += Grid_SplitSettings_CellFormatting;
			grid_SplitSettings.CellParsing += Grid_SplitSettings_CellParsing;
			grid_SplitSettings.CellValidating += Grid_SplitSettings_CellValidating;
			grid_SplitSettings.CellEndEdit += Grid_SplitSettings_CellEndEdit;
			grid_SplitSettings.SelectionChanged += Grid_SplitSettings_SelectionChanged;

			var segmentNameColumn = new DataGridViewTextBoxColumn();
			segmentNameColumn.Name = "Segment Name";
			segmentNameColumn.Width = 100;
			segmentNameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
			segmentNameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
			grid_SplitSettings.Columns.Add(segmentNameColumn);

			var eventTypeColumn = new DataGridViewComboBoxColumn();

			//Hide your children
			eventTypeColumn.Items.AddRange(Enum.GetValues(typeof(SplitEventType)).Cast<Enum>().Select(value => new
			{
				(Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute))	as DescriptionAttribute).Description
			}.Description).ToArray());
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

			grid_SplitSettings.EditingControlShowing += Grid_SplitSettings_EditingControlShowing;
			#endregion

			AddComboboxDataSources();
			CBox_OnRunReset.DataBindings.Add("SelectedValue", splitToEvents, "OnTimerResetBehaviour", false, DataSourceUpdateMode.OnPropertyChanged);
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
		}
		#endregion


		private void Grid_SplitSettings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < splitToEventList.Count)
			{
				if (e.ColumnIndex == COLUMNINDEX_DELAY)
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
				else if (e.ColumnIndex == COLUMNINDEX_EVENT)
				{
					e.Value = splitToEventList[e.RowIndex].EventType.ToString();
				}
				else if (e.ColumnIndex == COLUMNINDEX_SEGMENTNAME)
				{
					e.Value = splitToEventList[e.RowIndex].SegmentName;
				}
				else if(e.ColumnIndex == COLUMNINDEX_ACTION)
				{
					bool eventUsed = splitToEventList[e.RowIndex].Action.isUsed;
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
				}

				return new ParsingResults(true, value);
			}

			try
			{
				value = TimeSpanParser.Parse(value.ToString());
				if (columnIndex == COLUMNINDEX_DELAY)
				{
					splitToEventList[rowIndex].Delay = (TimeSpan)value;
				}

				return new ParsingResults(true, value);
			}
			catch { }

			return new ParsingResults(false, null);
		}

		private void Grid_SplitSettings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{

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
	}
	}

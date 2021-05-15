using System;
using System.Windows.Forms;
using static LiveSplit.TwitchPredictions.SplitsToEvents;

namespace LiveSplit.TwitchPredictions.Forms
{
	public partial class ActionEditor : Form
	{
		public SplitAction ReturnedAction { get; internal set; }

		public ActionEditor(SplitAction action)
		{
			ReturnedAction = (SplitAction)action.Clone();
			DialogResult = DialogResult.Cancel;
			InitializeComponent();

			//Bindings for whatever reason cause "right to left"...
			RB_Prediction_Name.Text = ReturnedAction.Header;
			RB_Option1.Text = ReturnedAction.Answer1;
			RB_Option2.Text = ReturnedAction.Answer2;
			Num_Lenght.Value = ReturnedAction.Lenght;

			L_PreditionLenghtLimit.Text = $"{RB_Prediction_Name.TextLength}/{RB_Prediction_Name.MaxLength}";
			L_CharactersUsedOutcome1.Text = $"{RB_Option1.TextLength}/{RB_Option1.MaxLength}";
			L_CharactersUsedOutcome2.Text = $"{RB_Option2.TextLength}/{RB_Option2.MaxLength}";
		}

		private void RB_Prediction_Name_TextChanged(object sender, EventArgs e)
		{
			L_PreditionLenghtLimit.Text = $"{RB_Prediction_Name.TextLength}/{RB_Prediction_Name.MaxLength}";
		}

		private void RB_Option1_TextChanged(object sender, EventArgs e)
		{
			L_CharactersUsedOutcome1.Text = $"{RB_Option1.TextLength}/{RB_Option1.MaxLength}";
		}

		private void RB_Option2_TextChanged(object sender, EventArgs e)
		{
			L_CharactersUsedOutcome2.Text = $"{RB_Option2.TextLength}/{RB_Option2.MaxLength}";
		}

		private void B_Ok_Click(object sender, EventArgs e)
		{
			ReturnedAction.Header = RB_Prediction_Name.Text.Trim();
			ReturnedAction.Answer1 = RB_Option1.Text.Trim();
			ReturnedAction.Answer2 = RB_Option2.Text.Trim();
			ReturnedAction.Lenght = (uint)Num_Lenght.Value;

			if (ReturnedAction.Header == "" || ReturnedAction.Answer1 == "" || ReturnedAction.Answer2 == "")
				MessageBox.Show("One of the fields is empty! Please fill in all of the necessery fields!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else if (ReturnedAction.Answer1 == ReturnedAction.Answer2)
				MessageBox.Show("Outcome 1 and 2 are the same!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
			{
				ReturnedAction.isUsed = true;
				DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void B_Cancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}

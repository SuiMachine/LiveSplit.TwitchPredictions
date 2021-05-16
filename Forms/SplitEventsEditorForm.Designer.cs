
namespace LiveSplit.TwitchPredictions
{
	partial class SplitEventsEditorForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.CBox_OnRunReset = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.CB_UsePBPrediction = new System.Windows.Forms.CheckBox();
			this.CBox_RunCompletion = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.B_Verify = new System.Windows.Forms.Button();
			this.B_Cancel = new System.Windows.Forms.Button();
			this.B_Save = new System.Windows.Forms.Button();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.grid_SplitSettings = new System.Windows.Forms.DataGridView();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.B_MoveDown = new System.Windows.Forms.Button();
			this.B_MoveUp = new System.Windows.Forms.Button();
			this.B_ImportEvents = new System.Windows.Forms.Button();
			this.B_ExportEvents = new System.Windows.Forms.Button();
			this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
			this.CB_NotifyOfErrorsInChat = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grid_SplitSettings)).BeginInit();
			this.tableLayoutPanel4.SuspendLayout();
			this.tableLayoutPanel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// CBox_OnRunReset
			// 
			this.CBox_OnRunReset.FormattingEnabled = true;
			this.CBox_OnRunReset.Location = new System.Drawing.Point(77, 3);
			this.CBox_OnRunReset.Name = "CBox_OnRunReset";
			this.CBox_OnRunReset.Size = new System.Drawing.Size(190, 21);
			this.CBox_OnRunReset.TabIndex = 3;
			this.CBox_OnRunReset.SelectedIndexChanged += new System.EventHandler(this.CBox_OnRunReset_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(68, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "On run reset:";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(944, 641);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.CB_UsePBPrediction);
			this.panel1.Controls.Add(this.CBox_RunCompletion);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.CBox_OnRunReset);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(938, 28);
			this.panel1.TabIndex = 2;
			// 
			// CB_UsePBPrediction
			// 
			this.CB_UsePBPrediction.AutoSize = true;
			this.CB_UsePBPrediction.Location = new System.Drawing.Point(553, 5);
			this.CB_UsePBPrediction.Name = "CB_UsePBPrediction";
			this.CB_UsePBPrediction.Size = new System.Drawing.Size(148, 17);
			this.CB_UsePBPrediction.TabIndex = 8;
			this.CB_UsePBPrediction.Text = "Finish with option 1 on PB";
			this.CB_UsePBPrediction.UseVisualStyleBackColor = true;
			this.CB_UsePBPrediction.CheckedChanged += new System.EventHandler(this.CB_UsePBPrediction_CheckedChanged);
			// 
			// CBox_RunCompletion
			// 
			this.CBox_RunCompletion.FormattingEnabled = true;
			this.CBox_RunCompletion.Location = new System.Drawing.Point(357, 3);
			this.CBox_RunCompletion.Name = "CBox_RunCompletion";
			this.CBox_RunCompletion.Size = new System.Drawing.Size(190, 21);
			this.CBox_RunCompletion.TabIndex = 7;
			this.CBox_RunCompletion.SelectedIndexChanged += new System.EventHandler(this.CBox_RunCompletion_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(273, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(78, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "On completion:";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.B_Verify, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.B_Cancel, 3, 0);
			this.tableLayoutPanel2.Controls.Add(this.B_Save, 2, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 604);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(938, 34);
			this.tableLayoutPanel2.TabIndex = 3;
			// 
			// B_Verify
			// 
			this.B_Verify.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Verify.Location = new System.Drawing.Point(3, 3);
			this.B_Verify.Name = "B_Verify";
			this.B_Verify.Size = new System.Drawing.Size(101, 28);
			this.B_Verify.TabIndex = 2;
			this.B_Verify.Text = "Verify";
			this.B_Verify.UseVisualStyleBackColor = true;
			this.B_Verify.Click += new System.EventHandler(this.B_Verify_Click);
			// 
			// B_Cancel
			// 
			this.B_Cancel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Cancel.Location = new System.Drawing.Point(833, 3);
			this.B_Cancel.Name = "B_Cancel";
			this.B_Cancel.Size = new System.Drawing.Size(102, 28);
			this.B_Cancel.TabIndex = 1;
			this.B_Cancel.Text = "Cancel";
			this.B_Cancel.UseVisualStyleBackColor = true;
			this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
			// 
			// B_Save
			// 
			this.B_Save.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_Save.Location = new System.Drawing.Point(726, 3);
			this.B_Save.Name = "B_Save";
			this.B_Save.Size = new System.Drawing.Size(101, 28);
			this.B_Save.TabIndex = 0;
			this.B_Save.Text = "Save";
			this.B_Save.UseVisualStyleBackColor = true;
			this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.grid_SplitSettings, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 37);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(938, 561);
			this.tableLayoutPanel3.TabIndex = 4;
			// 
			// grid_SplitSettings
			// 
			this.grid_SplitSettings.AllowUserToAddRows = false;
			this.grid_SplitSettings.AllowUserToDeleteRows = false;
			this.grid_SplitSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid_SplitSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid_SplitSettings.Location = new System.Drawing.Point(178, 3);
			this.grid_SplitSettings.Name = "grid_SplitSettings";
			this.grid_SplitSettings.Size = new System.Drawing.Size(757, 555);
			this.grid_SplitSettings.TabIndex = 2;
			this.grid_SplitSettings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_SplitSettings_KeyDown);
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 1;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.B_MoveDown, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.B_MoveUp, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.B_ImportEvents, 0, 2);
			this.tableLayoutPanel4.Controls.Add(this.B_ExportEvents, 0, 3);
			this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 4);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 5;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(169, 252);
			this.tableLayoutPanel4.TabIndex = 3;
			// 
			// B_MoveDown
			// 
			this.B_MoveDown.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_MoveDown.Location = new System.Drawing.Point(3, 37);
			this.B_MoveDown.Name = "B_MoveDown";
			this.B_MoveDown.Size = new System.Drawing.Size(163, 28);
			this.B_MoveDown.TabIndex = 4;
			this.B_MoveDown.Text = "Move Events To Next Split";
			this.B_MoveDown.UseVisualStyleBackColor = true;
			this.B_MoveDown.Click += new System.EventHandler(this.B_MoveDown_Click);
			// 
			// B_MoveUp
			// 
			this.B_MoveUp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_MoveUp.Location = new System.Drawing.Point(3, 3);
			this.B_MoveUp.Name = "B_MoveUp";
			this.B_MoveUp.Size = new System.Drawing.Size(163, 28);
			this.B_MoveUp.TabIndex = 3;
			this.B_MoveUp.Text = "Move Events to Previous Split";
			this.B_MoveUp.UseVisualStyleBackColor = true;
			this.B_MoveUp.Click += new System.EventHandler(this.B_MoveUp_Click);
			// 
			// B_ImportEvents
			// 
			this.B_ImportEvents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_ImportEvents.Enabled = false;
			this.B_ImportEvents.Location = new System.Drawing.Point(3, 71);
			this.B_ImportEvents.Name = "B_ImportEvents";
			this.B_ImportEvents.Size = new System.Drawing.Size(163, 28);
			this.B_ImportEvents.TabIndex = 6;
			this.B_ImportEvents.Text = "Import events";
			this.B_ImportEvents.UseVisualStyleBackColor = true;
			// 
			// B_ExportEvents
			// 
			this.B_ExportEvents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.B_ExportEvents.Enabled = false;
			this.B_ExportEvents.Location = new System.Drawing.Point(3, 105);
			this.B_ExportEvents.Name = "B_ExportEvents";
			this.B_ExportEvents.Size = new System.Drawing.Size(163, 28);
			this.B_ExportEvents.TabIndex = 5;
			this.B_ExportEvents.Text = "Export events";
			this.B_ExportEvents.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel5
			// 
			this.tableLayoutPanel5.ColumnCount = 1;
			this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel5.Controls.Add(this.CB_NotifyOfErrorsInChat, 0, 0);
			this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 139);
			this.tableLayoutPanel5.Name = "tableLayoutPanel5";
			this.tableLayoutPanel5.RowCount = 2;
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel5.Size = new System.Drawing.Size(163, 110);
			this.tableLayoutPanel5.TabIndex = 7;
			// 
			// CB_NotifyOfErrorsInChat
			// 
			this.CB_NotifyOfErrorsInChat.AutoSize = true;
			this.CB_NotifyOfErrorsInChat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CB_NotifyOfErrorsInChat.Location = new System.Drawing.Point(3, 3);
			this.CB_NotifyOfErrorsInChat.Name = "CB_NotifyOfErrorsInChat";
			this.CB_NotifyOfErrorsInChat.Size = new System.Drawing.Size(157, 49);
			this.CB_NotifyOfErrorsInChat.TabIndex = 0;
			this.CB_NotifyOfErrorsInChat.Text = "Notify of errors in chat";
			this.CB_NotifyOfErrorsInChat.UseVisualStyleBackColor = true;
			this.CB_NotifyOfErrorsInChat.CheckedChanged += new System.EventHandler(this.CB_NotifyOfErrorsInChat_CheckedChanged);
			// 
			// SplitEventsEditorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(944, 641);
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(960, 680);
			this.Name = "SplitEventsEditorForm";
			this.Text = "Split Events Editor";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grid_SplitSettings)).EndInit();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel5.ResumeLayout(false);
			this.tableLayoutPanel5.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ComboBox CBox_OnRunReset;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button B_Cancel;
		private System.Windows.Forms.Button B_Save;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		public System.Windows.Forms.DataGridView grid_SplitSettings;
		private System.Windows.Forms.Button B_Verify;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.Button B_MoveUp;
		private System.Windows.Forms.Button B_MoveDown;
		private System.Windows.Forms.Button B_ExportEvents;
		private System.Windows.Forms.Button B_ImportEvents;
		private System.Windows.Forms.CheckBox CB_UsePBPrediction;
		private System.Windows.Forms.ComboBox CBox_RunCompletion;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
		private System.Windows.Forms.CheckBox CB_NotifyOfErrorsInChat;
	}
}
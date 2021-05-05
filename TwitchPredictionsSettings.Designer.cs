
namespace LiveSplit.TwitchPredictions
{
	partial class TwitchPredictionsSettings
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.TB_Channel = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.CB_ConnectOnComponentLaunch = new System.Windows.Forms.CheckBox();
			this.button2 = new System.Windows.Forms.Button();
			this.B_GenerateAouth = new System.Windows.Forms.Button();
			this.TB_Oauth = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.TB_UserName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.NumB_Port = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.TB_ServerAdress = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.grid_SplitSettings = new System.Windows.Forms.DataGridView();
			this.SegmentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Action = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumB_Port)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grid_SplitSettings)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.TB_Channel);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.CB_ConnectOnComponentLaunch);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.B_GenerateAouth);
			this.groupBox1.Controls.Add(this.TB_Oauth);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.TB_UserName);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.NumB_Port);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.TB_ServerAdress);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(455, 134);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection settings";
			// 
			// TB_Channel
			// 
			this.TB_Channel.Location = new System.Drawing.Point(286, 45);
			this.TB_Channel.Name = "TB_Channel";
			this.TB_Channel.Size = new System.Drawing.Size(155, 20);
			this.TB_Channel.TabIndex = 13;
			this.TB_Channel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(231, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(49, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Channel:";
			// 
			// CB_ConnectOnComponentLaunch
			// 
			this.CB_ConnectOnComponentLaunch.Location = new System.Drawing.Point(184, 95);
			this.CB_ConnectOnComponentLaunch.Name = "CB_ConnectOnComponentLaunch";
			this.CB_ConnectOnComponentLaunch.Size = new System.Drawing.Size(126, 30);
			this.CB_ConnectOnComponentLaunch.TabIndex = 11;
			this.CB_ConnectOnComponentLaunch.Text = "Connect on component launch";
			this.CB_ConnectOnComponentLaunch.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(345, 97);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(96, 23);
			this.button2.TabIndex = 10;
			this.button2.Text = "Connect";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// B_GenerateAouth
			// 
			this.B_GenerateAouth.Location = new System.Drawing.Point(6, 98);
			this.B_GenerateAouth.Name = "B_GenerateAouth";
			this.B_GenerateAouth.Size = new System.Drawing.Size(96, 23);
			this.B_GenerateAouth.TabIndex = 8;
			this.B_GenerateAouth.Text = "Generate OAuth";
			this.B_GenerateAouth.UseVisualStyleBackColor = true;
			// 
			// TB_Oauth
			// 
			this.TB_Oauth.Location = new System.Drawing.Point(70, 71);
			this.TB_Oauth.Name = "TB_Oauth";
			this.TB_Oauth.PasswordChar = '*';
			this.TB_Oauth.Size = new System.Drawing.Size(371, 20);
			this.TB_Oauth.TabIndex = 7;
			this.TB_Oauth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 74);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 13);
			this.label4.TabIndex = 6;
			this.label4.Text = "Oauth:";
			// 
			// TB_UserName
			// 
			this.TB_UserName.Location = new System.Drawing.Point(70, 45);
			this.TB_UserName.Name = "TB_UserName";
			this.TB_UserName.Size = new System.Drawing.Size(155, 20);
			this.TB_UserName.TabIndex = 5;
			this.TB_UserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Username:";
			// 
			// NumB_Port
			// 
			this.NumB_Port.Location = new System.Drawing.Point(358, 19);
			this.NumB_Port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.NumB_Port.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
			this.NumB_Port.Name = "NumB_Port";
			this.NumB_Port.Size = new System.Drawing.Size(83, 20);
			this.NumB_Port.TabIndex = 3;
			this.NumB_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.NumB_Port.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(323, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Port:";
			// 
			// TB_ServerAdress
			// 
			this.TB_ServerAdress.Location = new System.Drawing.Point(70, 19);
			this.TB_ServerAdress.Name = "TB_ServerAdress";
			this.TB_ServerAdress.Size = new System.Drawing.Size(209, 20);
			this.TB_ServerAdress.TabIndex = 1;
			this.TB_ServerAdress.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Server:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.tableLayoutPanel1);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 134);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(455, 403);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Prediction settings";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.grid_SplitSettings, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(449, 384);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// grid_SplitSettings
			// 
			this.grid_SplitSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grid_SplitSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SegmentName,
            this.Action});
			this.grid_SplitSettings.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grid_SplitSettings.Location = new System.Drawing.Point(3, 72);
			this.grid_SplitSettings.Name = "grid_SplitSettings";
			this.grid_SplitSettings.Size = new System.Drawing.Size(443, 309);
			this.grid_SplitSettings.TabIndex = 0;
			// 
			// SegmentName
			// 
			this.SegmentName.HeaderText = "Segment name";
			this.SegmentName.Name = "SegmentName";
			this.SegmentName.ReadOnly = true;
			// 
			// Action
			// 
			this.Action.HeaderText = "Action";
			this.Action.Name = "Action";
			// 
			// TwitchPredictionsSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "TwitchPredictionsSettings";
			this.Size = new System.Drawing.Size(455, 537);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumB_Port)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grid_SplitSettings)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown NumB_Port;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox TB_ServerAdress;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TB_UserName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TB_Oauth;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckBox CB_ConnectOnComponentLaunch;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button B_GenerateAouth;
		private System.Windows.Forms.TextBox TB_Channel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.DataGridView grid_SplitSettings;
		private System.Windows.Forms.DataGridViewTextBoxColumn SegmentName;
		private System.Windows.Forms.DataGridViewComboBoxColumn Action;
	}
}

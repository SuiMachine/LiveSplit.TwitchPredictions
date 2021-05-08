
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
			this.B_SaveSettings = new System.Windows.Forms.Button();
			this.TB_Channel = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.CB_ConnectOnComponentLaunch = new System.Windows.Forms.CheckBox();
			this.B_Connect = new System.Windows.Forms.Button();
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
			this.B_EditSplitEvents = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumB_Port)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.B_SaveSettings);
			this.groupBox1.Controls.Add(this.TB_Channel);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.CB_ConnectOnComponentLaunch);
			this.groupBox1.Controls.Add(this.B_Connect);
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
			this.groupBox1.Size = new System.Drawing.Size(455, 161);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Connection settings";
			// 
			// B_SaveSettings
			// 
			this.B_SaveSettings.Location = new System.Drawing.Point(6, 127);
			this.B_SaveSettings.Name = "B_SaveSettings";
			this.B_SaveSettings.Size = new System.Drawing.Size(96, 23);
			this.B_SaveSettings.TabIndex = 14;
			this.B_SaveSettings.Text = "Save";
			this.B_SaveSettings.UseVisualStyleBackColor = true;
			this.B_SaveSettings.Click += new System.EventHandler(this.B_SaveSettings_Click);
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
			this.CB_ConnectOnComponentLaunch.Location = new System.Drawing.Point(315, 95);
			this.CB_ConnectOnComponentLaunch.Name = "CB_ConnectOnComponentLaunch";
			this.CB_ConnectOnComponentLaunch.Size = new System.Drawing.Size(126, 30);
			this.CB_ConnectOnComponentLaunch.TabIndex = 11;
			this.CB_ConnectOnComponentLaunch.Text = "Connect on component launch";
			this.CB_ConnectOnComponentLaunch.UseVisualStyleBackColor = true;
			// 
			// B_Connect
			// 
			this.B_Connect.Location = new System.Drawing.Point(345, 131);
			this.B_Connect.Name = "B_Connect";
			this.B_Connect.Size = new System.Drawing.Size(96, 23);
			this.B_Connect.TabIndex = 10;
			this.B_Connect.Text = "Connect";
			this.B_Connect.UseVisualStyleBackColor = true;
			// 
			// B_GenerateAouth
			// 
			this.B_GenerateAouth.Location = new System.Drawing.Point(6, 98);
			this.B_GenerateAouth.Name = "B_GenerateAouth";
			this.B_GenerateAouth.Size = new System.Drawing.Size(96, 23);
			this.B_GenerateAouth.TabIndex = 8;
			this.B_GenerateAouth.Text = "Generate OAuth";
			this.B_GenerateAouth.UseVisualStyleBackColor = true;
			this.B_GenerateAouth.Click += new System.EventHandler(this.B_GenerateAouth_Click);
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
			this.groupBox2.Controls.Add(this.B_EditSplitEvents);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 161);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(455, 64);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Prediction settings";
			// 
			// B_EditSplitEvents
			// 
			this.B_EditSplitEvents.Location = new System.Drawing.Point(9, 19);
			this.B_EditSplitEvents.Name = "B_EditSplitEvents";
			this.B_EditSplitEvents.Size = new System.Drawing.Size(104, 23);
			this.B_EditSplitEvents.TabIndex = 0;
			this.B_EditSplitEvents.Text = "Edit split events";
			this.B_EditSplitEvents.UseVisualStyleBackColor = true;
			this.B_EditSplitEvents.Click += new System.EventHandler(this.B_EditSplitEvents_Click);
			// 
			// TwitchPredictionsSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "TwitchPredictionsSettings";
			this.Size = new System.Drawing.Size(455, 225);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumB_Port)).EndInit();
			this.groupBox2.ResumeLayout(false);
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
		private System.Windows.Forms.Button B_Connect;
		private System.Windows.Forms.Button B_GenerateAouth;
		private System.Windows.Forms.TextBox TB_Channel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button B_SaveSettings;
		private System.Windows.Forms.Button B_EditSplitEvents;
	}
}

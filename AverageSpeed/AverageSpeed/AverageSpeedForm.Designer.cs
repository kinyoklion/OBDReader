namespace AverageSpeed.AverageSpeed
{
    partial class AverageSpeedForm
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
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CommandBox = new System.Windows.Forms.ListBox();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.CalculatedDistanceLabel = new System.Windows.Forms.Label();
            this.AverageCalcMLabel = new System.Windows.Forms.Label();
            this.ReadyButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.LoggingEnabledCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.SetupButton = new System.Windows.Forms.Button();
            this.MilePostListView = new System.Windows.Forms.ListView();
            this.PostListPostName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PostListPostDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PostListPostMile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PostListPostTarget = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PostListPostDeviation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PostListPostOdometer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PostListPostSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RemainingTimeLabel = new System.Windows.Forms.Label();
            this.RaceEnabledCheckbox = new System.Windows.Forms.CheckBox();
            this.LogSessionCheckbox = new System.Windows.Forms.CheckBox();
            this.StartButton = new System.Windows.Forms.Button();
            this.PeakSpeedLabel = new System.Windows.Forms.Label();
            this.TargetSpeedLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectButton.Location = new System.Drawing.Point(937, 492);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 69);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Connect (C)";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.OnConnectButtonClick);
            // 
            // CommandBox
            // 
            this.CommandBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CommandBox.FormattingEnabled = true;
            this.CommandBox.Location = new System.Drawing.Point(12, 492);
            this.CommandBox.Name = "CommandBox";
            this.CommandBox.Size = new System.Drawing.Size(232, 95);
            this.CommandBox.TabIndex = 2;
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.Location = new System.Drawing.Point(937, 567);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(75, 23);
            this.DisconnectButton.TabIndex = 3;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.OnDisconnectButtonClick);
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.Location = new System.Drawing.Point(12, 9);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(215, 39);
            this.TimeLabel.TabIndex = 6;
            this.TimeLabel.Text = "00:00:00:000";
            // 
            // CalculatedDistanceLabel
            // 
            this.CalculatedDistanceLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CalculatedDistanceLabel.AutoSize = true;
            this.CalculatedDistanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CalculatedDistanceLabel.Location = new System.Drawing.Point(900, 9);
            this.CalculatedDistanceLabel.MinimumSize = new System.Drawing.Size(112, 39);
            this.CalculatedDistanceLabel.Name = "CalculatedDistanceLabel";
            this.CalculatedDistanceLabel.Size = new System.Drawing.Size(112, 39);
            this.CalculatedDistanceLabel.TabIndex = 8;
            this.CalculatedDistanceLabel.Text = "0.00";
            this.CalculatedDistanceLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // AverageCalcMLabel
            // 
            this.AverageCalcMLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AverageCalcMLabel.AutoSize = true;
            this.AverageCalcMLabel.BackColor = System.Drawing.SystemColors.Control;
            this.AverageCalcMLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AverageCalcMLabel.Location = new System.Drawing.Point(375, -9);
            this.AverageCalcMLabel.MinimumSize = new System.Drawing.Size(274, 76);
            this.AverageCalcMLabel.Name = "AverageCalcMLabel";
            this.AverageCalcMLabel.Size = new System.Drawing.Size(274, 76);
            this.AverageCalcMLabel.TabIndex = 10;
            this.AverageCalcMLabel.Text = "0.00";
            this.AverageCalcMLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReadyButton
            // 
            this.ReadyButton.BackColor = System.Drawing.SystemColors.Control;
            this.ReadyButton.Enabled = false;
            this.ReadyButton.Location = new System.Drawing.Point(856, 492);
            this.ReadyButton.Name = "ReadyButton";
            this.ReadyButton.Size = new System.Drawing.Size(75, 69);
            this.ReadyButton.TabIndex = 11;
            this.ReadyButton.Text = "Ready (R)";
            this.ReadyButton.UseVisualStyleBackColor = true;
            this.ReadyButton.Click += new System.EventHandler(this.OnReadyButtonClick);
            // 
            // StopButton
            // 
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(856, 567);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 12;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.OnStopButtonClicked);
            // 
            // LoggingEnabledCheckBox
            // 
            this.LoggingEnabledCheckBox.AutoSize = true;
            this.LoggingEnabledCheckBox.Location = new System.Drawing.Point(250, 515);
            this.LoggingEnabledCheckBox.Name = "LoggingEnabledCheckBox";
            this.LoggingEnabledCheckBox.Size = new System.Drawing.Size(108, 17);
            this.LoggingEnabledCheckBox.TabIndex = 13;
            this.LoggingEnabledCheckBox.Text = "Show Commands";
            this.LoggingEnabledCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(250, 564);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "Clear Log";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnClearLog);
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedLabel.AutoSize = true;
            this.SpeedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpeedLabel.Location = new System.Drawing.Point(375, 511);
            this.SpeedLabel.MinimumSize = new System.Drawing.Size(274, 76);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(274, 76);
            this.SpeedLabel.TabIndex = 15;
            this.SpeedLabel.Text = "0.00";
            this.SpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SetupButton
            // 
            this.SetupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SetupButton.Location = new System.Drawing.Point(775, 567);
            this.SetupButton.Name = "SetupButton";
            this.SetupButton.Size = new System.Drawing.Size(75, 23);
            this.SetupButton.TabIndex = 16;
            this.SetupButton.Text = "Setup";
            this.SetupButton.UseVisualStyleBackColor = true;
            this.SetupButton.Click += new System.EventHandler(this.OnSetupButtonClicked);
            // 
            // MilePostListView
            // 
            this.MilePostListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PostListPostName,
            this.PostListPostDescription,
            this.PostListPostMile,
            this.PostListPostTarget,
            this.PostListPostDeviation,
            this.PostListPostOdometer,
            this.PostListPostSpeed});
            this.MilePostListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MilePostListView.FullRowSelect = true;
            this.MilePostListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.MilePostListView.Location = new System.Drawing.Point(12, 88);
            this.MilePostListView.MultiSelect = false;
            this.MilePostListView.Name = "MilePostListView";
            this.MilePostListView.Size = new System.Drawing.Size(1000, 398);
            this.MilePostListView.TabIndex = 18;
            this.MilePostListView.UseCompatibleStateImageBehavior = false;
            this.MilePostListView.View = System.Windows.Forms.View.Details;
            this.MilePostListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.OnMilePostListViewColumnWidthChanging);
            this.MilePostListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnMilePostListKeyPress);
            // 
            // PostListPostName
            // 
            this.PostListPostName.Text = "Name";
            this.PostListPostName.Width = 118;
            // 
            // PostListPostDescription
            // 
            this.PostListPostDescription.Text = "Description";
            this.PostListPostDescription.Width = 418;
            // 
            // PostListPostMile
            // 
            this.PostListPostMile.Text = "Mile";
            // 
            // PostListPostTarget
            // 
            this.PostListPostTarget.Text = "Target";
            this.PostListPostTarget.Width = 130;
            // 
            // PostListPostDeviation
            // 
            this.PostListPostDeviation.Text = "Deviation";
            this.PostListPostDeviation.Width = 130;
            // 
            // PostListPostOdometer
            // 
            this.PostListPostOdometer.DisplayIndex = 6;
            this.PostListPostOdometer.Text = "Odo";
            // 
            // PostListPostSpeed
            // 
            this.PostListPostSpeed.DisplayIndex = 5;
            this.PostListPostSpeed.Text = "Speed";
            // 
            // RemainingTimeLabel
            // 
            this.RemainingTimeLabel.AutoSize = true;
            this.RemainingTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemainingTimeLabel.Location = new System.Drawing.Point(12, 46);
            this.RemainingTimeLabel.Name = "RemainingTimeLabel";
            this.RemainingTimeLabel.Size = new System.Drawing.Size(215, 39);
            this.RemainingTimeLabel.TabIndex = 19;
            this.RemainingTimeLabel.Text = "00:00:00:000";
            // 
            // RaceEnabledCheckbox
            // 
            this.RaceEnabledCheckbox.AutoSize = true;
            this.RaceEnabledCheckbox.Location = new System.Drawing.Point(250, 492);
            this.RaceEnabledCheckbox.Name = "RaceEnabledCheckbox";
            this.RaceEnabledCheckbox.Size = new System.Drawing.Size(52, 17);
            this.RaceEnabledCheckbox.TabIndex = 20;
            this.RaceEnabledCheckbox.Text = "Race";
            this.RaceEnabledCheckbox.UseVisualStyleBackColor = true;
            this.RaceEnabledCheckbox.CheckedChanged += new System.EventHandler(this.OnRaceEnabledToggle);
            // 
            // LogSessionCheckbox
            // 
            this.LogSessionCheckbox.AutoSize = true;
            this.LogSessionCheckbox.Checked = true;
            this.LogSessionCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LogSessionCheckbox.Location = new System.Drawing.Point(250, 538);
            this.LogSessionCheckbox.Name = "LogSessionCheckbox";
            this.LogSessionCheckbox.Size = new System.Drawing.Size(84, 17);
            this.LogSessionCheckbox.TabIndex = 21;
            this.LogSessionCheckbox.Text = "Log Session";
            this.LogSessionCheckbox.UseVisualStyleBackColor = true;
            this.LogSessionCheckbox.CheckedChanged += new System.EventHandler(this.OnLogSessionCheckboxChanged);
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.SystemColors.Control;
            this.StartButton.Enabled = false;
            this.StartButton.Location = new System.Drawing.Point(775, 492);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 69);
            this.StartButton.TabIndex = 22;
            this.StartButton.Text = "Start (S)";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.OnStartButtonClick);
            // 
            // PeakSpeedLabel
            // 
            this.PeakSpeedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PeakSpeedLabel.AutoSize = true;
            this.PeakSpeedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.PeakSpeedLabel.Location = new System.Drawing.Point(893, 46);
            this.PeakSpeedLabel.MinimumSize = new System.Drawing.Size(119, 39);
            this.PeakSpeedLabel.Name = "PeakSpeedLabel";
            this.PeakSpeedLabel.Size = new System.Drawing.Size(119, 39);
            this.PeakSpeedLabel.TabIndex = 23;
            this.PeakSpeedLabel.Text = "0.00";
            this.PeakSpeedLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TargetSpeedLabel
            // 
            this.TargetSpeedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TargetSpeedLabel.AutoSize = true;
            this.TargetSpeedLabel.BackColor = System.Drawing.SystemColors.Control;
            this.TargetSpeedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TargetSpeedLabel.Location = new System.Drawing.Point(375, 57);
            this.TargetSpeedLabel.MinimumSize = new System.Drawing.Size(274, 20);
            this.TargetSpeedLabel.Name = "TargetSpeedLabel";
            this.TargetSpeedLabel.Size = new System.Drawing.Size(274, 31);
            this.TargetSpeedLabel.TabIndex = 24;
            this.TargetSpeedLabel.Text = "0.00";
            this.TargetSpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AverageSpeedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.TargetSpeedLabel);
            this.Controls.Add(this.PeakSpeedLabel);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.LogSessionCheckbox);
            this.Controls.Add(this.RaceEnabledCheckbox);
            this.Controls.Add(this.RemainingTimeLabel);
            this.Controls.Add(this.MilePostListView);
            this.Controls.Add(this.SetupButton);
            this.Controls.Add(this.SpeedLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LoggingEnabledCheckBox);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.ReadyButton);
            this.Controls.Add(this.AverageCalcMLabel);
            this.Controls.Add(this.CalculatedDistanceLabel);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.CommandBox);
            this.Controls.Add(this.ConnectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 600);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.Name = "AverageSpeedForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Open Road Challenge";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnAverageSpeedFormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnFormKeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.ListBox CommandBox;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label CalculatedDistanceLabel;
        private System.Windows.Forms.Label AverageCalcMLabel;
        private System.Windows.Forms.Button ReadyButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.CheckBox LoggingEnabledCheckBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.Button SetupButton;
        private System.Windows.Forms.ListView MilePostListView;
        private System.Windows.Forms.ColumnHeader PostListPostName;
        private System.Windows.Forms.ColumnHeader PostListPostDescription;
        private System.Windows.Forms.ColumnHeader PostListPostMile;
        private System.Windows.Forms.ColumnHeader PostListPostTarget;
        private System.Windows.Forms.ColumnHeader PostListPostDeviation;
        private System.Windows.Forms.ColumnHeader PostListPostSpeed;
        private System.Windows.Forms.Label RemainingTimeLabel;
        private System.Windows.Forms.CheckBox RaceEnabledCheckbox;
        private System.Windows.Forms.ColumnHeader PostListPostOdometer;
        private System.Windows.Forms.CheckBox LogSessionCheckbox;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label PeakSpeedLabel;
        private System.Windows.Forms.Label TargetSpeedLabel;
    }
}


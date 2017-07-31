namespace AverageSpeed.Settings
{
    using System.Windows.Forms;

    partial class SettingsForm
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.TargetTimeGroup = new System.Windows.Forms.GroupBox();
            this.MillisecondsLabel = new System.Windows.Forms.Label();
            this.SecondsLabel = new System.Windows.Forms.Label();
            this.MinutesLabel = new System.Windows.Forms.Label();
            this.HoursLabel = new System.Windows.Forms.Label();
            this.MillisecondsEntry = new System.Windows.Forms.NumericUpDown();
            this.SecondsEntry = new System.Windows.Forms.NumericUpDown();
            this.MinutesEntry = new System.Windows.Forms.NumericUpDown();
            this.HoursEntry = new System.Windows.Forms.NumericUpDown();
            this.TargetDistanceLabel = new System.Windows.Forms.Label();
            this.MilePostSettingsGrid = new System.Windows.Forms.DataGridView();
            this.PostName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostMile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComPortTextBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.TargetDistanceEntry = new System.Windows.Forms.NumericUpDown();
            this.TargetTimeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MillisecondsEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondsEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinutesEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HoursEntry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MilePostSettingsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetDistanceEntry)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(937, 565);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.OnCancelButtonPressed);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(856, 565);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.OnSaveButtonPressed);
            // 
            // TargetTimeGroup
            // 
            this.TargetTimeGroup.Controls.Add(this.MillisecondsLabel);
            this.TargetTimeGroup.Controls.Add(this.SecondsLabel);
            this.TargetTimeGroup.Controls.Add(this.MinutesLabel);
            this.TargetTimeGroup.Controls.Add(this.HoursLabel);
            this.TargetTimeGroup.Controls.Add(this.MillisecondsEntry);
            this.TargetTimeGroup.Controls.Add(this.SecondsEntry);
            this.TargetTimeGroup.Controls.Add(this.MinutesEntry);
            this.TargetTimeGroup.Controls.Add(this.HoursEntry);
            this.TargetTimeGroup.Location = new System.Drawing.Point(12, 12);
            this.TargetTimeGroup.Name = "TargetTimeGroup";
            this.TargetTimeGroup.Size = new System.Drawing.Size(237, 63);
            this.TargetTimeGroup.TabIndex = 4;
            this.TargetTimeGroup.TabStop = false;
            this.TargetTimeGroup.Text = "Target Time";
            // 
            // MillisecondsLabel
            // 
            this.MillisecondsLabel.AutoSize = true;
            this.MillisecondsLabel.Location = new System.Drawing.Point(165, 42);
            this.MillisecondsLabel.Name = "MillisecondsLabel";
            this.MillisecondsLabel.Size = new System.Drawing.Size(64, 13);
            this.MillisecondsLabel.TabIndex = 12;
            this.MillisecondsLabel.Text = "Milliseconds";
            // 
            // SecondsLabel
            // 
            this.SecondsLabel.AutoSize = true;
            this.SecondsLabel.Location = new System.Drawing.Point(111, 42);
            this.SecondsLabel.Name = "SecondsLabel";
            this.SecondsLabel.Size = new System.Drawing.Size(49, 13);
            this.SecondsLabel.TabIndex = 11;
            this.SecondsLabel.Text = "Seconds";
            // 
            // MinutesLabel
            // 
            this.MinutesLabel.AutoSize = true;
            this.MinutesLabel.Location = new System.Drawing.Point(57, 42);
            this.MinutesLabel.Name = "MinutesLabel";
            this.MinutesLabel.Size = new System.Drawing.Size(44, 13);
            this.MinutesLabel.TabIndex = 10;
            this.MinutesLabel.Text = "Minutes";
            // 
            // HoursLabel
            // 
            this.HoursLabel.AutoSize = true;
            this.HoursLabel.Location = new System.Drawing.Point(6, 42);
            this.HoursLabel.Name = "HoursLabel";
            this.HoursLabel.Size = new System.Drawing.Size(35, 13);
            this.HoursLabel.TabIndex = 9;
            this.HoursLabel.Text = "Hours";
            // 
            // MillisecondsEntry
            // 
            this.MillisecondsEntry.Location = new System.Drawing.Point(168, 19);
            this.MillisecondsEntry.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.MillisecondsEntry.Name = "MillisecondsEntry";
            this.MillisecondsEntry.Size = new System.Drawing.Size(48, 20);
            this.MillisecondsEntry.TabIndex = 8;
            // 
            // SecondsEntry
            // 
            this.SecondsEntry.Location = new System.Drawing.Point(114, 19);
            this.SecondsEntry.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.SecondsEntry.Name = "SecondsEntry";
            this.SecondsEntry.Size = new System.Drawing.Size(48, 20);
            this.SecondsEntry.TabIndex = 7;
            // 
            // MinutesEntry
            // 
            this.MinutesEntry.Location = new System.Drawing.Point(60, 19);
            this.MinutesEntry.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.MinutesEntry.Name = "MinutesEntry";
            this.MinutesEntry.Size = new System.Drawing.Size(48, 20);
            this.MinutesEntry.TabIndex = 6;
            // 
            // HoursEntry
            // 
            this.HoursEntry.Location = new System.Drawing.Point(6, 19);
            this.HoursEntry.Name = "HoursEntry";
            this.HoursEntry.Size = new System.Drawing.Size(48, 20);
            this.HoursEntry.TabIndex = 5;
            // 
            // TargetDistanceLabel
            // 
            this.TargetDistanceLabel.AutoSize = true;
            this.TargetDistanceLabel.Location = new System.Drawing.Point(255, 22);
            this.TargetDistanceLabel.Name = "TargetDistanceLabel";
            this.TargetDistanceLabel.Size = new System.Drawing.Size(86, 13);
            this.TargetDistanceLabel.TabIndex = 6;
            this.TargetDistanceLabel.Text = "Target Distance:";
            // 
            // MilePostSettingsGrid
            // 
            this.MilePostSettingsGrid.AllowUserToResizeColumns = false;
            this.MilePostSettingsGrid.AllowUserToResizeRows = false;
            this.MilePostSettingsGrid.BackgroundColor = System.Drawing.Color.White;
            this.MilePostSettingsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MilePostSettingsGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.MilePostSettingsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MilePostSettingsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PostName,
            this.Description,
            this.PostMile});
            this.MilePostSettingsGrid.Location = new System.Drawing.Point(15, 81);
            this.MilePostSettingsGrid.Name = "MilePostSettingsGrid";
            this.MilePostSettingsGrid.Size = new System.Drawing.Size(997, 478);
            this.MilePostSettingsGrid.TabIndex = 7;
            this.MilePostSettingsGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.OnMilePostSettingsGridCellValidating);
            // 
            // PostName
            // 
            this.PostName.DataPropertyName = "Name";
            this.PostName.HeaderText = "Post Name";
            this.PostName.MaxInputLength = 15;
            this.PostName.Name = "PostName";
            this.PostName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PostName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.MaxInputLength = 255;
            this.Description.Name = "Description";
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Description.Width = 754;
            // 
            // PostMile
            // 
            this.PostMile.DataPropertyName = "Mile";
            this.PostMile.HeaderText = "Mile";
            this.PostMile.MaxInputLength = 10;
            this.PostMile.Name = "PostMile";
            this.PostMile.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PostMile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ComPortTextBox
            // 
            this.ComPortTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ComPortTextBox.Location = new System.Drawing.Point(347, 47);
            this.ComPortTextBox.Name = "ComPortTextBox";
            this.ComPortTextBox.Size = new System.Drawing.Size(100, 20);
            this.ComPortTextBox.TabIndex = 8;
            this.ComPortTextBox.Text = "COM6";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(312, 50);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(29, 13);
            this.PortLabel.TabIndex = 9;
            this.PortLabel.Text = "Port:";
            // 
            // TargetDistanceEntry
            // 
            this.TargetDistanceEntry.DecimalPlaces = 3;
            this.TargetDistanceEntry.Location = new System.Drawing.Point(347, 22);
            this.TargetDistanceEntry.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.TargetDistanceEntry.Name = "TargetDistanceEntry";
            this.TargetDistanceEntry.Size = new System.Drawing.Size(87, 20);
            this.TargetDistanceEntry.TabIndex = 13;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.TargetDistanceEntry);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.ComPortTextBox);
            this.Controls.Add(this.MilePostSettingsGrid);
            this.Controls.Add(this.TargetDistanceLabel);
            this.Controls.Add(this.TargetTimeGroup);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.CancelButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingsForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SettingsForm";
            this.TargetTimeGroup.ResumeLayout(false);
            this.TargetTimeGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MillisecondsEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SecondsEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinutesEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HoursEntry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MilePostSettingsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TargetDistanceEntry)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button SaveButton;
        private GroupBox TargetTimeGroup;
        private NumericUpDown MillisecondsEntry;
        private NumericUpDown SecondsEntry;
        private NumericUpDown MinutesEntry;
        private NumericUpDown HoursEntry;
        private Label MillisecondsLabel;
        private Label SecondsLabel;
        private Label MinutesLabel;
        private Label HoursLabel;
        private Label TargetDistanceLabel;
        private DataGridView MilePostSettingsGrid;
        private DataGridViewTextBoxColumn PostName;
        private DataGridViewTextBoxColumn Description;
        private DataGridViewTextBoxColumn PostMile;
        private TextBox ComPortTextBox;
        private Label PortLabel;
        private NumericUpDown TargetDistanceEntry;
    }
}
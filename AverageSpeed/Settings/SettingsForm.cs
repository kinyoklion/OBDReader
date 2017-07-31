namespace AverageSpeed.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Settings;

    public partial class SettingsForm : Form
    {
        #region Events

        /// <summary>
        /// Event fired when the user wants to save.
        /// </summary>
        public EventHandler<EventArgs> Save;

        /// <summary>
        /// Event fired when the user wants to cancel.
        /// </summary>
        public EventHandler<EventArgs> Cancel;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets/Sets the target time.
        /// </summary>
        public TimeSpan TargetTime
        {
            set
            {
                HoursEntry.Value = value.Hours;
                MinutesEntry.Value = value.Minutes;
                SecondsEntry.Value = value.Seconds;
                MillisecondsEntry.Value = value.Milliseconds;
            } 

            get
            {
                return new TimeSpan(0, (int) HoursEntry.Value, (int) MinutesEntry.Value, (int) SecondsEntry.Value,
                                    (int) MillisecondsEntry.Value);
            }
        }

        /// <summary>
        /// Gets/sets the target speed.
        /// </summary>
        public decimal TargetDistance
        {
            set
            {
                TargetDistanceEntry.Value = value;
            } 

            get
            {
                return TargetDistanceEntry.Value;
            }
        }

        /// <summary>
        /// Gets the mile posts.
        /// </summary>
        public IList<MilePostSetting> MilePosts { private set; get; }

        /// <summary>
        /// Gets/Sets the com port.
        /// </summary>
        public string ComPort
        {
            set { ComPortTextBox.Text = value; }
            get { return ComPortTextBox.Text; }
        }

        #endregion

        #region Fields

        /// <summary>
        /// Binding source for the mile post data grid view.
        /// </summary>
        private readonly BindingSource milePostsBinding = new BindingSource();

        #endregion

        #region Constructor

        /// <summary>
        /// Construct an instance of the form with the given settings.
        /// </summary>
        /// <param name="settings">Settings to initialize the form with.</param>
        public SettingsForm(SessionSettings settings)
        {
            InitializeComponent();
            TargetTime = settings.TargetTime;
            TargetDistance = settings.TargetDistance;
            MilePosts = new List<MilePostSetting>(settings.MilePosts);
            milePostsBinding.DataSource = MilePosts;
            MilePostSettingsGrid.DataSource = milePostsBinding;
            ComPort = settings.Port;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Handler for the save button press.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="arguments">Arguments for the event.</param>
        private void OnSaveButtonPressed(object sender, EventArgs arguments)
        {
            var handler = Save;
            if(Save != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handler for the cancel button press.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="arguments">Arguments for the event.</param>
        private void OnCancelButtonPressed(object sender, EventArgs arguments)
        {
            var handler = Cancel;
            if(Cancel != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Event handler which handles validation of entries in the mile post settings.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnMilePostSettingsGridCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var row = MilePostSettingsGrid.Rows[e.RowIndex];
            var cell = row.Cells[e.ColumnIndex];
            if(cell.OwningColumn.DataPropertyName == "Mile")
            {
                double doubleValue;
                if (!double.TryParse(e.FormattedValue.ToString(), out doubleValue))
                {
                    e.Cancel = true;
                    row.ErrorText = "Value must be a double.";
                }
            }
        }

        #endregion
    }
}

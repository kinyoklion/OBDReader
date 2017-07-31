namespace AverageSpeed.AverageSpeed
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Forms;

    /// <summary>
    /// View for showing average speed information.
    /// </summary>
    public partial class AverageSpeedForm : Form
    {
        #region Private Fields

        /// <summary>
        /// Format string for time display.
        /// </summary>
        private const string TimerFormatString = "{0}:{1}:{2}:{3}";

        /// <summary>
        /// Elapsed time for a session.
        /// </summary>
        private TimeSpan elapsedTime;

        /// <summary>
        /// Remaining time in the session.
        /// </summary>
        private TimeSpan remainingTime;

        /// <summary>
        /// Average speed for a session.
        /// </summary>
        private double averageSpeed;

        /// <summary>
        /// The current speed.
        /// </summary>
        private double speed;

        /// <summary>
        /// The peak speed for the session.
        /// </summary>
        private double peakSpeed;

        /// <summary>
        /// Distance covered in the session.
        /// </summary>
        private double distance;

        /// <summary>
        /// The maximum length of the log.
        /// </summary>
        private const int MaxLogCount = 2000;

        /// <summary>
        /// Number of items beyond the current mile post to ensure are visible in the milepost list.
        /// </summary>
        private const int ForwardVisible = 3;

        /// <summary>
        /// The rate at which the form attempts to update. The actual rate may be slower.
        /// </summary>
        private const int RefreshRateTargetMilliseconds = 16;

        /// <summary>
        /// Timer for refreshing the data shown on the form.
        /// </summary>
        private readonly Timer refreshTimer = new Timer();

        /// <summary>
        /// Color which indicates that the target speed is being exceeded.
        /// </summary>
        private readonly Color overSpeedColor = Color.Red;

        /// <summary>
        /// Color which indicates that the target speed is not being met.
        /// </summary>
        private readonly Color underSpeedColor = Color.Blue;

        /// <summary>
        /// Color which indicates that something is on track.
        /// </summary>
        private readonly Color onTrackColor = Color.Green;

        /// <summary>
        /// Boolean which indicates is the average speed is on track.
        /// </summary>
        private SpeedStatus averageSpeedOnTarget;

        /// <summary>
        /// Flag indicating if stop is logically enabled.
        /// </summary>
        private bool stopCapable;

        /// <summary>
        /// Flag indicating if the disconnect is logically enabled.
        /// </summary>
        private bool disconnectCapable;

        /// <summary>
        /// Flag indicating if ready and start are enabled.
        /// </summary>
        private bool readyStartEnabled;

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets/Sets a flag indicating if ready and start are enabled.
        /// </summary>
        /// <remarks>Currently uses the button state.</remarks>
        private bool CanStart
        {
            set
            {
                ReadyButton.Enabled = value;
                StartButton.Enabled = value;
                readyStartEnabled = value;
            }
            get { return readyStartEnabled; }
        }

        /// <summary>
        /// Gets/Sets a flag indicating if race mode is enabled.
        /// </summary>
        private bool RaceEnabled
        {
            get { return RaceEnabledCheckbox.Checked; }
        }

        /// <summary>
        /// Gets/Sets a flag indicating if stop is enabled.
        /// </summary>
        /// <remarks>Currently uses the button state.</remarks>
        private bool StopEnabled
        {
            set
            {
                stopCapable = value;
                UpdateStopButtonState();
            }
            get
            {
                return stopCapable;
            }
        }

        /// <summary>
        /// Gets/Sets a flag indicating if connect is enabled.
        /// </summary>
        /// <remarks>Currently uses the button state.</remarks>
        private bool ConnectEnabled
        {
            set { ConnectButton.Enabled = value; }
            get { return ConnectButton.Enabled; }
        }

        /// <summary>
        /// Gets/Sets a flag indicating if disconnect is enabled.
        /// </summary>
        /// <remarks>Currently uses the button state.</remarks>
        private bool DisconnectEnabled
        {
            set
            {
                disconnectCapable = value;
                UpdateDisconnectButtonState();
            }
            get { return disconnectCapable; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event fired when the user wants to connect.
        /// </summary>
        public EventHandler<ConnectEventArgs> Connect;

        /// <summary>
        /// Event fired when the user wants to disconnect.
        /// </summary>
        public EventHandler<DisconnectEventArgs> Disconnect;

        /// <summary>
        /// Event fired when the user is ready to track the average speed.
        /// Data collection will start once the vehicle speed is not 0.
        /// </summary>
        public EventHandler<EventArgs> Ready;

        /// <summary>
        /// Event fired when the user wants to start a session.
        /// </summary>
        public EventHandler<EventArgs> Start;

        /// <summary>
        /// Event fired when the user wishes to stop tracking the average speed.
        /// </summary>
        public EventHandler<EventArgs> Stop;

        /// <summary>
        /// Event fired when the form is being closed.
        /// </summary>
        public EventHandler<EventArgs> ViewClosing;

        /// <summary>
        /// Event fired when the view would like to have its data updated.
        /// </summary>
        public EventHandler<EventArgs> RefreshData;

        /// <summary>
        /// Event fired when the user wants to change settings.
        /// </summary>
        public EventHandler<EventArgs> OpenSettings;

        /// <summary>
        /// Event fired when a mile post is marked.
        /// </summary>
        public EventHandler<MarkPostEventArgs> MarkPost;

        /// <summary>
        /// Event fired when the user enables or disables session logging.
        /// </summary>
        public EventHandler<LogSessionChangedEventArgs> LogSessionChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets/Sets the elapsed time.
        /// </summary>
        public TimeSpan ElapsedTime
        {
            set
            {
                TimeLabel.Text = string.Format(CultureInfo.InvariantCulture, TimerFormatString,
                                               value.Hours.ToString("D2"),
                                               value.Minutes.ToString("D2"),
                                               value.Seconds.ToString("D2"),
                                               value.Milliseconds.ToString("D3"));
                elapsedTime = value;
            }
            get { return elapsedTime; }
        }


        /// <summary>
        /// Gets/Sets the remaining time.
        /// </summary>
        public TimeSpan RemainingTime
        {
            set
            {
                RemainingTimeLabel.Text = string.Format(CultureInfo.InvariantCulture, TimerFormatString,
                               value.Hours.ToString("D2"),
                               value.Minutes.ToString("D2"),
                               value.Seconds.ToString("D2"),
                               value.Milliseconds.ToString("D3"));

                remainingTime = value;
            }
            get { return remainingTime; }
        }

        /// <summary>
        /// Gets/Sets the average speed in miles per hour.
        /// </summary>
        public double AverageSpeed
        {
            set
            {
                AverageCalcMLabel.Text = value.ToString("F2");
                averageSpeed = value;
            }
            get { return averageSpeed; }
        }

        /// <summary>
        /// Gets/Sets an enumeration indicating if the average speed is on track.
        /// </summary>
        public SpeedStatus AverageSpeedOnTarget
        {
            set
            {
                averageSpeedOnTarget = value;
                AverageCalcMLabel.ForeColor = GetStatusColor(value);
            }
            get
            {
                return averageSpeedOnTarget;
            }
        }

        /// <summary>
        /// Gets/Sets the current speed in miles per hour.
        /// </summary>
        public double Speed
        {
            set 
            { 
                SpeedLabel.Text = value.ToString("F2");
                speed = value;
            }
            get { return speed; }
        }

        /// <summary>
        /// Sets the target speed in miles per hour.
        /// </summary>
        public double TargetSpeed
        {
            set { TargetSpeedLabel.Text = value.ToString(("F2")); }
        }

        /// <summary>
        /// Gets/Sets the peak speed for the session.
        /// </summary>
        public double PeakSpeed
        {
            set
            {
                PeakSpeedLabel.Text = value.ToString("F2");
                peakSpeed = value;
            }
            get { return peakSpeed; }
        }

        /// <summary>
        /// Show a message for the error.
        /// </summary>
        /// <param name="message">Message to show.</param>
        /// <param name="exception">Exception which caused the error.</param>
        public void ShowError(string message, Exception exception)
        {
            MessageBox.Show(message + Environment.NewLine + exception);
        }

        /// <summary>
        /// Gets/Sets the distance traveled.
        /// </summary>
        public double Distance
        {
            set
            {
                CalculatedDistanceLabel.Text = value.ToString("F2");
                distance = value;
            }
            get { return distance; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construct an instance of the forum.
        /// </summary>
        public AverageSpeedForm()
        {
            InitializeComponent();
            refreshTimer.Tick += OnRefreshTimer;
            refreshTimer.Interval = RefreshRateTargetMilliseconds;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the log with the given items.
        /// </summary>
        /// <param name="itemsToAdd">The items to add to the log.</param>
        public void UpdateLog(IEnumerable<string> itemsToAdd)
        {
            if (LoggingEnabledCheckBox.Checked)
            {
                foreach (var item in itemsToAdd)
                {
                    CommandBox.Items.Add(item);
                    if (CommandBox.Items.Count > MaxLogCount)
                    {
                        CommandBox.Items.RemoveAt(0);
                    }
                }

                CommandBox.SelectedIndex = CommandBox.Items.Count - 1;
                CommandBox.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Disable setup.
        /// </summary>
        public void DisableSetup()
        {
            SetupButton.Enabled = false;
        }

        /// <summary>
        /// Enable setup.
        /// </summary>
        public void EnableSetup()
        {
            SetupButton.Enabled = true;
        }

        /// <summary>
        /// Update the form to indicate that it is disconnected.
        /// </summary>
        public void SetDisconnected()
        {
            ConnectEnabled = true;
            DisconnectEnabled = false;

            StopEnabled = false;
            CanStart = false;
        }

        /// <summary>
        /// Update the form to indicate that it is connected.
        /// </summary>
        public void SetConnected()
        {
            ConnectEnabled = false;
            DisconnectEnabled = true;
            SetStopped();

            refreshTimer.Start();
        }

        /// <summary>
        /// Update the form to indicate it is in the ready or started state.
        /// </summary>
        public void SetStart()
        {
            CanStart = false;
            StopEnabled = true;
            LogSessionCheckbox.Enabled = false;
        }

        /// <summary>
        /// Update the form to indicate that it is in the stopped state.
        /// </summary>
        public void SetStopped()
        {
            CanStart = true;
            StopEnabled = false;
            LogSessionCheckbox.Enabled = true;
        }

        /// <summary>
        /// Set the mile posts for the view.
        /// </summary>
        /// <param name="milePosts">The mile posts to show in the view.</param>
        public void SetMilePosts(IEnumerable<MilePost> milePosts)
        {
            MilePostListView.Items.Clear();

            foreach (var listViewItem in milePosts.Select(CreateListViewItem))
            {
                MilePostListView.Items.Add(listViewItem);
            }
        }

        /// <summary>
        /// Update the given mile post in the post list.
        /// </summary>
        /// <param name="milePost">The mile post to update.</param>
        public void UpdateMilePost(MilePost milePost)
        {
            for (var index = 0; index < MilePostListView.Items.Count; index++)
            {
                var post = (MilePost)MilePostListView.Items[index].Tag;
                if(post.Id == milePost.Id)
                {
                    var item = MilePostListView.Items[index];
                    UpdateListViewItem(item, milePost);
                    item.BackColor = GetStatusColor(milePost.OnTarget);
                    item.ForeColor = Color.White;

                    var selectedIndex = index + 1 >= MilePostListView.Items.Count ? index : index + 1;

                    //Keep several following mile posts viewable.
                    var visibleIndex = selectedIndex + ForwardVisible >= MilePostListView.Items.Count
                                           ? MilePostListView.Items.Count - 1
                                           : selectedIndex + ForwardVisible;

                    MilePostListView.EnsureVisible(visibleIndex);

                    MilePostListView.SelectedIndices.Clear();
                    MilePostListView.SelectedIndices.Add(selectedIndex);
                    //Must focus the item otherwise navigation with the arrow keys will not work.
                    MilePostListView.FocusedItem = MilePostListView.Items[selectedIndex];
                    break;
                }
            }
        }

        /// <summary>
        /// Post any events for the default state.
        /// </summary>
        public void Initialize()
        {
            UpdateSessionLogEnabledStatus();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create a list view item from the given mile post.
        /// </summary>
        /// <param name="milePost">Mile post to create the list view item for.</param>
        /// <returns>List view item for the given mile post.</returns>
        private static ListViewItem CreateListViewItem(MilePost milePost)
        {
            var listViewItem = new ListViewItem(milePost.Settings.Name) { Tag = milePost };
            listViewItem.SubItems.Add(milePost.Settings.Description);
            listViewItem.SubItems.Add(milePost.Settings.Mile.ToString());
            listViewItem.SubItems.Add(milePost.TargetTime.ToString());
            listViewItem.SubItems.Add(milePost.Deviation.ToString());
            listViewItem.SubItems.Add(milePost.Odometer.ToString("F2"));
            listViewItem.SubItems.Add(milePost.Speed.ToString("F2"));
            return listViewItem;
        }

        /// <summary>
        /// Update a list view item with the given mile post.
        /// </summary>
        /// <param name="item">Item to update.</param>
        /// <param name="milePost">Post to get values from.</param>
        private static void UpdateListViewItem(ListViewItem item, MilePost milePost)
        {
            item.SubItems[4].Text = milePost.Deviation.ToString();
            item.SubItems[5].Text = milePost.Odometer.ToString("F2");
            item.SubItems[6].Text = milePost.Speed.ToString("F2");
            item.Tag = milePost;
        }

        /// <summary>
        /// Handle the ready action. Triggered by shortcut or the ready button.
        /// </summary>
        private void PerformReadyAction()
        {
            if (CanStart)
            {
                var handler = Ready;
                if (handler != null)
                {
                    handler(this, new EventArgs());
                }

                PrepareMilePostListViewForSession();
            }
        }

        /// <summary>
        /// Handle the start action. Triggered by shortcut or the start button.
        /// </summary>
        private void PerformStartAction()
        {
            if(CanStart)
            {
                var handler = Start;
                if(handler != null)
                {
                    handler(this, new EventArgs());
                }
                
                PrepareMilePostListViewForSession();
            }
        }

        /// <summary>
        /// Perform the stop action.
        /// </summary>
        private void PerformStopAction()
        {
            if (StopEnabled && !RaceEnabled)
            {
                var handler = Stop;
                if (handler != null)
                {
                    handler(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Perform the connect action.
        /// </summary>
        private void PerformConnectAction()
        {
            if (ConnectEnabled)
            {
                var handler = Connect;
                if(handler != null)
                {
                    handler(this, new ConnectEventArgs());
                }
            }
        }

        /// <summary>
        /// Perform the disconnect action.
        /// </summary>
        private void PerformDisconnectAction()
        {
            if (DisconnectEnabled && !RaceEnabled)
            {
                refreshTimer.Stop();
                var handler = Disconnect;
                if(handler != null)
                {
                    handler(this, new DisconnectEventArgs());
                }
            }
        }

        /// <summary>
        /// Function which updates the stop button state.
        /// </summary>
        private void UpdateStopButtonState()
        {
            StopButton.Enabled = RaceEnabled ? false : StopEnabled;
        }

        /// <summary>
        /// Function which updates the disconnect button state.
        /// </summary>
        private void UpdateDisconnectButtonState()
        {
            DisconnectButton.Enabled = RaceEnabled ? false : DisconnectEnabled;
        }

        /// <summary>
        /// Post the event which refreshes the session log enabled status.
        /// </summary>
        private void UpdateSessionLogEnabledStatus()
        {
            var handler = LogSessionChanged;

            if (handler != null)
            {
                handler(this, new LogSessionChangedEventArgs(LogSessionCheckbox.Checked));
            }
        }

        /// <summary>
        /// Prepare the mile post list view for a session.
        /// </summary>
        private void PrepareMilePostListViewForSession()
        {
            MilePostListView.Select();

            if (MilePostListView.Items.Count > 0)
            {
                MilePostListView.SelectedIndices.Clear();
                MilePostListView.SelectedIndices.Add(0);
            }
        }

        /// <summary>
        /// Get the color for the specified status.
        /// </summary>
        /// <param name="value">The status to get a color for.</param>
        /// <returns>The color for the status.</returns>
        private Color GetStatusColor(SpeedStatus value)
        {
            var color = onTrackColor;

            switch (value)
            {
                case SpeedStatus.OverTarget:
                    color = overSpeedColor;
                    break;
                case SpeedStatus.UnderTarget:
                    color = underSpeedColor;
                    break;
                default:
                    break;
            }
            return color;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the connect button being clicked.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnConnectButtonClick(object sender, EventArgs e)
        {
            PerformConnectAction();
        }

        /// <summary>
        /// Handles the disconnect button being clicked.
        /// </summary>
        /// <param name="sender">The originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnDisconnectButtonClick(object sender, EventArgs e)
        {
            PerformDisconnectAction();
        }

        /// <summary>
        /// Handles the forum closing.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnAverageSpeedFormClosing(object sender, FormClosingEventArgs e)
        {
            var handler = ViewClosing;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the refresh timer elapsing.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="eventArgs">Arguments for the event.</param>
        private void OnRefreshTimer(object sender, EventArgs eventArgs)
        {
            refreshTimer.Stop();

            var handler = RefreshData;
            if(handler != null)
            {
                handler(this, new EventArgs());
            }

            refreshTimer.Start();
        }

        /// <summary>
        /// Handles the ready button being clicked.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnReadyButtonClick(object sender, EventArgs e)
        {
            PerformReadyAction();
        }

        /// <summary>
        /// Handles the stop button being clicked.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnStopButtonClicked(object sender, EventArgs e)
        {
            PerformStopAction();
        }

        /// <summary>
        /// Handles the clear log button being pressed.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnClearLog(object sender, EventArgs e)
        {
            CommandBox.Items.Clear();
        }

        /// <summary>
        /// Handles the setup button being pressed.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnSetupButtonClicked(object sender, EventArgs e)
        {
            var handler = OpenSettings;
            if(handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles key presses in the mile post list.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnMilePostListKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == ' ')
            {
                var item = (MilePost)MilePostListView.SelectedItems[0].Tag;
                if(item != null)
                {
                    var handler = MarkPost;
                    if(handler != null)
                    {
                        handler(this, new MarkPostEventArgs(item.Id));
                        e.Handled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Handle the column width changing in the mile post list view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnMilePostListViewColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            //There is a bug that allows the columns to resize even when cancelled.
            //Forcing the width to the old width fixes this.
            e.NewWidth = MilePostListView.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        /// <summary>
        /// Global key handler. Used to create shortcuts.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnFormKeyPress(object sender, KeyPressEventArgs e)
        {
            var lowerChar = char.ToLower(e.KeyChar);
            if(lowerChar == 'r')
            {
                PerformReadyAction();
                e.Handled = true;
            }
            if(lowerChar == 'c')
            {
                PerformConnectAction();
                e.Handled = true;
            }
            if(lowerChar == 's')
            {
                PerformStartAction();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handle a toggle of the race enabled check box.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnRaceEnabledToggle(object sender, EventArgs e)
        {
            UpdateStopButtonState();
            UpdateDisconnectButtonState();
        }

        /// <summary>
        /// Handle a toggle of the session logging check box.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnLogSessionCheckboxChanged(object sender, EventArgs e)
        {
            UpdateSessionLogEnabledStatus();
        }

        /// <summary>
        /// Handle the start button being pressed.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="e">Arguments for the event.</param>
        private void OnStartButtonClick(object sender, EventArgs e)
        {
            PerformStartAction();
        }

        #endregion
    }
}

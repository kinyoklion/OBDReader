namespace AverageSpeed.AverageSpeed
{
    using System;
    using System.IO;
    using OBDReader.Obd2;
    using Settings;

    /// <summary>
    /// Controller for the AverageSpeed.
    /// </summary>
    class AverageSpeedController : IDisposable
    {
        #region Fields

        /// <summary>
        /// The view being controlled.
        /// </summary>
        private readonly AverageSpeedForm view;

        /// <summary>
        /// The model being controlled.
        /// </summary>
        private readonly AverageSpeedModel model;

        /// <summary>
        /// Boolean indicating if the settings dialog is open.
        /// </summary>
        private bool settingsOpen;

        /// <summary>
        /// The settings to use for the next session.
        /// </summary>
        private SessionSettings sessionSettings;

        /// <summary>
        /// File name to use for saving settings.
        /// </summary>
        private const string SettingsFileName = "AverageSpeedSettings.dat";

        #endregion

        #region Constructor

        /// <summary>
        /// Construct an instance of the controller.
        /// </summary>
        /// <param name="view">The view the controller will be controlling.</param>
        public AverageSpeedController(AverageSpeedForm view)
        {
            try
            {
                sessionSettings = SessionSettings.LoadSettings(SettingsFileName);
            }
            catch (FileNotFoundException)
            { 
                sessionSettings = new SessionSettings();
            }

            model = new AverageSpeedModel();
 
            this.view = view;
            view.Connect += OnConnect;
            view.Disconnect += OnDisconnect;
            view.ViewClosing += OnViewClosing;
            view.RefreshData += OnRefreshData;
            view.Ready += OnReady;
            view.Stop += OnStop;
            view.OpenSettings += OnOpenSettings;
            view.MarkPost += OnMarkPost;
            view.LogSessionChanged += OnLogSessionChanged;
            view.Start += OnStart;

            model.MilePostListChanged += OnMilePostListChanged;
            model.MilePostUpdated += OnMilePostUpdated;
            model.Disconnected += OnModelDisconnected;

            ApplyModelSettings();

            view.Initialize();
        }

        #endregion

        #region View Event Handlers

        /// <summary>
        /// Handle the connect event from the view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="connectArguments">Arguments for the event. Contains the port to connect on.</param>
        private void OnConnect(object sender, ConnectEventArgs connectArguments)
        {
            try
            {
                model.Connect(9600, Protocol.Iso15765V4Can11Bit500Kbaud);
                view.SetConnected();
            }
            catch(Exception e)
            {
                model.Disconnect();
                view.ShowError("Unable to connect:", e);
            }
        }

        /// <summary>
        /// Handle the disconnect event from the view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="disconnectArguments">Arguments for the event.</param>
        private void OnDisconnect(object sender, DisconnectEventArgs disconnectArguments)
        {
            model.Disconnect();
        }

        /// <summary>
        /// Handle the disconnected event from the model.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="arguments">Arguments for the event.</param>
        private void OnModelDisconnected(object sender, EventArgs arguments)
        {
            view.SetDisconnected();
            view.EnableSetup();
        }

        /// <summary>
        /// Handle the view closing event.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="viewClosingArguments">Arguments for the event.</param>
        private void OnViewClosing(object sender, EventArgs viewClosingArguments)
        {
            model.Disconnect();
        }

        /// <summary>
        /// Handle the ready event from the view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="eventArgs">Arguments for the vent.</param>
        private void OnReady(object sender, EventArgs eventArgs)
        {
            view.DisableSetup();
            model.Ready();
            view.SetStart();
        }

        /// <summary>
        /// Handle the stop event from the view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="eventArgs">Arguments for the event.</param>
        private void OnStop(object sender, EventArgs eventArgs)
        {
            view.EnableSetup();
            model.Stop();
            view.SetStopped();
        }

        /// <summary>
        /// Handle the refresh data event from the view. This event is used to update the model and apply its data to
        /// the view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="timerEventArgs">Arguments for the event.</param>
        private void OnRefreshData(object sender, EventArgs timerEventArgs)
        {
            try
            {
                model.Update();
            }
            catch(Exception e)
            {
                model.Disconnect();
                view.ShowError("Error updating:", e);
            }
            
            var averageMilesPerHour = model.AverageSpeedMph;
            var distance = model.DistanceMiles;
            var milesPerHour = model.SpeedMph;

            view.Distance = distance;
            view.AverageSpeed = averageMilesPerHour;
            view.ElapsedTime = model.ElapsedTime;
            view.UpdateLog(model.GetCommandLog());
            view.Speed = milesPerHour;
            view.AverageSpeedOnTarget = model.AverageSpeedOnTarget;
            view.RemainingTime = model.RemainingTime;
            view.PeakSpeed = model.PeakSpeedMph;
        }

        /// <summary>
        /// Handle the open settings event from the view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="arguments">Arguments for the event.</param>
        private void OnOpenSettings(object sender, EventArgs arguments)
        {
            if (!settingsOpen)
            {
                settingsOpen = true;
                view.DisableSetup();

                var settingsController = new SettingsController(sessionSettings);
                settingsController.SettingsClosed += OnSettingsClosed;
                settingsController.ApplySettings += OnApplySettings;

                settingsController.ShowDialog(view.Location);
            }
        }

        /// <summary>
        /// Handle the closed event from the settings dialog.
        /// </summary>
        /// <param name="sender">The settings dialog which was closed.</param>
        /// <param name="arguments">Arguments for the event.</param>
        private void OnSettingsClosed(object sender, EventArgs arguments)
        {
            settingsOpen = false;
            var settingsController = (SettingsController) sender;
            if(settingsController != null)
            {
                settingsController.SettingsClosed -= OnSettingsClosed;
                settingsController.ApplySettings -= OnApplySettings;
                settingsController.Dispose();
            }
            
            view.EnableSetup();
        }

        /// <summary>
        /// Handle the apply settings event from the settings controller.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="arguments">Arguments containing the settings to apply.</param>
        private void OnApplySettings(object sender, ApplySettingsEventArgs arguments)
        {
            sessionSettings = arguments.Settings;
            sessionSettings.WriteSettings(SettingsFileName);
            ApplyModelSettings();
        }

        /// <summary>
        /// Handle the mark post event from the average speed view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="eventArgs">Arguments for the event.</param>
        private void OnMarkPost(object sender, MarkPostEventArgs eventArgs)
        {
            model.MarkMilePostPassed(eventArgs.MilePostId);
        }

        /// <summary>
        /// Handle the log session changed event from the average speed view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="eventArgs">Arguments for the event.</param>
        private void OnLogSessionChanged(object sender, LogSessionChangedEventArgs eventArgs)
        {
            model.LogSession = eventArgs.SessionLoggingEnabled;
        }

        /// <summary>
        /// Handle a start event from the view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="eventArgs">Arguments for the event.</param>
        private void OnStart(object sender, EventArgs eventArgs)
        {
            view.DisableSetup();
            model.Start();
            view.SetStart();
        }

        #endregion

        #region Model Event Handlers

        /// <summary>
        /// Event handler to handle mile posts being updated.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="milePostUpdatedArgs">Arguments for the event.</param>
        private void OnMilePostUpdated(object sender, MilePostUpdatedEventArgs milePostUpdatedArgs)
        {
            view.UpdateMilePost(milePostUpdatedArgs.Post);
        }

        /// <summary>
        /// Event handler to handle the mile post list changing.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="eventArgs">Arguments for the event.</param>
        private void OnMilePostListChanged(object sender, EventArgs eventArgs)
        {
            view.SetMilePosts(model.MilePosts);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Apply the current settings to the model.
        /// </summary>
        private void ApplyModelSettings()
        {
            model.TargetSpeedMph = sessionSettings.TargetSpeed;
            model.TargetTime = sessionSettings.TargetTime;
            model.SetMilePosts(sessionSettings.MilePosts);
            model.Port = sessionSettings.Port;

            view.TargetSpeed = model.TargetSpeedMph;
        }

        #endregion

        #region Finalizer

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~AverageSpeedController()
        {
            Dispose(false);
        }

        #endregion

        #region IDisposable Implementation

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose any disposable objects or unmanaged resources.
        /// </summary>
        /// <param name="disposing">True if disposing, false if finalizing.</param>
        protected void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(model != null)
                {
                    model.Dispose();
                }
            }
        }

        #endregion
    }
}

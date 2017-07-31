namespace AverageSpeed.Settings
{
    using System;
    using System.Drawing;
    using System.Linq;
    using Settings;

    /// <summary>
    /// Controller for the settings view.
    /// </summary>
    class SettingsController : IDisposable
    {
        #region Fields

        /// <summary>
        /// The view for this controller.
        /// </summary>
        private readonly SettingsForm view;

        #endregion

        #region Events

        /// <summary>
        /// Event fired when the settings are closed.
        /// </summary>
        public EventHandler<EventArgs> SettingsClosed;

        /// <summary>
        /// Event fired when settings should be applied.
        /// </summary>
        public EventHandler<ApplySettingsEventArgs> ApplySettings;

        #endregion

        #region Constructor

        /// <summary>
        /// Construct an instance of the settings controller.
        /// </summary>
        /// <param name="sessionSettings">Current application settings.</param>
        public SettingsController(SessionSettings sessionSettings)
        {
            view = new SettingsForm(sessionSettings);
            view.Cancel += OnCancel;
            view.Save += OnSave;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Show the settings dialog.
        /// </summary>
        public void ShowDialog(Point location)
        {
            view.Show();
            view.SetDesktopLocation(location.X, location.Y);
        }

        /// <summary>
        /// Evant handler for the cancel event from the view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="arguments">Arguments for the event.</param>
        private void OnCancel(object sender, EventArgs arguments)
        {
            CloseSettings();
        }

        /// <summary>
        /// Event handler for the save event from the view.
        /// </summary>
        /// <param name="sender">Originator of the event.</param>
        /// <param name="arguments">Arguments for the event.</param>
        private void OnSave(object sender, EventArgs arguments)
        {
            var sessionSettings = new SessionSettings
                                      {
                                          TargetTime = view.TargetTime,
                                          TargetDistance = view.TargetDistance,
                                          MilePosts = view.MilePosts.OrderBy(post => post.Mile).ToList(),
                                          Port = view.ComPort
                                      };

            var applyHandler = ApplySettings;
            if(applyHandler != null)
            {
                applyHandler(this, new ApplySettingsEventArgs(sessionSettings));
            }
            CloseSettings();
        }

        /// <summary>
        /// Close the settings.
        /// </summary>
        private void CloseSettings()
        {
            view.Close();

            var handler = SettingsClosed;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        #endregion

        #region Finalizer

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~SettingsController()
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
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(view != null)
                {
                   view.Dispose();
                }
            }
        }

        #endregion
    }
}

namespace AverageSpeed.Settings
{
    using System;

    /// <summary>
    /// Arguments for the ApplySettings event.
    /// </summary>
    class ApplySettingsEventArgs : EventArgs
    {
        /// <summary>
        /// Settings for the event.
        /// </summary>
        public SessionSettings Settings { private set; get; }

        /// <summary>
        /// Construct an instance of the event args.
        /// </summary>
        /// <param name="settings">Settings to apply.</param>
        public ApplySettingsEventArgs(SessionSettings settings)
        {
            Settings = settings;
        }
    }
}

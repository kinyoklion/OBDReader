namespace AverageSpeed.AverageSpeed
{
    using System;

    /// <summary>
    /// Event arguments for enabling and disabling session logging.
    /// </summary>
    public class LogSessionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Flag indicating if session logging is enabled.
        /// </summary>
        public bool SessionLoggingEnabled { private set; get; }

        /// <summary>
        /// Construct a new instance of the event arguments.
        /// </summary>
        /// <param name="enabled">Flag indicating if logging should be enabled.</param>
        public LogSessionChangedEventArgs(bool enabled)
        {
            SessionLoggingEnabled = enabled;
        }
    }
}

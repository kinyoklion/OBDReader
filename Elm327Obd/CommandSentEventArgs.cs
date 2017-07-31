namespace OBDReader.Obd2
{
    using System;

    /// <summary>
    /// Class containing information about a sent command.
    /// </summary>
    public class CommandSentEventArgs : EventArgs
    {
        /// <summary>
        /// Command which was sent.
        /// </summary>
        public string Command { private set; get; }

        /// <summary>
        /// Construct an instance of the event arguments with the given sender and command.
        /// </summary>
        /// <param name="command">The command which was sent.</param>
        public CommandSentEventArgs(string command)
        {
            Command = command;
        }
    }
}

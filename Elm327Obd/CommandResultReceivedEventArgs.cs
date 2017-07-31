namespace OBDReader.Obd2
{
    using System;

    /// <summary>
    /// Class containing the result of a command.
    /// </summary>
    public class CommandResultReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Command result sent from the ELM327 device.
        /// </summary>
        public string Result { private set; get; }

        /// <summary>
        /// Construct an instance of the event arguments with the given result.
        /// </summary>
        /// <param name="result">Command result received from the ELM327 device.</param>
        public CommandResultReceivedEventArgs(string result)
        {
            Result = result;
        }
    }
}

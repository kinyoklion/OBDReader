namespace OBDReader.Obd2
{
    using System;

    /// <summary>
    /// Class which indicates that an attempt was made to access an invalid PID.
    /// </summary>
    [Serializable]
    public class UnsupportedPidException : Exception
    {
        /// <summary>
        /// The mode of the unsupported PID.
        /// </summary>
        public int Mode { private set; get; }

        /// <summary>
        /// The PID which was not supported.
        /// </summary>
        public int Pid {private set; get;}

        /// <summary>
        /// Construct an instance of the exception with the given arguments.
        /// </summary>
        /// <param name="mode">The mode of the PID.</param>
        /// <param name="pid">The value of the PID.</param>
        public UnsupportedPidException(int mode, int pid)
        {
            Mode = mode;
            Pid = pid;
        }
    }
}

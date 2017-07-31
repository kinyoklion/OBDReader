namespace OBDReader.Obd2
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO.Ports;

    /// <summary>
    /// Class for communicating with the ELM327 device.
    /// </summary>
    public class Elm327 : IDisposable
    {
        #region Fields

        /// <summary>
        /// Serial port used for commmunication.
        /// </summary>
        private readonly SerialPort serialPort;

        /// <summary>
        /// Protocol being used for communication.
        /// </summary>
        private readonly Protocol protocol;

        /// <summary>
        /// Pids supported by the car.
        /// </summary>
        private readonly List<int> supportedPids = new List<int>();

        /// <summary>
        /// Timeout for serial port operations.
        /// </summary>
        private const int SerialPortTimeout = 5000;

        #endregion

        #region Properties

        /// <summary>
        /// List of the PIDs supported by the car.
        /// </summary>
        public IEnumerable<int> SupportedPids { get { return new ReadOnlyCollection<int>(supportedPids); } }

        #endregion

        #region Events

        /// <summary>
        /// Event which is fired whenever a command is sent.
        /// </summary>
        /// <remarks>Can be used for echo.</remarks>
        public EventHandler<CommandSentEventArgs> CommandSent;

        /// <summary>
        /// Event which is fired when a result of a command is received.
        /// </summary>
        public EventHandler<CommandResultReceivedEventArgs> CommandResultReceived;

        #endregion

        #region Nested Types

        /// <summary>
        /// Class containing constants for different available AT commands.
        /// </summary>
        private struct AtCommands
        {
            public const string Reset = "Z";
            public const string DisableEcho = "E0";
            public const string DisableLineFeeds = "L0";
            public const string EnableHeaders = "H1";
            public const string DisableHeahders = "H0";
            public const string SetProtocol = "SP";
            public const string SetBitRate = " BRD";
            public const string SetHeader = " SH";
        }

        /// <summary>
        /// Header for OBD messages.
        /// </summary>
        private class ObdHeader
        {
            /// <summary>
            /// Device the message is from.
            /// </summary>
            public string Device { private set; get; }

            /// <summary>
            /// Size of the message.
            /// </summary>
            public int MessageSize { private set; get; }

            /// <summary>
            /// Mode of the PID the message is from.
            /// </summary>
            public int Mode { private set; get; }

            /// <summary>
            /// PID the message is a response to.
            /// </summary>
            public int Pid { private set; get; }

            /// <summary>
            /// Construct an instance of the header with the given values.
            /// </summary>
            /// <param name="device">Device the message was from.</param>
            /// <param name="messageSize">Size of the message.</param>
            /// <param name="mode">Mode of the message.</param>
            /// <param name="pid">PID of the message.</param>
            public ObdHeader(string device, int messageSize, int mode, int pid)
            {
                Device = device;
                MessageSize = messageSize;
                Mode = mode;
                Pid = pid;
            }
        }

        /// <summary>
        /// Result of a PID command.
        /// </summary>
        private class PidResult
        {
            /// <summary>
            /// Header for the result.
            /// </summary>
            public ObdHeader Header { private set; get; }

            /// <summary>
            /// Data for the result.
            /// </summary>
            public byte[] Data { private set; get; }

            /// <summary>
            /// Construct a PID result.
            /// </summary>
            /// <param name="header">Header of the result.</param>
            /// <param name="data">Data for the result.</param>
            public PidResult(ObdHeader header, byte[] data)
            {
                Header = header;
                Data = data;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Construct an instance of the Elm327 class with the given settings.
        /// </summary>
        /// <param name="comDevice">The string name of the com device. i.e. COM1</param>
        /// <param name="baudRate">Baud rate to use for the connection.</param>
        /// <param name="protocol">Protocol which the car uses.</param>
        public Elm327(string comDevice, int baudRate, Protocol protocol)
        {
            serialPort = new SerialPort(comDevice, baudRate, Parity.None, 8, StopBits.OnePointFive)
                             {
                                 ReadTimeout = SerialPortTimeout,
                                 WriteTimeout = SerialPortTimeout
                             };
            this.protocol = protocol;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connect to the ELM327 device and perform initialization.
        /// </summary>
        public void Connect()
        {
            serialPort.Open();
            serialPort.DiscardInBuffer();
            SendAtCommand(AtCommands.Reset);
            SendAtCommand(AtCommands.DisableEcho);
            SendAtCommand(AtCommands.DisableLineFeeds);
            SendAtCommand(AtCommands.EnableHeaders);
            SendAtCommand(AtCommands.SetProtocol + " " + (int)protocol);
            GetSupportedCurrentDataPids();
        }

        /// <summary>
        /// Close the connection to the device.
        /// </summary>
        public void Disconnect()
        {
            if (serialPort != null)
            {
                serialPort.Close();
            }
            supportedPids.Clear();
        }
        
        /// <summary>
        /// Set the device being comminicated with.
        /// </summary>
        public void SetHeader(string device)
        {
            SendAtCommand(AtCommands.SetHeader + " " + device);
        }

        /// <summary>
        /// Get the current vehicle speed.
        /// </summary>
        /// <returns>The current speed in km/h.</returns>
        public byte GetSpeed()
        {
            var result = SendPid(CurrentDataPid.VehicleSpeed);
            var byteSpeed = GetPidResult(result).Data[0];
            return byteSpeed;
        }

        /// <summary>
        /// Get the distance since the codes were last cleared.
        /// </summary>
        /// <param name="device">ID of the device to get the distance for.</param>
        /// <param name="tries">Number of tries to get the distance.</param>
        /// <returns>Distance since codes cleared in km/h.</returns>
        public int GetDistanceSinceCodesCleared(string device, int tries)
        {
            var result = SendPid(CurrentDataPid.DistanceSingeCodesCleared);

            if(tries == 0 || result.Contains("NO DATA"))
            {
                return 0;
            }

            var pidResult = GetPidResult(result);
            if (pidResult.Header.Device == device)
            {
                Array.Reverse(pidResult.Data);
                return BitConverter.ToUInt16(pidResult.Data, 0);
            }
            else
            {
                tries--;
                return GetDistanceSinceCodesCleared(device, tries);
            }
        }

        /// <summary>
        /// Attempt to increase the baudrate.
        /// </summary>
        public void IncreaseBaudRate()
        {
            var result = SendAtCommand(AtCommands.SetBitRate + " 23");
            if (!result.Contains("?"))
            {
                serialPort.BaudRate = 115200;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get the supported list of pids for the specified PID range.
        /// </summary>
        /// <param name="supportedPidList">List of PIDs to add to.</param>
        /// <param name="pid">PID indicating the range of PIDs to check.</param>
        /// <param name="currentPid">At start it is the first PID to test, at exit it is the next PID to test.</param>
        private void GetSupportedPidListForRange(List<int> supportedPidList, CurrentDataPid pid, ref int currentPid)
        {
            var result = SendPid(pid);
            var supportedPidBytes = GetPidResult(result).Data;

            foreach (var bitField in supportedPidBytes)
            {
                for (var i = 128; i > 0; i >>= 1)
                {
                    if ((i & bitField) != 0)
                    {
                        supportedPidList.Add(currentPid);
                    }
                    currentPid++;
                }
            }
        }

        /// <summary>
        /// Take the given PID result and convert it in to a byte array.
        /// Remove infomation about the requested mode and PID.
        /// </summary>
        /// <param name="pidReturn">Result of a PID.</param>
        /// <returns>Byte array result of the PID.</returns>
        private static PidResult GetPidResult(string pidReturn)
        {
            var stringBytes = pidReturn.Split(' ');
            var length = int.Parse(stringBytes[1], NumberStyles.HexNumber);
            const int headerChunks = 2;
            const int messageLabel = 2;

            var header = new ObdHeader(stringBytes[0], int.Parse(stringBytes[1], NumberStyles.HexNumber),
                                       int.Parse(stringBytes[2], NumberStyles.HexNumber),
                                       int.Parse(stringBytes[3], NumberStyles.HexNumber));

            var bytes = new byte[length - messageLabel];
            const int combinedHeader = headerChunks + messageLabel;

            for (var index = (combinedHeader); index < (length + headerChunks); index++)
            {
                bytes[index - combinedHeader] = byte.Parse(stringBytes[index].Substring(0, 2), NumberStyles.HexNumber);
            }

            return new PidResult(header, bytes);
        }

        /// <summary>
        /// Get the PIDs supported by Mode 1. 
        /// </summary>
        private void GetSupportedCurrentDataPids()
        {
            var currentPid = 1;

            supportedPids.Add(0x00);
            GetSupportedPidListForRange(supportedPids, CurrentDataPid.SupportedPids1To20, ref currentPid);

            if (supportedPids.Contains(0x20))
            {
                GetSupportedPidListForRange(supportedPids, CurrentDataPid.SupportedPids21To40, ref currentPid);
            }
            if (supportedPids.Contains(0x40))
            {
                GetSupportedPidListForRange(supportedPids, CurrentDataPid.SupportedPids41To60, ref currentPid);
            }
            if (supportedPids.Contains(0x60))
            {
                GetSupportedPidListForRange(supportedPids, CurrentDataPid.SupportedPids61To80, ref currentPid);
            }
            if (supportedPids.Contains(0x80))
            {
                GetSupportedPidListForRange(supportedPids, CurrentDataPid.SupportedPids81ToA0, ref currentPid);
            }
        }

        /// <summary>
        /// Send the given Mode 1 PID.
        /// </summary>
        /// <param name="pid">The PID to send.</param>
        /// <returns>The string returned from the ELM327.</returns>
        /// <exception cref="UnsupportedPidException">Thrown when the given PID is not supported by the car.</exception>
        private string SendPid(CurrentDataPid pid)
        {
            if (supportedPids.Contains((int)pid))
            {
                return SendPid(Mode.CurrentData, (int) pid);
            }
            else
            {
                throw new UnsupportedPidException((int) Mode.CurrentData, (int) pid);
            }
        }

        /// <summary>
        /// Send the given mode and pid.
        /// </summary>
        /// <param name="mode">The mode of the PID.</param>
        /// <param name="pid">The PID to send.</param>
        /// <returns>The string returned from the ELM327</returns>
        private string SendPid(Mode mode, int pid)
        {
            var command = ((int)mode).ToString("X2") + " " + pid.ToString("X2");
            serialPort.Write(command + "\r\n");

            var handler = CommandSent;
            if (handler != null)
            {
                handler(this, new CommandSentEventArgs(command));
            }

            return HandleResult();
        }

        /// <summary>
        /// Send an AT command to the device.
        /// </summary>
        /// <param name="command">Command to send. Does not include the "AT" portion of the command.</param>
        private string SendAtCommand(string command)
        {
            var qualifiedCommand = "AT" + command + "\r\n";

            serialPort.Write(qualifiedCommand);

            var handler = CommandSent;
            if(handler != null)
            {
                handler(this, new CommandSentEventArgs(qualifiedCommand));
            }

            return HandleResult();
        }

        /// <summary>
        /// Handle the results of a command sent to the ELM device.
        /// </summary>
        private string HandleResult()
        {
            var result = serialPort.ReadTo(">").TrimStart('\r').TrimEnd(' ', '\r', '\n');

            var handler = CommandResultReceived;
            if(handler != null)
            {
                handler(this, new CommandResultReceivedEventArgs(result));
            }
            return result;
        }

        #endregion

        #region Finalizer

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~Elm327()
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
                if(serialPort != null)
                {
                   serialPort.Dispose();
                }
            }
        }

        #endregion
    }

}

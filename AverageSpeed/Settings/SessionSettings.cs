namespace AverageSpeed.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Class for storing application settings.
    /// </summary>
    [Serializable]
    public class SessionSettings
    {
        /// <summary>
        /// The target time for the session.
        /// </summary>
        public TimeSpan TargetTime { get; set; }

        /// <summary>
        /// The target distance for the session.
        /// </summary>
        public decimal TargetDistance { get; set; }

        /// <summary>
        /// The target speed for the session.
        /// </summary>
        public double TargetSpeed
        {
            get
            {
                if (TargetTime.TotalHours != 0)
                {
                    return ((double) TargetDistance) / TargetTime.TotalHours;
                }

                return 0;
            }
        }


        /// <summary>
        /// Mile posts for the session.
        /// </summary>
        public IList<MilePostSetting> MilePosts { get; set; }

        /// <summary>
        /// The port for the session.
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Construct a settings instance with the default settings.
        /// </summary>
        public SessionSettings()
        {
            TargetTime = new TimeSpan(0, 0, 49, 5, 454);
            MilePosts = new List<MilePostSetting>();
        }

        /// <summary>
        /// Write the settings to the given file.
        /// </summary>
        /// <param name="fileName">File to write the settings to.</param>
        /// <returns>True if the setting were written.</returns>
        public void WriteSettings(string fileName)
        {
            using (var stream = File.OpenWrite(fileName))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Load settings from the given path.
        /// </summary>
        /// <param name="fileName">The file to load settings from.</param>
        /// <returns>The loaded settings. Null if the file could not be found.</returns>
        public static SessionSettings LoadSettings(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                var formatter = new BinaryFormatter();
                var settings = (SessionSettings) formatter.Deserialize(stream);
                if(settings.MilePosts == null)
                {
                    settings.MilePosts = new List<MilePostSetting>();
                }
                return settings;
            }
        }
    }
}

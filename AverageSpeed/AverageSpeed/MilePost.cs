namespace AverageSpeed.AverageSpeed
{
    using System;
    using System.Globalization;
    using Settings;

    /// <summary>
    /// Class which contains information about a mile post.
    /// </summary>
    public class MilePost
    {
        #region Public Properties

        /// <summary>
        /// Static information about the mile post. It does not change during the session.
        /// </summary>
        public MilePostSetting Settings { private set; get; }

        /// <summary>
        /// Time from the session start to when the milepost should be passed.
        /// </summary>
        public TimeSpan TargetTime { private set; get; }

        /// <summary>
        /// Indicates if the user has marked this mile post as passed.
        /// </summary>
        public bool Passed { private set; get; }

        /// <summary>
        /// The amount of time that the passing of the milepose deviated from the target.
        /// </summary>
        public TimeSpan Deviation { private set; get; }

        /// <summary>
        /// Average speed based on the time the mile post was passed.
        /// </summary>
        public double Speed { private set; get; }

        /// <summary>
        /// Flag indicating if the time passed was close to the target time.
        /// </summary>
        public SpeedStatus OnTarget
        {
            get
            {
                if (Math.Abs(Deviation.TotalSeconds) < OnTargetDeviation)
                {
                    return SpeedStatus.OnTarget;
                }
                else if (Deviation.TotalSeconds <= -OnTargetDeviation)
                {
                    return SpeedStatus.UnderTarget;
                }
                else
                {
                    return SpeedStatus.OverTarget;
                }
            }
        }

        /// <summary>
        /// Unique ID for this mile post.
        /// </summary>
        public Guid Id { private set; get; }

        /// <summary>
        /// Odometer reading, in miles, when the mile post was passed.
        /// </summary>
        public double Odometer { private set; get; }

        #endregion

        #region Private Fields

        /// <summary>
        /// Format string for time display.
        /// </summary>
        private const string TimerFormatString = "{0}:{1}:{2}:{3}";

        /// <summary>
        /// Format string for performing a ToString.
        /// </summary>
        private const string ToStringFormat = "{0,-20}|{1,-100}|{2,-10}|{3,-16}|{4,-16}|{5,-10}|{6,-10}";

        /// <summary>
        /// Constant used for determining the on-target status.
        /// </summary>
        private const double OnTargetDeviation = 5;

        #endregion

        #region Constructor

        /// <summary>
        /// Construct an instance of the mile post.
        /// </summary>
        /// <param name="settings">Settings for the post.</param>
        /// <param name="speed">Target speed. Used to calculate the target time.</param>
        public MilePost(MilePostSetting settings, double speed)
        {
            Settings = settings;
            if (speed != 0)
            {
                var time = settings.Mile / speed;
                TargetTime = TimeSpan.FromHours(time);
            }
            
            Id = Guid.NewGuid();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Indicate that the mile post has been passed.
        /// </summary>
        /// <param name="ellapsedTime">Time the mile post was passed.</param>
        /// <param name="odometer">Distance traveled, in miles, when the milepost was passed.</param>
        public void Pass(TimeSpan ellapsedTime, double odometer)
        {
            Passed = true;
            Deviation = TargetTime - ellapsedTime;
            Speed = Settings.Mile / ellapsedTime.TotalHours;
            Odometer = odometer;
        }

        /// <summary>
        /// Reset the mile post to indicate it has not been passed.
        /// </summary>
        public void Reset()
        {
            Deviation = new TimeSpan();
            Passed = false;
            Speed = 0;
            Odometer = 0;
        }

        /// <summary>
        /// Overridden to provide better information.
        /// </summary>
        /// <returns>String representation of the object.</returns>
        public override string ToString()
        {
            var targetTimeString = string.Format(CultureInfo.InvariantCulture, TimerFormatString,
                                                 TargetTime.Hours.ToString("D2"),
                                                 TargetTime.Minutes.ToString("D2"),
                                                 TargetTime.Seconds.ToString("D2"),
                                                 TargetTime.Milliseconds.ToString("D3"));

            var deviationString = string.Format(CultureInfo.InvariantCulture, TimerFormatString,
                                                Deviation.Hours.ToString("D2"),
                                                Deviation.Minutes.ToString("D2"),
                                                Deviation.Seconds.ToString("D2"),
                                                Deviation.Milliseconds.ToString("D3"));

            return string.Format(CultureInfo.InvariantCulture, ToStringFormat,
                                 Settings.Name,
                                 Settings.Description, Settings.Mile, targetTimeString, deviationString,
                                 Odometer.ToString("F2"), Speed.ToString("F2"));
        }

        #endregion
    }
}

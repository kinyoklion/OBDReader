namespace AverageSpeed.AverageSpeed
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using OBDReader.Obd2;
    using Settings;

    /// <summary>
    /// Class for tracking average speed using OBD II.
    /// </summary>
    class AverageSpeedModel : IDisposable
    {
        #region Enumerations

        /// <summary>
        /// Enumeration which contains the different possible states of the model.
        /// </summary>
        private enum CollectionState
        {
            Idle,
            Ready,
            Collecting
        }

        #endregion

        #region Events

        /// <summary>
        /// Event fired when a mile post is updated.
        /// </summary>
        public EventHandler<MilePostUpdatedEventArgs> MilePostUpdated;

        /// <summary>
        /// Event fired when the mile post list is changed.
        /// Anyone using the list should get a fresh copy.
        /// </summary>
        public EventHandler<EventArgs> MilePostListChanged;

        /// <summary>
        /// Event fired when the model is disconnected.
        /// </summary>
        public EventHandler<EventArgs> Disconnected;

        #endregion

        #region Private Fields

        /// <summary>
        /// Class used to collect OBD information.
        /// </summary>
        private Elm327 elm;

        /// <summary>
        /// Stopwatch used to track the interval for the average speed.
        /// </summary>
        private readonly Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// Current state of the model.
        /// </summary>
        private CollectionState state;

        /// <summary>
        /// Dictionary containing handlers for the different states.
        /// </summary>
        private Dictionary<CollectionState, Action> stateHandlers;

        /// <summary>
        /// Contains the current commandLog entries.
        /// </summary>
        private List<string> commandLog = new List<string>();

        /// <summary>
        /// The last speed recorded.
        /// </summary>
        private int lastSpeed;

        /// <summary>
        /// The last time a speed was sampled.
        /// </summary>
        private long lastTime;

        /// <summary>
        /// A list of mile posts and their current status.
        /// </summary>
        private List<MilePost> milePosts = new List<MilePost>();

        /// <summary>
        /// Constant for converting between kilometers and miles.
        /// </summary>
        private const double MilesPerKilometer = 0.621371192;

        /// <summary>
        /// StreamWriter used to log session information.
        /// </summary>
        private StreamWriter sessionLog;

        /// <summary>
        /// Deviation, in kilometers, used to base the on-target status.
        /// </summary>
        private const double OnTargetDeviation = 8;

        #endregion

        #region Public Properties

        /// <summary>
        /// Flag which indicates if there is an active OBD II connection.
        /// </summary>
        public bool Connected { private set; get; }

        /// <summary>
        /// The distance traveled since the average speed has been tracked.
        /// </summary>
        public double Distance { private set; get; }

        /// <summary>
        /// The distance traveled since the average speed has been tracked in miles.
        /// </summary>
        public double DistanceMiles { get { return ConvertKilometersToMiles(Distance); } }

        /// <summary>
        /// The calculated average speed.
        /// </summary>
        public double AverageSpeed { private set; get; }

        /// <summary>
        /// The calculated average speed in miles per hour.
        /// </summary>
        public double AverageSpeedMph { get { return ConvertKilometersToMiles(AverageSpeed); } }

        /// <summary>
        /// Flag which indicates if the average speed is being followed.
        /// </summary>
        public SpeedStatus AverageSpeedOnTarget { private set; get; }

        /// <summary>
        /// Get the time which has elapsed so far.
        /// </summary>
        public TimeSpan ElapsedTime { get { return stopwatch.Elapsed; } }

        /// <summary>
        /// Gets/Sets the target time.
        /// </summary>
        public TimeSpan TargetTime { get; set; }

        /// <summary>
        /// Gets the time remaining for the session.
        /// </summary>
        public TimeSpan RemainingTime { get { return TargetTime - ElapsedTime; } }

        /// <summary>
        /// The current instantaneous speed.
        /// </summary>
        public int Speed { private set; get; }

        /// <summary>
        /// The current instantaneous speed in miles per hour.
        /// </summary>
        public double SpeedMph { get { return ConvertKilometersToMiles(Speed); } }

        /// <summary>
        /// The target speed for the session.
        /// </summary>
        public double TargetSpeed { get { return ConvertMilesToKilometers(TargetSpeedMph); } }

        /// <summary>
        /// The target speed for the session in miles per hour.
        /// </summary>
        public double TargetSpeedMph { set; get; }

        /// <summary>
        /// Gets a list of the mile posts.
        /// </summary>
        public ReadOnlyCollection<MilePost> MilePosts { get { return new ReadOnlyCollection<MilePost>(milePosts); } }

        /// <summary>
        /// The port to connect to.
        /// </summary>
        public string Port { set; get; }

        /// <summary>
        /// Gets/Sets a flag which determines if the session should be logged or not.
        /// </summary>
        public bool LogSession { set; get; }

        /// <summary>
        /// The peak speed for the session.
        /// </summary>
        public double PeakSpeed { private set; get; }

        /// <summary>
        /// The peak speed for the session in miles per hour.
        /// </summary>
        public double PeakSpeedMph { get { return ConvertKilometersToMiles(PeakSpeed); } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get the logged items since the last time GetCommandLog was called.
        /// </summary>
        public IEnumerable<string> GetCommandLog()
        {
            var returnLog = commandLog;
            commandLog = new List<string>();
            return returnLog;       
        }

        /// <summary>
        /// Connect to the OBD II device.
        /// </summary>
        /// <param name="baudRate">The baud rate for the connection.</param>
        /// <param name="protocol">The protocol used by the car.</param>
        public void Connect(int baudRate, Protocol protocol)
        {
            stateHandlers = new Dictionary<CollectionState, Action>
                                {
                                    {CollectionState.Idle, HandleIdle},
                                    {CollectionState.Ready, HandleReady},
                                    {CollectionState.Collecting, HandleCollecting}
                                };

            elm = new Elm327(Port, baudRate, protocol);
            elm.CommandSent += (s, e) => commandLog.Add(e.Command);
            elm.CommandResultReceived += (s, e) => commandLog.Add(e.Result);
            elm.Connect();

            Connected = true;
            state = CollectionState.Idle;
        }

        /// <summary>
        /// Disconnect from the OBD II device.
        /// </summary>
        public void Disconnect()
        {
            if(state != CollectionState.Idle)
            {
                Stop();
            }
            if(elm != null)
            {
                elm.Disconnect();
            }

            Connected = false;
            var handler = Disconnected;
            if(handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Indicate that the user is ready to start a session. The average speed
        /// will start to be calculated once the vehicle is in motion.
        /// </summary>
        public void Ready()
        {
            PrepareSession();
            state = CollectionState.Ready;
        }

        /// <summary>
        /// Start a session. Unlike "Ready" this this will immediately start data collection.
        /// </summary>
        public void Start()
        {
            PrepareSession();
            var speed = elm.GetSpeed();
            StartCollecting(speed);
        }

        /// <summary>
        /// Stop collecting the average speed.
        /// </summary>
        public void Stop()
        {
            StopSessionLog();
            state = CollectionState.Idle;
            stopwatch.Stop();
        }

        /// <summary>
        /// Function which instructs the model to update.
        /// </summary>
        public void Update()
        {
            if(Connected)
            {
                stateHandlers[state]();
            }
        }

        /// <summary>
        /// Set the mile posts for the session.
        /// </summary>
        /// <param name="milePostSettings">A list of mile post settings.</param>
        /// <remarks>Target average speed should be set before calling this function.</remarks>
        public void SetMilePosts(IEnumerable<MilePostSetting> milePostSettings)
        {
            milePosts = (from setting in milePostSettings select new MilePost(setting, TargetSpeedMph)).ToList();

            FireMilePostListChanged();
        }

        /// <summary>
        /// Set the given post as having been passed.
        /// </summary>
        /// <param name="id">The unique ID of the post passed.</param>
        public void MarkMilePostPassed(Guid id)
        {
            //Only mark posts if data is being collected.
            if (state == CollectionState.Collecting)
            {
                var milePost = milePosts.Find(post => post.Id == id);
                if (milePost != null)
                {
                    milePost.Pass(ElapsedTime, DistanceMiles);
                    Log(milePost);

                    var handler = MilePostUpdated;
                    if (handler != null)
                    {
                        handler(this, new MilePostUpdatedEventArgs(milePost));
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handler for the Ready state.
        /// </summary>
        private void HandleReady()
        {
            var speed = elm.GetSpeed();

            if(speed != 0)
            {
                StartCollecting(speed);
            }
        }

        /// <summary>
        /// Start collecting data.
        /// </summary>
        /// <param name="speed">The speed at the time collecting is initiated.</param>
        private void StartCollecting(byte speed)
        {
            lastSpeed = speed;
            state = CollectionState.Collecting;
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Handler for the collecting state.
        /// </summary>
        private void HandleCollecting()
        {
            Speed = elm.GetSpeed();

            if(Speed > PeakSpeed)
            {
                PeakSpeed = Speed;
            }

            var time = stopwatch.ElapsedMilliseconds;

            //Integrate the speed in order to determine the distance traveled.
            var equation = new LineEquation(lastTime, lastSpeed, time, Speed);
            Distance += Integration.Integrate(equation.Calculate, lastTime, time, 1000) / (1000 * 60 * 60);
            
            //Calculate the speed without integration.
            //double calcElapsed = time - lastTime;
            //Distance += speed * (calcElapsed / (1000 * 60 * 60));

            var kmPerSecond = Distance / (time / 1000d);
            AverageSpeed = kmPerSecond * 60 * 60;
            CheckAverageSpeed();

            lastTime = time;
            lastSpeed = Speed;
        }

        /// <summary>
        /// Check the average speed and determine if it is on track.
        /// </summary>
        private void CheckAverageSpeed()
        {
            var difference = TargetSpeed - AverageSpeed;

            if (Math.Abs(difference) < OnTargetDeviation)
            {
                AverageSpeedOnTarget = SpeedStatus.OnTarget;
            }
            else if (difference >= OnTargetDeviation)
            {
                AverageSpeedOnTarget = SpeedStatus.UnderTarget;
            }
            else
            {
                AverageSpeedOnTarget = SpeedStatus.OverTarget;
            }
        }

        /// <summary>
        /// Handler for the idle state.
        /// </summary>
        private static void HandleIdle()
        {
            //No idle behavior.
        }

        /// <summary>
        /// Convert the given kilometers to miles.
        /// </summary>
        /// <param name="kilometers">Kilometers to convert.</param>
        /// <returns>Number of miles for the given kilometers.</returns>
        private static double ConvertKilometersToMiles(double kilometers)
        {
            return kilometers * MilesPerKilometer;
        }

        /// <summary>
        /// Convert the given kilometers to miles.
        /// </summary>
        /// <param name="kilometers">Kilometers to convert.</param>
        /// <returns>Number of miles for the given kilometers.</returns>
        private static double ConvertMilesToKilometers(double kilometers)
        {
            return kilometers / MilesPerKilometer;
        }

        /// <summary>
        /// Fire the mile post changed event.
        /// </summary>
        private void FireMilePostListChanged()
        {
            var handler = MilePostListChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Start logging the session.
        /// </summary>
        private void StartSessionLog()
        {
            if (LogSession)
            {
                var fileName = "Session" + DateTime.Now.ToString("o").Replace(':', '_').Replace('.', '_') +
                               ".txt";

                sessionLog = new StreamWriter(fileName);
            }
        }

        /// <summary>
        /// Stop logging the session.
        /// </summary>
        private void StopSessionLog()
        {
            if(sessionLog != null)
            {
                sessionLog.Close();
                sessionLog = null;
            }
        }

        /// <summary>
        /// Log to the session log.
        /// </summary>
        /// <param name="logItem">Item to log.</param>
        private void Log(object logItem)
        {
            if (LogSession && sessionLog != null && state == CollectionState.Collecting)
            {
                sessionLog.WriteLine(logItem.ToString());
                sessionLog.Flush();
            }
        }

        /// <summary>
        /// Prepare for a new session.
        /// </summary>
        private void PrepareSession()
        {
            lastTime = 0;
            lastSpeed = 0;
            Distance = 0;
            AverageSpeed = 0;
            PeakSpeed = 0;
            stopwatch.Stop();
            stopwatch.Reset();
            milePosts.ForEach(post => post.Reset());
            FireMilePostListChanged();
            StartSessionLog();
        }

        #endregion

        #region Finalizer

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~AverageSpeedModel()
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
            if(disposing)
            {
                if(sessionLog != null)
                {
                    sessionLog.Close();
                    sessionLog.Dispose();
                }
                if(elm != null)
                {
                    elm.Dispose();
                }
            }
        }

        #endregion
    }
}

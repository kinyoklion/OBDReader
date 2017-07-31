namespace AverageSpeed.AverageSpeed
{
    /// <summary>
    /// Class which represents the equation of a line.
    /// </summary>
    public class LineEquation
    {
        /// <summary>
        /// Slope of the line.
        /// </summary>
        private readonly double m;

        /// <summary>
        /// The y-intercept of the line.
        /// </summary>
        private readonly double b;

        /// <summary>
        /// Create an instance of the equation.
        /// </summary>
        /// <param name="time1">Time of first sample.</param>
        /// <param name="speed1">Speed at first sample.</param>
        /// <param name="time2">Time of second sample.</param>
        /// <param name="speed2">Speed at second sample.</param>
        public LineEquation(double time1, double speed1, double time2, double speed2)
        {
            m = (speed2 - speed1) / (time2 - time1);
            b = speed1 - (m * time1);
        }

        /// <summary>
        /// Calculate the speed at the given time.
        /// </summary>
        /// <param name="time">The time to calculate the speed for.</param>
        /// <returns>The speed at the specified time.</returns>
        public double Calculate(double time)
        {
            return (m * time) + b;
        }
    }
}

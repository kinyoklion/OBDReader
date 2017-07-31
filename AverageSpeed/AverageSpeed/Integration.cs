namespace AverageSpeed.AverageSpeed
{
    using System;

    /// <summary>
    /// CLass for performing quantized integration.
    /// </summary>
    public static class Integration
    {
        /// <summary>
        /// Integrate the given function over the specified interval with the given number of steps.
        /// </summary>
        /// <param name="function">Function to integrate.</param>
        /// <param name="start">Start value for integration.</param>
        /// <param name="end">End value for integration.</param>
        /// <param name="steps">The number of steps to integrate over.</param>
        /// <returns>The result of the integration.</returns>
        public static double Integrate(Func<double, double> function, double start, double end, int steps)
        {
            var interval = (end - start) / steps;
            var result = 0d;

            for (var step = 0; step < steps; step++)
            {
                var speedT = function(start + (step * interval));
                result += speedT * interval;
            }

            return result;
        }
    }
}

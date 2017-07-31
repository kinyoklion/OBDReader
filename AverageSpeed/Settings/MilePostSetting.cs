namespace AverageSpeed.Settings
{
    using System;

    /// <summary>
    /// Class which stores information about a mile post.
    /// </summary>
    [Serializable]
    public class MilePostSetting
    {
        /// <summary>
        /// The name of the mile post.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the mile post.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The distance of the mile post.
        /// </summary>
        public double Mile { get; set; }
    }
}

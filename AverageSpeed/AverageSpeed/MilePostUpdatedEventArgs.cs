namespace AverageSpeed.AverageSpeed
{
    using System;

    /// <summary>
    /// Event arguments containing information about a mile post change.
    /// </summary>
    public class MilePostUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// The mile post which was updated.
        /// </summary>
        public MilePost Post { private set; get; }

        /// <summary>
        /// Construct an instance of the event arguments.
        /// </summary>
        /// <param name="post">The mile post which was updated.</param>
        public MilePostUpdatedEventArgs(MilePost post)
        {
            Post = post;
        }
    }
}

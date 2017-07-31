namespace AverageSpeed.AverageSpeed
{
    using System;

    /// <summary>
    /// Event arguments for marking a mile post in the view.
    /// </summary>
    public class MarkPostEventArgs : EventArgs
    {
        /// <summary>
        /// The unique identifer of the mile post.
        /// </summary>
        public Guid MilePostId { private set; get; }

        /// <summary>
        /// Construct an instance of the event arguments.
        /// </summary>
        /// <param name="milePostId">The unique identifer of the milepost.</param>
        public MarkPostEventArgs(Guid milePostId)
        {
            MilePostId = milePostId;
        }
    }
}

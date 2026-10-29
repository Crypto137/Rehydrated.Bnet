namespace Rehydrated.Bnet.Jobs
{
    /// <summary>
    /// Abstract class for jobs that can be executed on a dedicated thread.
    /// </summary>
    internal abstract class Job
    {
        private static uint _currentJobId = 0;

        /// <summary>
        /// The sequential id of this <see cref="Job"/>.
        /// </summary>
        public uint JobId { get; } = Interlocked.Increment(ref _currentJobId);

        /// <summary>
        /// Executes this <see cref="Job"/>.
        /// </summary>
        public abstract void Execute();
    }
}

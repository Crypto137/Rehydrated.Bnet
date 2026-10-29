using NLog;
using Rehydrated.Bnet.Rpc;
using System.Collections.Concurrent;
using System.Globalization;

namespace Rehydrated.Bnet.Jobs
{
    /// <summary>
    /// Producer-consumer implementation for executing <see cref="Job"/> instances on dedicated worker threads.
    /// </summary>
    internal class JobQueue
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly BlockingCollection<Job> _jobs = new();

        private Thread _workerThread;
        private bool _isRunning;

        public JobQueue() { }

        public bool Initialize()
        {
            // Currently we use a single worker thread to process all jobs to avoid multithreading weirdness.
            _workerThread = new(ProcessQueue) { Name = "JobQueue", IsBackground = true, CurrentCulture = CultureInfo.InvariantCulture };
            _workerThread.Start();

            return true;
        }

        /// <summary>
        /// Enqueues an <see cref="Job"/> to be executed on worker threads.
        /// </summary>
        public void EnqueueJob(Job job)
        {
            _jobs.Add(job);
        }

        /// <summary>
        /// Process enqueued <see cref="Job"/> instances in a loop.
        /// </summary>
        private void ProcessQueue()
        {
            Logger.Info("Started processing jobs");
            _isRunning = true;

            while (_isRunning)
            {
                Job job = _jobs.Take();
                //Logger.Debug($"ProcessQueue(): {job}");

                try
                {
                    job.Execute();
                }
                catch (RpcException e)
                {
                    // This is sorta kinda valid for when we are lazy (e.g. we haven't implemented a callback yet).
                    Logger.Warn($"ProcessQueue(): RPC exception while executing [{job}]\n{e}");
                }
                catch (Exception e)
                {
                    Logger.Error($"ProcessQueue(): Unhandled exception while executing [{job}]\n{e}");
                }
            }
        }
    }
}

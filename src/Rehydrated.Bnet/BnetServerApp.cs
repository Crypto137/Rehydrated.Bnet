using NLog;
using Rehydrated.Bnet.Jobs;
using Rehydrated.Bnet.Network;
using Rehydrated.Bnet.Services;

namespace Rehydrated.Bnet
{
    internal class BnetServerApp
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private bool _isRunning = false;

        public ServiceManager ServiceManager { get; } = new();
        public JobQueue JobQueue { get; } = new();
        public BnetServer BnetServer { get; } = new();

        public BnetServerApp() { }

        public static BnetServerApp Instance { get; private set; }

        public void Run()
        {
            if (_isRunning)
                return;

            _isRunning = true;
            Instance = this;

            Logger.Info("Starting Rehydrated.Bnet...");

            ServiceManager.Initialize();
            JobQueue.Initialize();
            BnetServer.Start();

            while (_isRunning)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                    continue;

                if (string.Equals(input, "stop", StringComparison.OrdinalIgnoreCase))
                    _isRunning = false;
            }

            BnetServer.Stop();

            Logger.Info("Rehydrated.Bnet finished running");
        }
    }
}

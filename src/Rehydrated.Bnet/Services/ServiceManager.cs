using Google.ProtocolBuffers.Descriptors;
using NLog;
using Rehydrated.Bnet.Utils;
using System.Reflection;

namespace Rehydrated.Bnet.Services
{
    internal enum ServiceId : sbyte
    {
        ServiceReply = -2,
        ConnectionService = 0,      // 0x65446991
        AccountService,             // 0x62DA0891
        AchievementsService,        // 0x43487D3D
        AuthenticationServer,       // 0x0DECFC01
        ChallengeService,           // 0xDBBF6F19
        ChannelInvitationService,   // 0x83040608
        Channel,                    // 0xB732DB32
        ChannelOwner,               // 0x060CA08D
        ExchangeService,            // 0xD750148B
        FriendsService,             // 0xA3DDB1BD
        GameMaster,                 // 0x810CB195
        GameUtilities,              // 0x3FC1274D
        NotificationService,        // 0x0CBE3C43
        PresenceService,            // 0xFA0796FF
        ReportService,              // 0x7CAF61C9
        Resources,                  // 0xECBE75BA
        SearchService,              // 0x0A24A291
        UserManagerService,         // 0x3E19268A
        //ServerPoolService,        // 0x67960C70 (no bind request from the client)
    }

    internal class ServiceManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<ServiceId, IBnetService> _servicesById = new();
        private readonly Dictionary<uint, ServiceId> _servicesByHash = new();

        public ServiceManager() { }

        /// <summary>
        /// Constructs and registers <see cref="IBnetService"/> implementations.
        /// </summary>
        public bool Initialize()
        {
            int serviceId = 1;

            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (type.GetInterface(nameof(IBnetService)) == null)
                    continue;

                IBnetService service = (IBnetService)Activator.CreateInstance(type);

                ServiceDescriptor serviceDescriptor = service.DescriptorForType;
                uint serviceHash = HashUtility.Fnv1a(serviceDescriptor.FullName);

                if (serviceHash != 0x65446991)  // ConnectionService
                    service.ServiceId = (ServiceId)serviceId++;

                _servicesById.Add(service.ServiceId, service);
                _servicesByHash.Add(serviceHash, service.ServiceId);

                Logger.Info($"Registered server service [{serviceDescriptor.Name} (0x{serviceHash:X8})]");
            }

            return true;
        }

        /// <summary>
        /// Returns the registered <see cref="IBnetService"/> for the specified <see cref="ServiceId"/>. Returns <see langword="null"/> if no service found.
        /// </summary>
        public IBnetService GetServerService(ServiceId serviceId)
        {
            if (_servicesById.TryGetValue(serviceId, out IBnetService service) == false)
            {
                Logger.Warn($"GetService(): No service registered for id {serviceId}");
                return null;
            }

            return service;
        }

        /// <summary>
        /// Returns the registered <see cref="ServiceId"/> for the specified hash. Returns <see langword="null"/> if no service found.
        /// </summary>
        public ServiceId? GetServiceId(uint serviceHash)
        {
            if (_servicesByHash.TryGetValue(serviceHash, out ServiceId serviceId) == false)
            {
                Logger.Warn($"GetService(): No service registered for hash 0x{serviceHash:X8}");
                return null;
            }

            return serviceId;
        }
    }
}

using bnet.protocol.account;
using bnet.protocol.achievements;
using bnet.protocol.authentication;
using bnet.protocol.challenge;
using bnet.protocol.channel;
using bnet.protocol.channel_invitation;
using bnet.protocol.connection;
using bnet.protocol.diag;
using bnet.protocol.exchange;
using bnet.protocol.friends;
using bnet.protocol.notification;
using bnet.protocol.user_manager;
using Google.ProtocolBuffers;

namespace Rehydrated.Bnet.Services
{
    /// <summary>
    /// Collection of service stubs for client services.
    /// </summary>
    internal class ClientServiceStubCollection
    {
        private static readonly HashSet<uint> _knownClientServices = new()
        {
            0x65446991, 0x54DFDA17, 0xD4DCD093, 0x71240E35,
            0xBBDA171F, 0xF084FC20, 0xBF8C8094, 0x166FE4A1,
            0xB96F5297, 0x6F259A13, 0xE1CB2EA8, 0xBC872C22,
        };

        // not exported by the client
        // GameFactorySubscriber 0xC6F9CCC5
        // GameMasterSubscriber 0x5772AB33

        public ConnectionService.Stub ConnectionService { get; }                // 0x65446991 0
        public AccountNotify.Stub AccountNotify { get; }                        // 0x54DFDA17 1
        public AchievementsNotify.Stub AchievementsNotify { get; }              // 0xD4DCD093 2
        public AuthenticationClient.Stub AuthenticationClient { get; }          // 0x71240E35 3
        public ChallengeNotify.Stub ChallengeNotify { get; }                    // 0xBBDA171F 4
        public ChannelInvitationNotify.Stub ChannelInvitationNotify { get; }    // 0xF084FC20 5
        public ChannelSubscriber.Stub ChannelSubscriber { get; }                // 0xBF8C80946 6, bind request included 3 times
        public ExchangeNotify.Stub ExchangeNotify { get; }                      // 0x166FE4A1 7
        public DiagService.Stub DiagService { get; }                            // 0xB96F5297 8
        public FriendsNotify.Stub FriendsNotify { get; }                        // 0x6F259A13 9
        public NotificationListener.Stub NotificationListener { get; }          // 0xE1CB2EA8 10
        public UserManagerNotify.Stub UserManagerNotify { get; }                // 0xBC872C22 11

        /// <summary>
        /// Constructs a <see cref="ClientServiceStubCollection"/> for the provided <see cref="IRpcChannel"/>.
        /// </summary>
        public ClientServiceStubCollection(IRpcChannel channel)
        {
            ConnectionService = bnet.protocol.connection.ConnectionService.CreateStub(channel);
            AccountNotify = bnet.protocol.account.AccountNotify.CreateStub(channel);
            AchievementsNotify = bnet.protocol.achievements.AchievementsNotify.CreateStub(channel);
            AuthenticationClient = bnet.protocol.authentication.AuthenticationClient.CreateStub(channel);
            ChallengeNotify = bnet.protocol.challenge.ChallengeNotify.CreateStub(channel);
            ChannelInvitationNotify = bnet.protocol.channel_invitation.ChannelInvitationNotify.CreateStub(channel);
            ChannelSubscriber = bnet.protocol.channel.ChannelSubscriber.CreateStub(channel);
            ExchangeNotify = bnet.protocol.exchange.ExchangeNotify.CreateStub(channel);
            DiagService = bnet.protocol.diag.DiagService.CreateStub(channel);
            FriendsNotify = bnet.protocol.friends.FriendsNotify.CreateStub(channel);
            NotificationListener = bnet.protocol.notification.NotificationListener.CreateStub(channel);
            UserManagerNotify = bnet.protocol.user_manager.UserManagerNotify.CreateStub(channel);
        }

        public static bool IsKnownClientService(uint serviceHash)
        {
            return _knownClientServices.Contains(serviceHash);
        }
    }
}

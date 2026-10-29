using bnet.protocol;
using bnet.protocol.channel;
using bnet.protocol.presence;
using Google.ProtocolBuffers;
using NLog;
using Rehydrated.Bnet.Extensions;
using Rehydrated.Bnet.Network;
using Rehydrated.Bnet.Rpc;
using Rehydrated.Bnet.Utils;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class PresenceServiceImpl : PresenceService, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public override void Subscribe(IRpcController controller, SubscribeRequest request, Action<NoData> done)
        {
            Logger.Debug($"Subscribe()\n{request}");

            done(NoData.DefaultInstance);

            List<Field> fields = new();

            switch (request.EntityId.High)
            {
                case 0x0100000000000000:    // Account
                    // RealIDTag
                    string realIdTag = "rehydratedrealid";
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.D3, FieldKeyGroup.Account, 1, 0, realIdTag));

                    // LastPlayedGameAccountId
                    EntityId lastPlayerGameAccountId = EntityId.CreateBuilder().SetHigh(0x0200000000000000).SetLow(1).Build();
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.D3, FieldKeyGroup.Account, 2, 0, lastPlayerGameAccountId.ToByteString()));

                    // GameAccountList
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.D3, FieldKeyGroup.Account, 3, 0x0200000000000000, lastPlayerGameAccountId));
                    
                    // AccountBattleTag
                    string battleTag = "Rehydrated#666";
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.D3, FieldKeyGroup.Account, 4, 0, battleTag));

                    // LastOnline
                    int lastOnline = 0;
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.D3, FieldKeyGroup.Account, 6, 0, lastOnline));

                    break;

                case 0x0200000000000000:    // GameAccount
                    // BannerConfiguration
                    D3.Account.BannerConfiguration banner = D3.Account.BannerConfiguration.CreateBuilder()
                        .SetBannerShape(0)
                        .SetSigilMain(0)
                        .SetSigilAccent(0)
                        .SetPatternColor(0)
                        .SetBackgroundColor(0)
                        .SetSigilColor(0)
                        .SetSigilPlacement(0)
                        .SetPattern(0)
                        .SetUseSigilVariant(false)
                        .Build();
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.D3, FieldKeyGroup.GameAccount, 1, 0, banner.ToByteString()));

                    // LastPlayedHeroId
                    EntityId lastPlayerHeroId = EntityId.CreateBuilder().SetHigh(0x0300000000000000).SetLow(1).Build();
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.D3, FieldKeyGroup.GameAccount, 2, 0, lastPlayerHeroId));

                    // Status
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.BNet, FieldKeyGroup.GameAccount, 1, 0, false));

                    // LastOnline
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.BNet, FieldKeyGroup.GameAccount, 4, 0, 0));

                    // BattleTag
                    battleTag = "Rehydrated#666";
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.BNet, FieldKeyGroup.GameAccount, 5, 0, battleTag));

                    // OwnerId
                    EntityId ownerId = EntityId.CreateBuilder().SetHigh(0x0100000000000000).SetLow(1).Build();
                    fields.Add(PresenceUtility.CreateField(FieldKeyProgram.BNet, FieldKeyGroup.GameAccount, 7, 0, ownerId.ToByteString()));

                    break;
            }

            if (fields.Count > 0)
            {
                BnetClient client = controller.GetClient();

                var presenceChannelState = bnet.protocol.presence.ChannelState.CreateBuilder()
                    .SetEntityId(request.EntityId)
                    .AddRangeFieldOperation(fields.Select(field => PresenceUtility.BuildFieldOperation(FieldOperation.Types.OperationType.SET, field)))
                    .Build();

                var channelChannelState = bnet.protocol.channel.ChannelState.CreateBuilder()
                    .SetExtension(bnet.protocol.presence.ChannelState.Presence, presenceChannelState)
                    .Build();

                AddNotification addNotification = AddNotification.CreateBuilder()
                    .SetChannelState(channelChannelState)
                    .Build();

                client.Services.ChannelSubscriber.NotifyAdd(new RpcContext(client, request.ObjectId), addNotification, null);
            }
        }

        public override void Unsubscribe(IRpcController controller, UnsubscribeRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void Update(IRpcController controller, UpdateRequest request, Action<NoData> done)
        {
            Logger.Debug($"Update()\n{request}");
            done(NoData.DefaultInstance);
        }

        public override void Query(IRpcController controller, QueryRequest request, Action<QueryResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void Ownership(IRpcController controller, OwnershipRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void Heal(IRpcController controller, UpdateRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void SubscribeNotification(IRpcController controller, SubscribeNotificationRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }
    }
}

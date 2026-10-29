using bnet.protocol;
using bnet.protocol.channel;
using Google.ProtocolBuffers;
using NLog;
using Rehydrated.Bnet.Extensions;
using Rehydrated.Bnet.Network;
using Rehydrated.Bnet.Rpc;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class ChannelOwnerImpl : ChannelOwner, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public override void GetChannelId(IRpcController controller, GetChannelIdRequest request, Action<GetChannelIdResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void CreateChannel(IRpcController controller, CreateChannelRequest request, Action<CreateChannelResponse> done)
        {
            Logger.Debug($"CreateChannel():\n{request}");
            done(CreateChannelResponse.CreateBuilder()
                .SetObjectId(666)
                .SetChannelId(bnet.protocol.EntityId.CreateBuilder().SetHigh(0x0600000000000000).SetLow(1))
                .Build());

            BnetClient client = controller.GetClient();

            var presenceChannelState = bnet.protocol.presence.ChannelState.CreateBuilder()
                .SetEntityId(bnet.protocol.EntityId.CreateBuilder().SetHigh(0x0600000000000000).SetLow(1))
                .Build();

            var channelChannelState = bnet.protocol.channel.ChannelState.CreateBuilder()
                .SetExtension(bnet.protocol.presence.ChannelState.Presence, presenceChannelState)
                .Build();

            AddNotification addNotification = AddNotification.CreateBuilder()
                .SetChannelState(channelChannelState)
                .SetSelf(Member.CreateBuilder()
                    .SetIdentity(Identity.CreateBuilder().SetGameAccountId(EntityId.CreateBuilder().SetHigh(0x0200000000000000).SetLow(1)))
                    .SetState(MemberState.CreateBuilder()
                        .SetPrivileges(0x0000FBFF)
                        .AddRole(1)
                        .AddRole(2)))
                .AddMember(Member.CreateBuilder()
                    .SetIdentity(Identity.CreateBuilder().SetGameAccountId(EntityId.CreateBuilder().SetHigh(0x0200000000000000).SetLow(1)))
                    .SetState(MemberState.CreateBuilder()
                        .SetPrivileges(0x0000FBFF)
                        .AddRole(1)
                        .AddRole(2)))
                .Build();

            client.Services.ChannelSubscriber.NotifyAdd(new RpcContext(client, request.ObjectId), addNotification, null);

        }

        public override void JoinChannel(IRpcController controller, JoinChannelRequest request, Action<JoinChannelResponse> done)
        {
            Logger.Debug($"JoinChannel():\n{request}");

            done(JoinChannelResponse.CreateBuilder()
               .SetObjectId(67122)
               .Build());
        }

        public override void FindChannel(IRpcController controller, FindChannelRequest request, Action<FindChannelResponse> done)
        {
            Logger.Debug($"FindChannel():\n{request}");
            //done(FindChannelResponse.DefaultInstance);
        }

        public override void GetChannelInfo(IRpcController controller, GetChannelInfoRequest request, Action<GetChannelInfoResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void SubscribeChannel(IRpcController controller, SubscribeChannelRequest request, Action<SubscribeChannelResponse> done)
        {
            throw new NotImplementedException();
        }
    }
}

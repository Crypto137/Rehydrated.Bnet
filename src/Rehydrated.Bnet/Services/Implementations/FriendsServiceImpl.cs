using bnet.protocol;
using bnet.protocol.friends;
using bnet.protocol.invitation;
using Google.ProtocolBuffers;
using NLog;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class FriendsServiceImpl : FriendsService, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public override void SubscribeToFriends(IRpcController controller, SubscribeToFriendsRequest request, Action<SubscribeToFriendsResponse> done)
        {
            Logger.Debug($"SubscribeToFriends()\n{request}");
            done(SubscribeToFriendsResponse.CreateBuilder()
                .SetMaxFriends(127)
                .SetMaxReceivedInvitations(127)
                .SetMaxSentInvitations(127)
                .AddRole(Role.CreateBuilder().SetId(1).SetName("battle_tag_friend"))
                .AddRole(Role.CreateBuilder().SetId(2).SetName("real_id_friend"))
                .Build());
        }

        public override void SendInvitation(IRpcController controller, SendInvitationRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void AcceptInvitation(IRpcController controller, GenericRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void RevokeInvitation(IRpcController controller, GenericRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void DeclineInvitation(IRpcController controller, GenericRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void IgnoreInvitation(IRpcController controller, GenericRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void AssignRole(IRpcController controller, AssignRoleRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void RemoveFriend(IRpcController controller, GenericFriendRequest request, Action<GenericFriendResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void ViewFriends(IRpcController controller, ViewFriendsRequest request, Action<ViewFriendsResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void UpdateFriendState(IRpcController controller, UpdateFriendStateRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UnsubscribeToFriends(IRpcController controller, UnsubscribeToFriendsRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void RevokeAllInvitations(IRpcController controller, GenericFriendRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }
    }
}

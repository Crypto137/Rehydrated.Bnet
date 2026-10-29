using bnet.protocol;
using bnet.protocol.channel_invitation;
using bnet.protocol.invitation;
using Google.ProtocolBuffers;
using NLog;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class ChannelInvitationServiceImpl : ChannelInvitationService, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public override void Subscribe(IRpcController controller, SubscribeRequest request, Action<SubscribeResponse> done)
        {
            Logger.Debug($"Subscribe()\n{request}");
            done(SubscribeResponse.DefaultInstance);
        }

        public override void Unsubscribe(IRpcController controller, UnsubscribeRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void SendInvitation(IRpcController controller, SendInvitationRequest request, Action<SendInvitationResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void AcceptInvitation(IRpcController controller, AcceptInvitationRequest request, Action<AcceptInvitationResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void DeclineInvitation(IRpcController controller, GenericRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void RevokeInvitation(IRpcController controller, RevokeInvitationRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void SuggestInvitation(IRpcController controller, SuggestInvitationRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void IncrementChannelCount(IRpcController controller, IncrementChannelCountRequest request, Action<IncrementChannelCountResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void DecrementChannelCount(IRpcController controller, DecrementChannelCountRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UpdateChannelCount(IRpcController controller, UpdateChannelCountRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }
    }
}

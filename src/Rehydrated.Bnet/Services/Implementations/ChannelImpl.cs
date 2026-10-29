using bnet.protocol;
using bnet.protocol.channel;
using Google.ProtocolBuffers;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class ChannelImpl : Channel, IBnetService
    {
        public ServiceId ServiceId { get; set; }

        public override void AddMember(IRpcController controller, AddMemberRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void RemoveMember(IRpcController controller, RemoveMemberRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void SendMessage(IRpcController controller, SendMessageRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UpdateChannelState(IRpcController controller, UpdateChannelStateRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UpdateMemberState(IRpcController controller, UpdateMemberStateRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void Dissolve(IRpcController controller, DissolveRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void SetRoles(IRpcController controller, SetRolesRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UnsubscribeMember(IRpcController controller, UnsubscribeMemberRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }
    }
}

using bnet.protocol;
using bnet.protocol.notification;
using Google.ProtocolBuffers;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class NotificationServiceImpl : NotificationService, IBnetService
    {
        public ServiceId ServiceId { get; set; }

        public override void SendNotification(IRpcController controller, Notification request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void RegisterClient(IRpcController controller, RegisterClientRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UnregisterClient(IRpcController controller, UnregisterClientRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void FindClient(IRpcController controller, FindClientRequest request, Action<FindClientResponse> done)
        {
            throw new NotImplementedException();
        }
    }
}

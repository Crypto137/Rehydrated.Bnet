using bnet.protocol;
using bnet.protocol.achievements;
using Google.ProtocolBuffers;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class AchievementServiceImpl : AchievementsService, IBnetService
    {
        public ServiceId ServiceId { get; set; }

        public override void PostUpdate(IRpcController controller, PostUpdateRequest request, Action<PostUpdateResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void RegisterWithService(IRpcController controller, RegisterWithServiceRequest request, Action<RegisterWithServiceResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void RequestSnapshot(IRpcController controller, RequestSnapshotRequest request, Action<RequestSnapshotResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void UnregisterFromService(IRpcController controller, UnregisterFromServiceRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void Initialize(IRpcController controller, InitializeRequest request, Action<InitializeResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void ValidateStaticData(IRpcController controller, ValidateStaticDataRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }
    }
}

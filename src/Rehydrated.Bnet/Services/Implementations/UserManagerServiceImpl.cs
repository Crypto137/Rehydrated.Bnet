using bnet.protocol;
using bnet.protocol.user_manager;
using Google.ProtocolBuffers;
using NLog;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class UserManagerServiceImpl : UserManagerService, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public override void SubscribeToUserManager(IRpcController controller, SubscribeToUserManagerRequest request, Action<SubscribeToUserManagerResponse> done)
        {
            Logger.Debug($"SubscribeToUserManager():\n{request}");
            done(SubscribeToUserManagerResponse.DefaultInstance);
        }

        public override void AddRecentPlayers(IRpcController controller, AddRecentPlayersRequest request, Action<AddRecentPlayersResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void ClearRecentPlayers(IRpcController controller, ClearRecentPlayersRequest request, Action<ClearRecentPlayersResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void BlockEntity(IRpcController controller, BlockEntityRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UnblockEntity(IRpcController controller, UnblockEntityRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void BlockEntityForSession(IRpcController controller, BlockEntityRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void LoadBlockList(IRpcController controller, EntityId request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void Unsubscribe(IRpcController controller, UnsubscribeRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }
    }
}

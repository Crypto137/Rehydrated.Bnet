using bnet.protocol;
using bnet.protocol.challenge;
using Google.ProtocolBuffers;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class ChallengeServiceImpl : ChallengeService, IBnetService
    {
        public ServiceId ServiceId { get; set; }

        public override void ChallengePicked(IRpcController controller, ChallengePickedRequest request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void ChallengeAnswered(IRpcController controller, ChallengeAnsweredRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void ChallengeCancelled(IRpcController controller, NoData request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void SendChallengeToUser(IRpcController controller, SendChallengeToUserRequest request, Action<SendChallengeToUserResponse> done)
        {
            throw new NotImplementedException();
        }
    }
}

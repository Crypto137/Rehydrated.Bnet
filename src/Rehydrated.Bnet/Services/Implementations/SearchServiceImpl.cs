using bnet.protocol;
using bnet.protocol.search;
using Google.ProtocolBuffers;
namespace Rehydrated.Bnet.Services.Implementations
{
    internal class SearchServiceImpl : SearchService, IBnetService
    {
        public ServiceId ServiceId { get; set; }

        public override void FindMatches(IRpcController controller, FindMatchesRequest request, Action<FindMatchesResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void SetObject(IRpcController controller, SetObjectRequest request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void RemoveObjects(IRpcController controller, RemoveObjectsRequest request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }
    }
}

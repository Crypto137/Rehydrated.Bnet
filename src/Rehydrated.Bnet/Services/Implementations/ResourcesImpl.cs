using bnet.protocol;
using bnet.protocol.resources;
using Google.ProtocolBuffers;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class ResourcesImpl : Resources, IBnetService
    {
        public ServiceId ServiceId { get; set; }

        public override void GetContentHandle(IRpcController controller, ContentHandleRequest request, Action<ContentHandle> done)
        {
            throw new NotImplementedException();
        }
    }
}

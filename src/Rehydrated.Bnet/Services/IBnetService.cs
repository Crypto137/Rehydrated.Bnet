using Google.ProtocolBuffers;

namespace Rehydrated.Bnet.Services
{
    internal interface IBnetService : IService
    {
        public ServiceId ServiceId { get; set; }
    }
}

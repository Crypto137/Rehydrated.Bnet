using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using NLog;
using Rehydrated.Bnet.Extensions;
using Rehydrated.Bnet.Network;
using Rehydrated.Bnet.Services;
using Rehydrated.Bnet.Utils;

namespace Rehydrated.Bnet.Rpc
{
    /// <summary>
    /// Represents a connection to a remote RPC service provider (i.e. client).
    /// </summary>
    internal class RpcChannel : IRpcChannel
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<uint, uint> _serverServices = new();
        private readonly Dictionary<uint, uint> _clientServices = new();
        private readonly Dictionary<uint, RpcCallback> _callbacks = new();

        private uint _currentToken = 0;

        public BnetClient Client { get; }

        /// <summary>
        /// Constructs an <see cref="RpcChannel"/> for the provided <see cref="BnetClient"/>.
        /// </summary>
        public RpcChannel(BnetClient client)
        {
            Client = client;
        }

        /// <summary>
        /// Binds a server service to this <see cref="RpcChannel"/>.
        /// </summary>
        public uint BindServerService(uint serviceHash)
        {
            // We use fixed ids for our server services, but in theory they can be arbitrary I think.
            ServiceId? fixedServiceId = BnetServerApp.Instance.ServiceManager.GetServiceId(serviceHash);
            if (fixedServiceId == null)
                throw new Exception($"Client attempting to import service for unknown hash 0x{serviceHash:X}.");

            uint serviceId = (uint)fixedServiceId;

            if (_serverServices.TryAdd(serviceHash, serviceId) == false)
                throw new Exception($"Server service {fixedServiceId} is already bound.");

            Logger.Trace($"EXPORT 0x{serviceHash:X8} => {serviceId}");
            return serviceId;
        }

        /// <summary>
        /// Binds a client service to this <see cref="RpcChannel"/>.
        /// </summary>
        public bool BindClientService(uint serviceHash, uint serviceId)
        {
            if (ClientServiceStubCollection.IsKnownClientService(serviceHash) == false)
                throw new Exception($"Unexpected client service 0x{serviceHash:X}.");

            // Clients can sometimes send multiple channel ids for the same hash, are we supposed to overwrite them?
            _clientServices[serviceHash] = serviceId;

            Logger.Trace($"IMPORT 0x{serviceHash:X8} => {serviceId}");
            return true;
        }

        /// <summary>
        /// Returns the registered <see cref="RpcCallback"/> for the specified token.
        /// Return <see langword="null"/> if no callback is registered.
        /// </summary>
        public RpcCallback PopCallback(uint token)
        {
            if (_callbacks.Remove(token, out RpcCallback callback) == false)
                return null;

            return callback;
        }

        /// <summary>
        /// Makes an RPC to the client over this <see cref="RpcChannel"/>.
        /// </summary>
        /// <remarks>
        /// Do not call this directly, use service stubs from <see cref="ClientServiceStubCollection"/>.
        /// </remarks>
        public void CallMethod(MethodDescriptor method, IRpcController controller, IMessage request, IMessage responsePrototype, Action<IMessage> done)
        {
            string serviceName = method.Service.FullName;
            uint serviceHash = HashUtility.Fnv1a(serviceName);

            if (_clientServices.TryGetValue(serviceHash, out uint serviceId) == false)
                throw new RpcException($"No id bound to service {method.Service.Name} (0x{serviceHash:X}).");

            // Generate a new request token
            uint token = _currentToken++;
            ulong objectId = controller != null ? controller.GetObjectId() : 0;

            // Push a callback if needed
            if (bnet.protocol.NO_RESPONSE.DefaultInstance.Equals(responsePrototype) == false)
            {
                RpcCallback callback = new(token, responsePrototype.WeakToBuilder(), done);
                _callbacks.Add(token, callback);
            }

            RpcPacket packet = new((byte)serviceId, method.GetMethodId(), token, objectId, request);
            Client.Send(packet);
        }
    }
}

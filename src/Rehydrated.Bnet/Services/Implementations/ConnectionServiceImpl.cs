using bnet.protocol;
using bnet.protocol.connection;
using Google.ProtocolBuffers;
using NLog;
using Rehydrated.Bnet.Extensions;
using Rehydrated.Bnet.Rpc;
using Rehydrated.Bnet.Utils;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class ConnectionServiceImpl : ConnectionService, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public override void Connect(IRpcController controller, ConnectRequest request, Action<ConnectResponse> done)
        {
            Logger.Debug($"Connect()\n{request}");

            // Automatically bind ConnectionService
            RpcChannel rpcChannel = controller.GetRpcChannel();
            uint serviceHash = HashUtility.Fnv1a(Descriptor.FullName);
            rpcChannel.BindServerService(serviceHash);
            rpcChannel.BindClientService(serviceHash, 0);

            // Send response
            uint epoch = (uint)Clock.UnixTime.TotalSeconds;

            var response = ConnectResponse.CreateBuilder()
                .SetServerId(ProcessId.CreateBuilder().SetLabel(0).SetEpoch(epoch))
                .SetClientId(ProcessId.CreateBuilder().SetLabel(1).SetEpoch(epoch));

            if (request.HasClientId)
                response.SetClientId(request.ClientId);

            if (request.HasBindRequest)
            {
                BindResponse.Builder bindResponse = BindResponse.CreateBuilder();

                response.SetBindResult(0);

                // Export server service ids (for client -> server calls)
                foreach (uint serverServiceHash in request.BindRequest.ImportedServiceHashList)
                {
                    uint serviceId = rpcChannel.BindServerService(serverServiceHash);
                    bindResponse.AddImportedServiceId(serviceId);
                }

                // Import client service ids (for server -> client calls)
                foreach (BoundService clientService in request.BindRequest.ExportedServiceList)
                    rpcChannel.BindClientService(clientService.Hash, clientService.Id);

                response.SetBindResponse(bindResponse);
            }

            /*
            // this is no longer available to download and hangs the login process
            response.SetContentHandleArray(ConnectionMeteringContentHandles.CreateBuilder()
                .AddContentHandle(ContentHandle.CreateBuilder()
                    .SetRegion(0x5553)      // US
                    .SetUsage(0x6D74727A)   // mtrz
                    .SetHash(ByteString.Unsafe.FromBytes(Convert.FromHexString("acaeab71f005567974a656cf1207f74bb9a5365c84e9f22f1f82ffec3d1367a8")))));
            */

            done(response.Build());
        }

        public override void Bind(IRpcController controller, BindRequest request, Action<BindResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void Echo(IRpcController controller, EchoRequest request, Action<EchoResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void ForceDisconnect(IRpcController controller, DisconnectNotification request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void KeepAlive(IRpcController controller, NoData request, Action<NO_RESPONSE> done)
        {
            Logger.Debug("KeepAlive()");
            done(NO_RESPONSE.DefaultInstance);
        }

        public override void Encrypt(IRpcController controller, EncryptRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void RequestDisconnect(IRpcController controller, DisconnectRequest request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }
    }
}

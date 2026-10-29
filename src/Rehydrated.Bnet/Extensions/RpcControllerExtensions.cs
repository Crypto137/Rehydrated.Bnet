using Google.ProtocolBuffers;
using Rehydrated.Bnet.Network;
using Rehydrated.Bnet.Rpc;
using Rehydrated.Bnet.Services;

namespace Rehydrated.Bnet.Extensions
{
    internal static class RpcControllerExtensions
    {
        public static BnetClient GetClient(this IRpcController rpcController)
        {
            return ((RpcContext)rpcController).Client;
        }

        public static RpcChannel GetRpcChannel(this IRpcController rpcController)
        {
            return ((RpcContext)rpcController).Client?.RpcChannel;
        }

        public static ClientServiceStubCollection GetServices(this IRpcController rpcController)
        {
            return ((RpcContext)rpcController).Client?.Services;
        }

        public static ulong GetObjectId(this IRpcController rpcController)
        {
            return ((RpcContext)rpcController).ObjectId;
        }
    }
}

using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using NLog;
using Rehydrated.Bnet.Jobs;
using Rehydrated.Bnet.Network;

namespace Rehydrated.Bnet.Rpc
{
    /// <summary>
    /// Represents a job to process an RPC request from a client.
    /// </summary>
    internal sealed class RpcRequest : Job
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public BnetClient Client { get; }
        public IService Service { get; }
        public MethodDescriptor Method { get; }
        public uint RequestId { get; }
        public ulong ObjectId { get; }
        public IMessage Message { get; }

        public RpcRequest(BnetClient client, IService service, MethodDescriptor method, uint requestId, ulong objectId, IMessage message)
        {
            Client = client;
            Service = service;
            Method = method;
            RequestId = requestId;
            ObjectId = objectId;
            Message = message;
        }

        public override string ToString()
        {
            return $"Job {JobId}: RpcRequest [{Client}] => {Service.DescriptorForType.Name}.{Method.Name}() (RequestId={RequestId}, ObjectId={ObjectId})";
        }

        public override void Execute()
        {
            BnetClient client = Client;
            uint requestId = RequestId;

            RpcContext context = new(client, ObjectId);

            try
            {
                Service.CallMethod(Method, context, Message, response => client.Send(new RpcPacket(requestId, response)));
            }
            catch (NotImplementedException)
            {
                Logger.Warn($"Execute(): Unimplemented service method {Service.DescriptorForType.Name}.{Method.Name}()");
            }
        }
    }
}

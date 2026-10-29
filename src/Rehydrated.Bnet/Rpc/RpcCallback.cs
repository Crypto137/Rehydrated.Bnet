using Google.ProtocolBuffers;
using Rehydrated.Bnet.Jobs;

namespace Rehydrated.Bnet.Rpc
{
    /// <summary>
    /// Represents a callback for an RPC request made from the server to the client.
    /// </summary>
    internal sealed class RpcCallback : Job
    {
        public uint Token { get; }
        public IBuilder Builder { get; }
        public Action<IMessage> Action { get; }

        public RpcCallback(uint token, IBuilder builder, Action<IMessage> action)
        {
            Token = token;
            Builder = builder;
            Action = action;
        }

        public override string ToString()
        {
            return $"Job {JobId}: RpcCallback (Token={Token}, Message={Builder.DescriptorForType.Name})";
        }

        public override void Execute()
        {
            IMessage message = Builder.WeakBuild();

            if (Action == null)
                throw new RpcException($"No callback action is provided.");

            Action(message);
        }
    }
}

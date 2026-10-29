using Google.ProtocolBuffers;
using Rehydrated.Bnet.Network;

namespace Rehydrated.Bnet.Rpc
{
    /// <summary>
    /// Implementation of <see cref="IRpcController"/> that provides context to RPC requests and replies.
    /// </summary>
    internal class RpcContext : IRpcController
    {
        public BnetClient Client { get; }
        public ulong ObjectId { get; }

        public RpcContext(BnetClient client, ulong objectId)
        {
            Client = client;
            ObjectId = objectId;
        }

        #region IRpcController

        // Descriptions according to protobuf-csharp-port docs. Are we actually going to use this?

        // Client side (making requests)

        // Indicates that the server communicated that the request has failed.
        public bool Failed { get => throw new NotImplementedException(); }

        // Text for Failed.
        public string ErrorText { get => throw new NotImplementedException(); }

        public void Reset()
        {
            // Resets the controller to initial state to reuse for a new call.
            throw new NotImplementedException();
        }

        public void StartCancel()
        {
            // Cancels a call that has been made and does the callback without receiving a reply.
            // Potentially communicates this to the server, but this is not guaranteed.
            throw new NotImplementedException();
        }

        // Server side (replying to requests)

        public void SetFailed(string reason)
        {
            // Communicates to the client that Failed needs to be set to true.
            throw new NotImplementedException();
        }

        public bool IsCanceled()
        {
            // Indicates that the client communicated that the request needs to be cancelled via StartCancel().
            // The server should still send the reply.
            throw new NotImplementedException();
        }

        public void NotifyOnCancel(Action<object> callback)
        {
            // Callback to do server-side when the client cancels the request.
            throw new NotImplementedException();
        }

        #endregion
    }
}

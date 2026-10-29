namespace Rehydrated.Bnet.Rpc
{
    /// <summary>
    /// The exception that is thrown when something goes wrong with the RPC system.
    /// </summary>
    internal class RpcException : Exception
    {
        /// <summary>
        /// Constructs an <see cref="RpcException"/> with the provided message.
        /// </summary>
        public RpcException(string message) : base(message)
        {
        }
    }
}

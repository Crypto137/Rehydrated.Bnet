using Google.ProtocolBuffers;
using Google.ProtocolBuffers.Descriptors;
using NLog;
using Rehydrated.Bnet.Extensions;
using Rehydrated.Bnet.Jobs;
using Rehydrated.Bnet.Network;
using Rehydrated.Bnet.Services;
using System.Buffers.Binary;
using System.Diagnostics;

namespace Rehydrated.Bnet.Rpc
{
    internal static class RpcParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Attempts to consume data received from the provided <see cref="BnetClient"/>. Returns the number of bytes consumed.
        /// </summary>
        public static int ConsumeData(BnetClient client, byte[] buffer, int count)
        {
            if (count == 0)
                return 0;

            int offset = 0;

            while (offset < count)
            {
                int consumedBytes = TryParsePacket(client, buffer, offset, count);

                // Break the loop if the parser function exited without consuming anything.
                // This generally indicates there isn't enough data to parse the next packet.
                if (consumedBytes == 0)
                    break;

                offset += consumedBytes;
            }

            Debug.Assert(offset >= 0 && offset <= count);

            return offset;
        }

        /// <summary>
        /// Attempts to parse a packet from the provided buffer. Returns the number of bytes consumed.
        /// </summary>
        private static int TryParsePacket(BnetClient client, byte[] buffer, int offset, int count)
        {
            if (count < sizeof(ushort))
                return 0;

            int headerSize = (buffer[offset] << 8) | buffer[offset + 1];
            if (count < sizeof(ushort) + headerSize)
                return 0;

            CodedInputStream stream = CodedInputStream.CreateInstance(buffer, offset + sizeof(ushort), headerSize);
            bnet.protocol.Header header = bnet.protocol.Header.ParseFrom(stream);

            int payloadSize = (int)header.Size;
            if (count < sizeof(ushort) + headerSize + payloadSize)
                return 0;

            stream = CodedInputStream.CreateInstance(buffer, offset + sizeof(ushort) + headerSize, payloadSize);

            Job job = header.ServiceId == 0xFE
                ? ParseReply(client, stream, header)
                : ParseRequest(client, stream, header);

            if (job != null)
                BnetServerApp.Instance.JobQueue.EnqueueJob(job);

            return sizeof(ushort) + headerSize + payloadSize;
        }

        /// <summary>
        /// Parses an RPC request payload from the provided <see cref="CodedInputStream"/>.
        /// Returns an <see cref="Job"/> implementation to process it.
        /// </summary>
        private static RpcRequest ParseRequest(BnetClient client, CodedInputStream stream, bnet.protocol.Header header)
        {
            // Find a service implementation for the requested service id
            IService service = BnetServerApp.Instance.ServiceManager.GetServerService((ServiceId)header.ServiceId);
            if (service == null)
                throw new RpcException($"No registered service found for service id 0x{header.ServiceId:X}");

            // Find a method for the request method id
            MethodDescriptor method = service.GetMethod(header.MethodId);
            if (method == null)
                throw new RpcException($"No method found for method id 0x{header.MethodId:X} in service {(ServiceId)header.ServiceId}");

            // Parse request message for the found method
            IMessage message = null;

            try
            {
                IBuilder builder = service.GetRequestPrototype(method).WeakToBuilder();
                stream.ReadMessage(builder, ExtensionRegistry.Empty, (int)header.Size);
                message = builder.WeakBuild();
            }
            catch (UninitializedMessageException)
            {
                Logger.Warn($"ParseMessage(): Failed to parse request message [{message}]");
                return null;
            }

            return new(client, service, method, header.Token, header.ObjectId, message);
        }

        /// <summary>
        /// Parses an RPC reply payload from the provided <see cref="CodedInputStream"/>.
        /// Return an <see cref="Job"/> implementation to process it.
        /// </summary>
        private static RpcCallback ParseReply(BnetClient client, CodedInputStream stream, bnet.protocol.Header header)
        {
            RpcCallback callback = client.RpcChannel.PopCallback(header.Token);
            if (callback == null)
                throw new RpcException($"No callback registered for token {header.Token}.");

            stream.ReadMessage(callback.Builder, ExtensionRegistry.Empty, (int)header.Size);

            return callback;
        }
    }
}

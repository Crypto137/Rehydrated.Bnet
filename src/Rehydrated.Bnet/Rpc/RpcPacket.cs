using Google.ProtocolBuffers;

namespace Rehydrated.Bnet.Rpc
{
    /// <summary>
    /// Contains an <see cref="IMessage"/> with additional metadata needed to perform an RPC.
    /// </summary>
    internal readonly struct RpcPacket
    {
        public readonly bnet.protocol.Header Header;
        public readonly IMessage Message;

        public int SerializedSize { get => Header.SerializedSize + Message.SerializedSize; }

        /// <summary>
        /// Constructs an <see cref="RpcPacket"/> to make an RPC call to the client. 
        /// </summary>
        public RpcPacket(byte serviceId, uint methodId, uint token, ulong objectId, IMessage message)
        {
            Header = bnet.protocol.Header.CreateBuilder()
                .SetServiceId(serviceId)
                .SetMethodId(methodId)
                .SetToken(token)
                .SetObjectId(objectId)
                .SetSize((uint)message.SerializedSize)
                .Build();

            Message = message;
        }

        /// <summary>
        /// Constructs an <see cref="RpcPacket"/> to reply to an RPC call from the client.
        /// </summary>
        public RpcPacket(uint token, IMessage message)
        {
            Header = bnet.protocol.Header.CreateBuilder()
                .SetServiceId(0xFE) // -2 ReplyServiceId
                .SetMethodId(0)
                .SetToken(token)
                .SetObjectId(0)
                .SetSize((uint)message.SerializedSize)
                .Build();

            Message = message;
        }

        public int Serialize(Stream stream)
        {
            ushort headerSize = (ushort)Header.SerializedSize;
            stream.WriteByte((byte)(headerSize >> 8));
            stream.WriteByte((byte)(headerSize & 0xFF));

            CodedOutputStream cos = CodedOutputStream.CreateInstance(stream);
            Header.WriteTo(cos);
            Message.WriteTo(cos);
            cos.Flush();

            return (int)(sizeof(ushort) + headerSize + Header.Size);
        }
    }
}

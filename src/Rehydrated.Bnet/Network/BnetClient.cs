using NLog;
using Rehydrated.Bnet.Rpc;
using Rehydrated.Bnet.Services;
using System.Net.Sockets;

namespace Rehydrated.Bnet.Network
{
    internal class BnetClient
    {
        private const int ReceiveBufferSize = 8192;

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly Socket _socket;
        private readonly CancellationToken _cancellationToken;
        private readonly byte[] _receiveBuffer = new byte[ReceiveBufferSize];

        private int _receivePosition = 0;

        public RpcChannel RpcChannel { get; }
        public ClientServiceStubCollection Services { get; }

        public BnetClient(Socket socket, CancellationToken cancellationToken)
        {
            RpcChannel = new(this);
            Services = new(RpcChannel);

            _socket = socket;
            _cancellationToken = cancellationToken;
            _ = ReceiveData();
        }

        public void Send(RpcPacket packet)
        {
            // TODO: optimize this
            using MemoryStream ms = new();
            packet.Serialize(ms);
            byte[] buffer = ms.ToArray();
            _socket.Send(buffer);
        }

        private async Task ReceiveData()
        {
            Logger.Info("Connected");

            while (_cancellationToken.IsCancellationRequested == false)
            {
                try
                {
                    int received = await _socket.ReceiveAsync(_receiveBuffer.AsMemory(_receivePosition), _cancellationToken);
                    if (received == 0)
                        break;

                    _receivePosition += received;

                    int consumed = RpcParser.ConsumeData(this, _receiveBuffer, _receivePosition);
                    if (consumed > 0)
                    {
                        _receivePosition -= consumed;
                        if (_receivePosition > 0)
                            Buffer.BlockCopy(_receiveBuffer, consumed, _receiveBuffer, 0, _receivePosition);
                    }
                    else if (_receivePosition == ReceiveBufferSize)
                    {
                        throw new InternalBufferOverflowException();
                    }
                }
                catch (SocketException)
                {
                    break;
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    break;
                }
            }

            _socket?.Disconnect(false);
            Logger.Info("Disconnected");
        }
    }
}

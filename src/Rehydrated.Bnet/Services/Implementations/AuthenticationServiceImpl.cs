using bnet.protocol;
using bnet.protocol.authentication;
using Google.ProtocolBuffers;
using NLog;
using Rehydrated.Bnet.Extensions;
using Rehydrated.Bnet.Network;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class AuthenticationServiceImpl : AuthenticationServer, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public override void Logon(IRpcController controller, LogonRequest request, Action<NoData> done)
        {
            Logger.Debug($"Logon()\n{request}");
            done(NoData.DefaultInstance);
            
            // Currently we patch the client's battle.net.dll to disable module-based authentication and encryption.
            // To implement this as it used to be, we need to stream module dlls to the client to do authentication,
            // and then encrypt the stream with TLS.
            LogonResult result = LogonResult.CreateBuilder()
                .SetAccount(EntityId.CreateBuilder().SetHigh(0x0100000000000000).SetLow(1))
                .AddGameAccount(EntityId.CreateBuilder().SetHigh(0x0200000000000000).SetLow(1))
                .SetErrorCode(0)
                .Build();

            BnetClient client = controller.GetClient();
            client.Services.AuthenticationClient.LogonComplete(null, result, null);
        }

        public override void ModuleNotify(IRpcController controller, ModuleNotification request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void ModuleMessage(IRpcController controller, ModuleMessageRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void SelectGameAccount(IRpcController controller, EntityId request, Action<NoData> done)
        {
            Logger.Debug($"SelectGameAccount()\n{request}");
            done(NoData.DefaultInstance);
        }

        public override void GenerateSSOToken(IRpcController controller, GenerateSSOTokenRequest request, Action<GenerateSSOTokenResponse> done)
        {
            throw new NotImplementedException();
        }
    }
}

using bnet.protocol;
using bnet.protocol.game_utilities;
using bnet.protocol.server_pool;
using Google.ProtocolBuffers;
using NLog;

namespace Rehydrated.Bnet.Services.Implementations
{
    public enum GameUtilitiesClientRequestType
    {
        HeroCreate = 1,         // in D3.OnlineService.HeroCreateParams,
        HeroSelect = 3,         // in D3.OnlineService.EntityId
        InitialLogin = 6,       // in D3.GameMessage.InitialLoginDataRequest, out D3.GameMessage.InitialLoginDataResponse
        GetToonSettings = 8,    // in D3.GameMessage.GetToonSettings, out D3.Client.ToonSettings
        GetAccountPrefs = 15,   // in D3.GameMessage.GetAccountPrefs
    }

    internal class GameUtilitiesImpl : GameUtilities, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public static bnet.protocol.attribute.Attribute BuildMessageAttribute(IMessage message)
        {
            return bnet.protocol.attribute.Attribute.CreateBuilder()
                .SetName("CustomMessage")
                .SetValue(bnet.protocol.attribute.Variant.CreateBuilder().SetMessageValue(message.ToByteString()))
                .Build();
        }

        public override void ProcessClientRequest(IRpcController controller, ClientRequest request, Action<ClientResponse> done)
        {
            GameUtilitiesClientRequestType requestType = (GameUtilitiesClientRequestType)request.GetAttribute(1).Value.IntValue;

            ClientResponse.Builder response = ClientResponse.CreateBuilder();

            switch (requestType)
            {
                case GameUtilitiesClientRequestType.HeroCreate:     // hero creation request - D3.OnlineService.HeroCreateParams
                    D3.OnlineService.HeroCreateParams createParams = D3.OnlineService.HeroCreateParams.ParseFrom(
                        request.GetAttribute(2).Value.MessageValue);
                    Logger.Debug($"ProcessClientRequest(): HeroCreate\n{createParams}");
                    break;

                case GameUtilitiesClientRequestType.HeroSelect:     // hero switch request EntityId
                    D3.OnlineService.EntityId heroId = D3.OnlineService.EntityId.ParseFrom(
                        request.GetAttribute(2).Value.MessageValue);
                    Logger.Debug($"ProcessClientRequest(): HeroSelect\n{heroId}");
                    break;

                case GameUtilitiesClientRequestType.InitialLogin:     // login request - D3.GameMessage.InitialLoginDataRequest
                    Logger.Debug("ProcessClientRequest(): InitialLogin");
                    response.AddAttribute(BuildMessageAttribute(D3.GameMessage.InitialLoginDataResponse.CreateBuilder()
                        .SetGameAccountSettings(D3.Client.GameAccountSettings.CreateBuilder()
                            .SetUseLastHero(1))
                        .SetHeroDigests(D3.GameMessage.HeroDigestListResponse.CreateBuilder()
                            .AddDigestList(D3.Hero.Digest.CreateBuilder()
                                .SetVersion(902)
                                .SetHeroId(D3.OnlineService.EntityId.CreateBuilder().SetIdHigh(0x0300000000000000).SetIdLow(1))
                                .SetHeroName("Rehydrated")
                                .SetGbidClass(0x1D4681B1)
                                .SetLevel(1)
                                .SetPlayerFlags(2)
                                .SetVisualEquipment(D3.Hero.VisualEquipment.DefaultInstance)
                                .SetLastPlayedAct(0)
                                .SetHighestUnlockedAct(0)
                                .SetLastPlayedDifficulty(0)
                                .SetHighestUnlockedDifficulty(0)
                                .SetLastPlayedQuest(-1)
                                .SetLastPlayedQuestStep(-1)
                                .SetTimePlayed(0)))
                        .SetAccountDigest(D3.Account.Digest.CreateBuilder()
                            // 7447=>99, 7728=> 100, 8801=>102, 8296=>105, 8610=>106, 8815=>106, 8896=>106, 9183=>107
                            .SetVersion(107)
                            .SetLastPlayedHeroId(D3.OnlineService.EntityId.CreateBuilder().SetIdHigh(0x0300000000000000).SetIdLow(1))
                            .SetBannerConfiguration(D3.Account.BannerConfiguration.CreateBuilder()
                                .SetBannerShape(0)
                                .SetSigilMain(0)
                                .SetSigilAccent(0)
                                .SetPatternColor(0)
                                .SetBackgroundColor(0)
                                .SetSigilColor(0)
                                .SetSigilPlacement(0)
                                .SetPattern(0)
                                .SetUseSigilVariant(false)
                                .SetEpicBanner(0))
                                .SetFlags(1 << 1) // HARDCORE_HERO_UNLOCKED
                                .SetPvpCooldown(0))
                        .SetNumPlayers(1)
                        .SetAuctionHouseGoldUnitStackSize(100000)
                        .Build()));
                    break;

                case GameUtilitiesClientRequestType.GetToonSettings:
                    Logger.Debug("ProcessClientRequest(): GetToonSettings");
                    response.AddAttribute(BuildMessageAttribute(D3.Client.ToonSettings.DefaultInstance));
                    break;

                case GameUtilitiesClientRequestType.GetAccountPrefs:
                    Logger.Debug("ProcessClientRequest(): GetAccountPrefs");
                    response.AddAttribute(BuildMessageAttribute(D3.Client.Preferences.CreateBuilder()
                        .SetVersion(44)
                        .Build()));
                    break;

                default:
                    Logger.Warn($"ProcessClientRequest(): Unhandled client request type {requestType}");
                    break;
            }

            done(response.Build());
        }

        public override void PresenceChannelCreated(IRpcController controller, PresenceChannelCreatedRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void GetPlayerVariables(IRpcController controller, GetPlayerVariablesRequest request, Action<GetPlayerVariablesResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetLoad(IRpcController controller, GetLoadRequest request, Action<ServerState> done)
        {
            throw new NotImplementedException();
        }

        public override void ProcessServerRequest(IRpcController controller, ServerRequest request, Action<ServerResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void NotifyGameAccountOnline(IRpcController controller, GameAccountOnlineNotification request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void NotifyGameAccountOffline(IRpcController controller, GameAccountOfflineNotification request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }
    }
}

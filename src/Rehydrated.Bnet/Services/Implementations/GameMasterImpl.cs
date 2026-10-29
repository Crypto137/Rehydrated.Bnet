using bnet.protocol;
using bnet.protocol.game_master;
using bnet.protocol.notification;
using Google.ProtocolBuffers;
using NLog;
using Rehydrated.Bnet.Extensions;
using Rehydrated.Bnet.Network;
using BnetAttribute = bnet.protocol.attribute.Attribute;
using BnetVariant = bnet.protocol.attribute.Variant;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class GameMasterImpl : GameMaster, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public override void JoinGame(IRpcController controller, JoinGameRequest request, Action<JoinGameResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void ListFactories(IRpcController controller, ListFactoriesRequest request, Action<ListFactoriesResponse> done)
        {
            Logger.Debug($"ListFactories():\n{request}");

            // terrible hardcoded placeholder junk
            done(ListFactoriesResponse.CreateBuilder()
                .AddDescription(GameFactoryDescription.CreateBuilder()
                    .SetId(14249086168335147635)
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("min_players").SetValue(BnetVariant.CreateBuilder().SetIntValue(2)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("max_players").SetValue(BnetVariant.CreateBuilder().SetIntValue(4)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("num_teams").SetValue(BnetVariant.CreateBuilder().SetIntValue(1)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("version").SetValue(BnetVariant.CreateBuilder().SetStringValue("1.0.8")))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("PlayerGroup").SetValue(BnetVariant.CreateBuilder().SetIntValue(0)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("GameType").SetValue(BnetVariant.CreateBuilder().SetIntValue(1)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("Game.CurrentQuest").SetValue(BnetVariant.DefaultInstance))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("DifficultyLevel").SetValue(BnetVariant.CreateBuilder().SetIntValue(0)))
                    .AddStatsBucket(GameStatsBucket.CreateBuilder()
                        .SetBucketMin(0)
                        .SetBucketMax(4267296)
                        .SetWaitMilliseconds(1000)
                        .SetGamesPerHour(0)
                        .SetActiveGames(0)
                        .SetActivePlayers(0)
                        .SetFormingGames(0)
                        .SetWaitingPlayers(0)))
                .AddDescription(GameFactoryDescription.CreateBuilder()
                    .SetId(14249086168335147636)
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("min_players").SetValue(BnetVariant.CreateBuilder().SetIntValue(2)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("max_players").SetValue(BnetVariant.CreateBuilder().SetIntValue(4)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("num_teams").SetValue(BnetVariant.CreateBuilder().SetIntValue(1)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("version").SetValue(BnetVariant.CreateBuilder().SetStringValue("1.0.8")))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("PlayerGroup").SetValue(BnetVariant.CreateBuilder().SetIntValue(1)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("GameType").SetValue(BnetVariant.CreateBuilder().SetIntValue(1)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("Game.CurrentQuest").SetValue(BnetVariant.DefaultInstance))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("DifficultyLevel").SetValue(BnetVariant.CreateBuilder().SetIntValue(0)))
                    .AddStatsBucket(GameStatsBucket.CreateBuilder()
                        .SetBucketMin(0)
                        .SetBucketMax(4267296)
                        .SetWaitMilliseconds(1000)
                        .SetGamesPerHour(0)
                        .SetActiveGames(0)
                        .SetActivePlayers(0)
                        .SetFormingGames(0)
                        .SetWaitingPlayers(0)))
                .AddDescription(GameFactoryDescription.CreateBuilder()
                    .SetId(14249086168335147637)
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("min_players").SetValue(BnetVariant.CreateBuilder().SetIntValue(2)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("max_players").SetValue(BnetVariant.CreateBuilder().SetIntValue(4)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("num_teams").SetValue(BnetVariant.CreateBuilder().SetIntValue(1)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("version").SetValue(BnetVariant.CreateBuilder().SetStringValue("1.0.8")))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("PlayerGroup").SetValue(BnetVariant.CreateBuilder().SetIntValue(2)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("GameType").SetValue(BnetVariant.CreateBuilder().SetIntValue(1)))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("Game.CurrentQuest").SetValue(BnetVariant.DefaultInstance))
                    .AddAttribute(BnetAttribute.CreateBuilder().SetName("DifficultyLevel").SetValue(BnetVariant.CreateBuilder().SetIntValue(0)))
                    .AddStatsBucket(GameStatsBucket.CreateBuilder()
                        .SetBucketMin(0)
                        .SetBucketMax(4267296)
                        .SetWaitMilliseconds(1000)
                        .SetGamesPerHour(0)
                        .SetActiveGames(0)
                        .SetActivePlayers(0)
                        .SetFormingGames(0)
                        .SetWaitingPlayers(0)))
                .SetTotalResults(3)
                .Build());
        }

        public override void FindGame(IRpcController controller, FindGameRequest request, Action<FindGameResponse> done)
        {
            Logger.Debug($"FindGame():\n{request}");

            const ulong FactoryId = 666;
            const ulong RequestId = 1337;
            const ulong GameLowId = 80085;

            done(FindGameResponse.CreateBuilder()
                .SetFactoryId(FactoryId)
                .SetRequestId(RequestId)
                .Build());

            BnetClient client = controller.GetClient();

            EntityId gameAccountId = EntityId.CreateBuilder().SetHigh(0x0200000000000000).SetLow(1).Build();

            D3.OnlineService.GameCreateParams createParams = D3.OnlineService.GameCreateParams.CreateBuilder()
                .SetGameType(1)
                .SetCreationFlags(0)
                .Build();

            ConnectInfo connectInfo = ConnectInfo.CreateBuilder()
                .SetMemberId(gameAccountId)
                .SetHost("127.0.0.1")
                .SetPort(6112)
                .SetToken(ByteString.CopyFrom([0x07, 0x34, 0x02, 0x60, 0x91, 0x93, 0x76, 0x46, 0x28, 0x84]))
                .AddAttribute(BnetAttribute.CreateBuilder()
                    .SetName("SGameId").SetValue(BnetVariant.CreateBuilder().SetIntValue(1).Build()))
                .AddAttribute(BnetAttribute.CreateBuilder()
                    .SetName("GameCreateParams").SetValue(BnetVariant.CreateBuilder().SetMessageValue(createParams.ToByteString()).Build()))
                .Build();

            GameHandle gameHandle = GameHandle.CreateBuilder()
                .SetFactoryId(FactoryId)
                .SetGameId(EntityId.CreateBuilder().SetHigh(0x0600000000000000).SetLow(GameLowId))
                .Build();

            /*
            Notification mmStartNotification = Notification.CreateBuilder()
                .SetTargetId(gameAccountId)
                .SetType("MM_START")
                .AddAttribute(BnetAttribute.CreateBuilder()
                    .SetName("game_request_id").SetValue(BnetVariant.CreateBuilder().SetUintValue(RequestId)))
                .Build();

            client.Services.NotificationListener.OnNotificationReceived(null, mmStartNotification, null);
            */

            Notification gameEntryNotification = Notification.CreateBuilder()
                .SetTargetId(gameAccountId)
                .SetType("GAME_ENTRY")
                .AddAttribute(BnetAttribute.CreateBuilder()
                    .SetName("connection_info").SetValue(BnetVariant.CreateBuilder().SetMessageValue(connectInfo.ToByteString())))
                .AddAttribute(BnetAttribute.CreateBuilder()
                    .SetName("game_handle").SetValue(BnetVariant.CreateBuilder().SetMessageValue(gameHandle.ToByteString())))
                .AddAttribute(BnetAttribute.CreateBuilder()
                    .SetName("game_request_id").SetValue(BnetVariant.CreateBuilder().SetUintValue(RequestId)))
                .Build();

            client.Services.NotificationListener.OnNotificationReceived(null, gameEntryNotification, null);
        }

        public override void CancelGameEntry(IRpcController controller, CancelGameEntryRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void GameEnded(IRpcController controller, GameEndedNotification request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void PlayerLeft(IRpcController controller, PlayerLeftNotification request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void RegisterServer(IRpcController controller, RegisterServerRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UnregisterServer(IRpcController controller, UnregisterServerRequest request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void RegisterUtilities(IRpcController controller, RegisterUtilitiesRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UnregisterUtilities(IRpcController controller, UnregisterUtilitiesRequest request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void Subscribe(IRpcController controller, SubscribeRequest request, Action<SubscribeResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void Unsubscribe(IRpcController controller, UnsubscribeRequest request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void ChangeGame(IRpcController controller, ChangeGameRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void GetFactoryInfo(IRpcController controller, GetFactoryInfoRequest request, Action<GetFactoryInfoResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetGameStats(IRpcController controller, GetGameStatsRequest request, Action<GetGameStatsResponse> done)
        {
            throw new NotImplementedException();
        }
    }
}

using bnet.protocol;
using bnet.protocol.account;
using Google.ProtocolBuffers;
using NLog;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class AccountServiceImpl : AccountService, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public override void GetGameAccount(IRpcController controller, GameAccountHandle request, Action<GameAccountBlob> done)
        {
            throw new NotImplementedException();
        }

        public override void GetAccount(IRpcController controller, GetAccountRequest request, Action<GetAccountResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void CreateGameAccount(IRpcController controller, CreateGameAccountRequest request, Action<GameAccountHandle> done)
        {
            throw new NotImplementedException();
        }

        public override void IsIgrAddress(IRpcController controller, IsIgrAddressRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void CacheExpire(IRpcController controller, CacheExpireRequest request, Action<NO_RESPONSE> done)
        {
            throw new NotImplementedException();
        }

        public override void CredentialUpdate(IRpcController controller, CredentialUpdateRequest request, Action<CredentialUpdateResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void FlagUpdate(IRpcController controller, FlagUpdateRequest request, Action<FlagUpdateResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetWalletList(IRpcController controller, GetWalletListRequest request, Action<GetWalletListResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetEBalance(IRpcController controller, GetEBalanceRequest request, Action<GetEBalanceResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void Subscribe(IRpcController controller, SubscriptionUpdateRequest request, Action<SubscriptionUpdateResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void Unsubscribe(IRpcController controller, SubscriptionUpdateRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void GetEBalanceRestrictions(IRpcController controller, GetEBalanceRestrictionsRequest request, Action<GetEBalanceRestrictionsResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetAccountState(IRpcController controller, GetAccountStateRequest request, Action<GetAccountStateResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetGameAccountState(IRpcController controller, GetGameAccountStateRequest request, Action<GetGameAccountStateResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetLicenses(IRpcController controller, GetLicensesRequest request, Action<GetLicensesResponse> done)
        {
            Logger.Debug($"GetLicenses()\n{request}");
            done(GetLicensesResponse.CreateBuilder()
                .AddLicenses(AccountLicense.CreateBuilder().SetId(111))
                .AddLicenses(AccountLicense.CreateBuilder().SetId(227))
                .AddLicenses(AccountLicense.CreateBuilder().SetId(168)) //Full Game - Removes upgrade banner
                .Build());
        }

        public override void GetGameTimeRemainingInfo(IRpcController controller, GetGameTimeRemainingInfoRequest request, Action<GetGameTimeRemainingInfoResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetGameSessionInfo(IRpcController controller, GetGameSessionInfoRequest request, Action<GetGameSessionInfoResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetCAISInfo(IRpcController controller, GetCAISInfoRequest request, Action<GetCAISInfoResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void ForwardCacheExpire(IRpcController controller, ForwardCacheExpireRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }
    }
}

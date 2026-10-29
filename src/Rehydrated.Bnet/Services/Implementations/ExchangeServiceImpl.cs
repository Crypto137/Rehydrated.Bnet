using bnet.protocol;
using bnet.protocol.exchange;
using bnet.protocol.exchange_object_provider;
using bnet.protocol.exchange_risk;
using Google.ProtocolBuffers;
using NLog;
using Rehydrated.Bnet.Utils;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class ExchangeServiceImpl : ExchangeService, IBnetService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ServiceId ServiceId { get; set; }

        public override void CreateOrderBook(IRpcController controller, CreateOrderBookRequest request, Action<CreateOrderBookResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void PlaceOfferOnOrderBook(IRpcController controller, PlaceOfferOnOrderBookRequest request, Action<PlaceOfferOnOrderBookResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void PlaceOfferCreateOrderBookIfNeeded(IRpcController controller, PlaceOfferCreateOrderBookIfNeededRequest request, Action<PlaceOfferCreateOrderBookIfNeededResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void PlaceBidOnOrderBook(IRpcController controller, PlaceBidOnOrderBookRequest request, Action<PlaceBidOnOrderBookResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void PlaceBidCreateOrderBookIfNeeded(IRpcController controller, PlaceBidCreateOrderBookIfNeededRequest request, Action<PlaceBidCreateOrderBookIfNeededResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void QueryOffersByOrderBook(IRpcController controller, QueryOffersByOrderBookRequest request, Action<QueryOffersByOrderBookResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void QueryBidsByOrderBook(IRpcController controller, QueryBidsByOrderBookRequest request, Action<QueryBidsByOrderBookResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void QueryOffersByAccountForItem(IRpcController controller, QueryOffersByAccountForItemRequest request, Action<QueryOffersByAccountForItemResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void QueryBidsByAccountForItem(IRpcController controller, QueryBidsByAccountForItemRequest request, Action<QueryBidsByAccountForItemResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void QueryOrderBooksSummary(IRpcController controller, QueryOrderBooksSummaryRequest request, Action<QueryOrderBooksSummaryResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void ReportAuthorize(IRpcController controller, ReportAuthorizeRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void ReportSettle(IRpcController controller, ReportSettleRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void ReportCancel(IRpcController controller, ReportCancelRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void SubscribeOrderBookStatusChange(IRpcController controller, SubscribeOrderBookStatusChangeRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UnsubscribeOrderBookStatusChange(IRpcController controller, UnsubscribeOrderBookStatusChangeRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void SubscribeOrderUpdate(IRpcController controller, SubscribeOrderUpdateRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UnsubscribeOrderUpdate(IRpcController controller, UnsubscribeOrderUpdateRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void GetPaymentMethods(IRpcController controller, GetPaymentMethodsRequest request, Action<GetPaymentMethodsResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void ClaimBidItem(IRpcController controller, ClaimRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void ClaimBidMoney(IRpcController controller, ClaimRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void ClaimOfferItem(IRpcController controller, ClaimRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void ClaimOfferMoney(IRpcController controller, ClaimRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void CancelBid(IRpcController controller, CancelRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void CancelOffer(IRpcController controller, CancelRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void GetConfiguration(IRpcController controller, GetConfigurationRequest request, Action<GetConfigurationResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetBidFeeEstimation(IRpcController controller, GetBidFeeEstimationRequest request, Action<GetFeeEstimationResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetOfferFeeEstimation(IRpcController controller, GetOfferFeeEstimationRequest request, Action<GetFeeEstimationResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void QueryOrdersByAccountForItem(IRpcController controller, QueryOrdersByAccountForItemRequest request, Action<QueryOrdersByAccountForItemResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void ReportAuthorizeRiskVerdict(IRpcController controller, ReportAuthorizeRiskVerdictRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void ReportSettleRiskVerdict(IRpcController controller, ReportSettleRiskVerdictRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void DelaySettleRiskVerdict(IRpcController controller, DelaySettleRiskVerdictRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void GetFeeDetails(IRpcController controller, GetFeeDetailsRequest request, Action<GetFeeDetailsResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetOrderBookStatistics(IRpcController controller, GetOrderBookStatisticsRequest request, Action<GetOrderBookStatisticsResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetBidDetails(IRpcController controller, GetBidDetailsRequest request, Action<GetBidDetailsResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetOfferDetails(IRpcController controller, GetOfferDetailsRequest request, Action<GetOfferDetailsResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetSystemTime(IRpcController controller, NoData request, Action<GetSystemTimeResponse> done)
        {
            Logger.Debug("GetSystemTime()");
            done(GetSystemTimeResponse.CreateBuilder()
                .SetTime((ulong)Clock.UnixTime.TotalMicroseconds)
                .Build());
        }

        public override void CreateCSTrade(IRpcController controller, CreateCSTradeRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void RefundBid(IRpcController controller, RefundRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void ReportRefund(IRpcController controller, ReportRefundRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void GetOrderCount(IRpcController controller, GetOrderCountRequest request, Action<GetOrderCountResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void SubscribeAdvancedOrderUpdate(IRpcController controller, SubscribeAdvancedOrderUpdateRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void UnsubscribeAdvancedOrderUpdate(IRpcController controller, UnsubscribeAdvancedOrderUpdateRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void SettleHistoriesForCS(IRpcController controller, HistoriesForCSRequest request, Action<HistoriesForCSResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void CancelHistoriesForCS(IRpcController controller, HistoriesForCSRequest request, Action<HistoriesForCSResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void CreateCSTradeItem(IRpcController controller, CreateCSTradeItemRequest request, Action<CreateCSTradeResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void CreateCSTradeMoney(IRpcController controller, CreateCSTradeMoneyRequest request, Action<CreateCSTradeResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void EnterExchange(IRpcController controller, EnterRequest request, Action<EnterResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void LeaveExchange(IRpcController controller, LeaveRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }

        public override void GetAuthProgress(IRpcController controller, GetAuthProgressRequest request, Action<GetAuthProgressResponse> done)
        {
            throw new NotImplementedException();
        }

        public override void GetResolutionDetails(IRpcController controller, GetResolutionDetailsRequest request, Action<GetResolutionDetailsResponse> done)
        {
            throw new NotImplementedException();
        }
    }
}

using bnet.protocol;
using bnet.protocol.report;
using Google.ProtocolBuffers;

namespace Rehydrated.Bnet.Services.Implementations
{
    internal class ReportServiceImpl : ReportService, IBnetService
    {
        public ServiceId ServiceId { get; set; }

        public override void SendReport(IRpcController controller, SendReportRequest request, Action<NoData> done)
        {
            throw new NotImplementedException();
        }
    }
}

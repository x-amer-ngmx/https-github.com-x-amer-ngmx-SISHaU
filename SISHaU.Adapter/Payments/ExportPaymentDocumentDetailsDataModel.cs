using Integration.Payment;
using Integration.PaymentServiceAsync;
using SISHaU.Library;
using SISHaU.Library.API;

namespace SISHaU.Adapter.Payments
{
    public class ExportPaymentDocumentDetailsDataModel : Requester<StubService, PaymentPortsTypeAsyncClient>
    {
        public exportPaymentDocumentDetailsRequest CreateRequest()
        {
            var request = GenerateGenericType<exportPaymentDocumentDetailsRequest>();
            return request;
        }

        public getStateResultExportPaymentDocumentDetailsResult ProcessRequest()
        {
            return ProcessRequest<getStateResultExportPaymentDocumentDetailsResult>(CreateRequest());
        }
    }
}

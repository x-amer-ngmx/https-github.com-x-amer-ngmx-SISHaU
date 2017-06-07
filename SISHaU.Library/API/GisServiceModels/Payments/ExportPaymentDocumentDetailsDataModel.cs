using Integration.Payment;
using Integration.PaymentServiceAsync;

namespace SISHaU.Library.API.GisServiceModels.Payments
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

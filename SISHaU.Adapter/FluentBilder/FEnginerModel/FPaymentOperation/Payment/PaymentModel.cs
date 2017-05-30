using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integration.Payment;
using Integration.PaymentsBase;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FPaymentOperation.Payment
{
    public class PaymentModel : BaseModel<PaymentEnginer>
    {
        //Передать перечень документов "Извещение о принятии к исполнению распоряжения"
        private importNotificationsOfOrderExecutionRequestNotificationOfOrderExecutionType PaymentDocuments { get; }

        //Импорт документов "Извещение об аннулировании извещения о принятии к исполнению распоряжения"
        private importNotificationsOfOrderExecutionCancellationRequest ConcellationDocuments;

        //Импорт пакета документов «Извещение о принятии к исполнению распоряжения», размещаемых исполнителем
        private importSupplierNotificationsOfOrderExecutionRequestSupplierNotificationOfOrderExecution ExecutionDocuments;


        private exportPaymentDocumentDetailsRequest GetDocumentInfo;




        public PaymentModel(PaymentEnginer baseModel) : base(baseModel)
        {
            PaymentDocuments = new importNotificationsOfOrderExecutionRequestNotificationOfOrderExecutionType
            {
                TransportGUID = Guid.NewGuid().ToString()
            };
        }

        public PaymentModel PayerInfo(string idPayer, string namePayer)
        {
            PaymentDocuments.SupplierInfo.SupplierID = idPayer;
            PaymentDocuments.SupplierInfo.SupplierName = namePayer;
            return this;
        }

        public PaymentModel DocumentInfo(string idPayer, string namePayer)
        {
            //TODO: Разобраться и реализовать.
            PaymentDocuments.OrderInfo = new NotificationOfOrderExecutionTypeOrderInfo();
            return this;
        }
    }
}

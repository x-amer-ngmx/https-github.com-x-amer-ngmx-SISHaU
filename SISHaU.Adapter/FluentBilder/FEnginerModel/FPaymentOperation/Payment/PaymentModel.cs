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
        private importNotificationsOfOrderExecutionRequestNotificationOfOrderExecutionType PaymentDocument { get; }

        public PaymentModel(PaymentEnginer baseModel) : base(baseModel)
        {
            PaymentDocument = new importNotificationsOfOrderExecutionRequestNotificationOfOrderExecutionType
            {
                TransportGUID = Guid.NewGuid().ToString()
            };
        }

        public PaymentModel PayerInfo(string idPayer, string namePayer)
        {
            PaymentDocument.SupplierInfo.SupplierID = idPayer;
            PaymentDocument.SupplierInfo.SupplierName = namePayer;
            return this;
        }

        public PaymentModel DocumentInfo(string idPayer, string namePayer)
        {
            //TODO: Разобраться и реализовать.
            PaymentDocument.OrderInfo = new NotificationOfOrderExecutionTypeOrderInfo();
            return this;
        }
    }
}

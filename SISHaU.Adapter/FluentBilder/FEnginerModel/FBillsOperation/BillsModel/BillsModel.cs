using System;
using Integration.Bills;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FBillsOperation.BillsModel
{
    public class BillsModel : BaseModel<BillsOperationEnginer>
    {
        //TODO: Реализовать операции.
        importRSOSettlementsRequestImportSettlement stateBills;
        importRSOSettlementsRequestImportSettlementSettlement editStateBills;
        importRSOSettlementsRequestImportSettlementSettlementReportingPeriodAnnulmentReportingPeriod annulateBills;

        exportNotificationsOfOrderExecutionRequest exportBills;
        exportPaymentDocumentRequest exportBillsDoc;
        exportSettlementsRequest exportSettlement;



        public BillsModel(BillsOperationEnginer baseModel) : base(baseModel)
        {
        }

    }
}

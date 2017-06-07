using System;
using Integration.Base;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using SISHaU.Library.API;

namespace SISHaU.Adapter.HouseManagement
{
    public class SupplyResourceContractDataModel : Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        public exportSupplyResourceContractResult ExportRsoData(string contractNumber, string dateStart = null)
        {
            var gisRequest = GenerateGenericType<exportSupplyResourceContractRequest>();
            gisRequest.ContractNumber = contractNumber;
            gisRequest.ContractNumberSpecified = true;

            if (string.IsNullOrEmpty(dateStart)) return ProcessRequest<exportSupplyResourceContractResult>(gisRequest);

            gisRequest.SigningDateStart = DateTime.Parse(dateStart);
            gisRequest.SigningDateStartSpecified = false;

            return ProcessRequest<exportSupplyResourceContractResult>(gisRequest);
        }

        public getStateRequest BeginExportRsoData(string contractNumber, string dateStart = null)
        {
            var gisRequest = GenerateGenericType<exportSupplyResourceContractRequest>();
            gisRequest.ContractNumber = contractNumber;
            gisRequest.ContractNumberSpecified = true;

            if (string.IsNullOrEmpty(dateStart)) return BeginProcessRequest(gisRequest);
            gisRequest.SigningDateStart = DateTime.Parse(dateStart);
            gisRequest.SigningDateStartSpecified = true;

            return BeginProcessRequest(gisRequest);
        }
    }
}

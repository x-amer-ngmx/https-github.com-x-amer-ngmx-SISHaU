using System.Collections.Generic;
using AutoMapper;
using Integration.HouseManagement;
using SISHaU.Library.API;
using ServiceHelperHost.Library.GisServiceModel.HouseManagement;

namespace GisLayer.ServiceModel.HouseManagement
{
    public class zzzHouseManagementHelper : GisBinder
    {
        protected zzzHouseManagementHelper()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMissingTypeMaps = true;
            });

            UtilMapper = config.CreateMapper();
        }

        public importNotificationRequest CreateImportNotificationData(string topic, string content, bool isImportant, bool isShipOff)
        {
            var model = new ImportNotificationDataModel();
            return model.CreateRequest(topic, content, isImportant, isShipOff);
        }

        public importNotificationRequest CreateRemoveNotificationData(string notificationData)
        {
            var model = new ImportNotificationDataModel();
            return model.CreateRequest(notificationData);
        }

        public exportHouseRequest CreateExportHouseRequest(string fiasHouseGuid)
        {
            var model = new ExportHouseDataModel();
            return model.CreateRequest(fiasHouseGuid);
        }

        public importAccountRequest CreateImportAccountRequest(importAccountRequestAccount[] acra)
        {
            var model = new ImportAccountDataModel();
            return model.CreateRequest(acra);
        }

        /*public importSupplyResourceContractRequest CreateImportSupplyResourceContractRequest(List<importSupplyResourceContractRequestContract> contracts)
        {
            var model = new ImportSupplyResourceContractDataModel();
            return model.CreateRequest(contracts);
        }
        */
    }
}

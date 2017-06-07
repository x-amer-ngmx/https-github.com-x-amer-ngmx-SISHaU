using System;
using System.Linq;
using Integration.Base;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using ImportResult = Integration.HouseManagement.ImportResult;

//using ImportResult = Integration.HouseManagement.ImportResult;

namespace GisLayer.ServiceModel.HouseManagement
{
    public class zzzHouseManagementService : zzzHouseManagementHelper
    {
        private readonly HouseManagementPortsTypeClient _houseManagementProxy;
        private readonly HouseManagementPortsTypeAsyncClient _houseManagementAsyncProxy;

        public zzzHouseManagementService()
        {
            _houseManagementProxy = GetProxy<HouseManagementPortsTypeClient>();
            _houseManagementAsyncProxy = GetProxy<HouseManagementPortsTypeAsyncClient>();
        }

        public Integration.HouseManagement.ImportResult ImportHouseRsoData(importHouseRSORequest request)
        {
            var importResult = new Integration.HouseManagement.ImportResult();
            try
            {
                /*Использование GetHeader в данном случае нежелательно, так как есть общий класс обработки сообщений*/
                _houseManagementProxy.importHouseRSOData((RequestHeader)GetHeader(), request, out importResult);
            }
            catch (Exception exc)
            {
                importResult.ErrorMessage = new ErrorMessageType();
            }

            return importResult;
        }

        public Integration.HouseManagement.ImportResult ImportNotificationData(importNotificationRequest request)
        {
            var importResult = new Integration.HouseManagement.ImportResult();
            try
            {
                _houseManagementProxy.importNotificationData((RequestHeader)GetHeader(), request, out importResult);
            }
            catch (Exception exc)
            {
                importResult.ErrorMessage = new ErrorMessageType
                {
                    Description = exc.Message
                };
            }

            CheckAndProcessImportErrors(importResult);
            return importResult;
        }

        public Integration.HouseManagement.ImportResult RemoveNotification(importNotificationRequest request)
        {
            var importResult = new Integration.HouseManagement.ImportResult();
            try
            {
                _houseManagementProxy.importNotificationData((RequestHeader)GetHeader(), request, out importResult);
            }
            catch (Exception exc)
            {
                importResult.ErrorMessage = new ErrorMessageType
                {
                    Description = exc.Message
                };
            }
            CheckAndProcessImportErrors(importResult);
            return importResult;
        }

        public exportHouseResult ExportHouseRsoData(exportHouseRequest request)
        {
            var exportHouseResult = new exportHouseResult();
            try
            {
                _houseManagementProxy.exportHouseData((RequestHeader)GetHeader(), request, out exportHouseResult);
            }
            catch (Exception exc)
            {
                exportHouseResult.ErrorMessage = new ErrorMessageType
                {
                    Description = exc.Message
                };
            }
            HandleError<ErrorMessageType>(exportHouseResult);
            return exportHouseResult;
        }

        public exportHouseResult ExportHouseRsoData(string fiasHouseGuid)
        {
            var exportHouseResult = new exportHouseResult();
            var model = new ExportHouseDataModel();
            try
            {
                /*var result = _houseManagementProxy.GetType().InvokeMember("exportHouseData", 
                    BindingFlags.Public | BindingFlags.InvokeMethod, 
                    null, this, new object[]{ GetHeader(), model.CreateRequest(fiasHouseGuid), exportHouseResult });*/

                //_houseManagementProxy.exportHouseData(GetHeader(), model.CreateRequest(fiasHouseGuid),out exportHouseResult);
            }
            catch (Exception exc)
            {
                exportHouseResult.ErrorMessage = new ErrorMessageType {Description = exc.Message};
            }

            try
            {
                var header = GetHeader();
                var param = new object[] {header, model.CreateRequest(fiasHouseGuid), exportHouseResult};
                var result = _houseManagementProxy.GetType().GetMethod("exportHouseData").Invoke(_houseManagementProxy, param);
                    /*.InvokeMember("exportHouseData",
                    BindingFlags.Public | BindingFlags.InvokeMethod,
                    null, this, new object[] { GetHeader(), model.CreateRequest(fiasHouseGuid), exportHouseResult });*/

                //_houseManagementProxy.exportHouseData(GetHeader(), model.CreateRequest(fiasHouseGuid),out exportHouseResult);
            }
            catch (Exception exc)
            {
                exportHouseResult.ErrorMessage = new ErrorMessageType { Description = exc.Message };
            }

            HandleError<ErrorMessageType>(exportHouseResult);
            return exportHouseResult;
        }

        /// <summary>
        /// Здесь можно вынести importAccountRequestAccount[] acra выше, а можно уже тут указать запрос и маппинг
        /// </summary>
        /// <param name="acra"></param>
        /// <returns></returns>
        public ImportResult ImportAccountData(importAccountRequestAccount[] acra)
        {
            var importResult = new ImportResult();

            try
            {
                _houseManagementProxy.importAccountData((RequestHeader)GetHeader(), CreateImportAccountRequest(acra),
                    out importResult);
            }
            catch (Exception exc)
            {
                importResult.ErrorMessage = new ErrorMessageType{
                    Description = exc.Message
                };
            }

            HandleError<ErrorMessageType>(importResult);
            return importResult;
        }

        public exportAccountResult ExportAccountData(exportAccountRequest request)
        {
            var exportResult = new exportAccountResult();

            try
            {
                _houseManagementProxy.exportAccountData((RequestHeader)GetHeader(), request, out exportResult);
            }
            catch (Exception exc)
            {
                exportResult.ErrorMessage = new ErrorMessageType
                {
                    Description = exc.Message
                };
            }
            
            HandleError<ErrorMessageType>(exportResult);
            return exportResult;
        }

        /*
         * public ImportResult1 ImportSupplyResourceContractData(List<importSupplyResourceContractRequestContract> contracts)
        {
            var importResult = new ImportResult1();

            try
            {
                _houseManagementProxy.importSupplyResourceContractData((RequestHeader)GetHeader(),
                    CreateImportSupplyResourceContractRequest(contracts), out importResult);
            }
            catch (Exception exc)
            {
                importResult.ErrorMessage = new ErrorMessageType{
                    Description = exc.Message
                };
            }

            CheckAndProcessImportErrors(importResult);
            return importResult;
        }
        */
        /// <summary>
        /// Метод проверяет и обрабатывает ошибки на всех уровнях при импорте объектов
        /// </summary>
        /// <param name="importResult"></param>
        /// <returns></returns>
        private bool CheckAndProcessImportErrors(ImportResult importResult)
        {
            //Обработка транспортной ошибки
            var emt = HandleError<ErrorMessageType>(importResult);
            //Обработка ошибки функционала
            var crte = HandleError<CommonResultTypeError>(importResult.CommonResult);
            return emt || crte;
        }

        public object ProcessMethod<T>(string methodName, object request)
        {
            var method = _houseManagementProxy.GetType().GetMethod(methodName);
            var retInstance = Activator.CreateInstance(typeof (T));
            var attrs = new [] {GetHeader(), request, retInstance };
            try
            {
                method.Invoke(_houseManagementProxy, attrs);
                HandleError<ErrorMessageType>(attrs.Last());
            }
            catch (Exception exc)
            {
                HandleError<ErrorMessageType>(attrs.Last());
            }
            
            return attrs.Last();
        }

        public object ProcessMethodAsync<T>(string methodName, object request) where T : class
        {
            var method = _houseManagementAsyncProxy.GetType().GetMethod(methodName);
            var retInstance = new AckRequest();
            var attrs = new[] { GetHeader(), request, retInstance };
            try
            {
                method.Invoke(_houseManagementAsyncProxy, attrs);
                retInstance = attrs.Last() as AckRequest;
                var ret = ProcessAsyncMessage(new getStateRequest {MessageGUID = retInstance.Ack.MessageGUID});
                HandleError<ErrorMessageType>(attrs.Last());
            }
            catch (Exception exc)
            {
                HandleError<ErrorMessageType>(attrs.Last());
            }

            return attrs.Last();
        }

        private object ProcessAsyncMessage(object request)
        {
            var method = _houseManagementAsyncProxy.GetType().GetMethod("getState");
            var stateResult = new getStateResult();
            var attrs = new [] { new RequestHeader(), request, stateResult };
            do
            {
                attrs[0] = GetHeader();
                method.Invoke(_houseManagementAsyncProxy, attrs);
                stateResult = attrs.Last() as getStateResult;
                HandleError<ErrorMessageType>(attrs.Last());
            } while (stateResult.RequestState != 3);

            return attrs.Last();
        }
    }
}

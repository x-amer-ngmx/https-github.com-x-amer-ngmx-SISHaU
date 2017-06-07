using System.Collections.Generic;
using AutoMapper;
using Integration.Base;
using Integration.Nsi;
using Microsoft.Practices.ServiceLocation;
using SISHaU.Library.API.Interfaces;
using SISHaU.Library.Util;

namespace SISHaU.Library.API.GisServiceModels.NsiService
{
    public class ExportDataProviderNsiItemModel : NsiUtil
    {
        private readonly INsiCommonServiceCaching _nsiCommonService;

        public ExportDataProviderNsiItemModel()
        {
            _nsiCommonService = ServiceLocator.Current.GetInstance<INsiCommonServiceCaching>();
            RegistryNumber = new Dictionary<string, exportDataProviderNsiItemRequestRegistryNumber>
            {
                {"1", exportDataProviderNsiItemRequestRegistryNumber.Item1 },
                {"51", exportDataProviderNsiItemRequestRegistryNumber.Item51 },
                {"59", exportDataProviderNsiItemRequestRegistryNumber.Item59 },
                {"219", exportDataProviderNsiItemRequestRegistryNumber.Item219 }
            };

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<exportNsiItemResult, Integration.NsiCommon.exportNsiItemResult>();
            });
            InitMapper(cfg);
        }

        private Dictionary<string, exportDataProviderNsiItemRequestRegistryNumber> RegistryNumber { get; set; }

        private exportNsiItemResult ProcessRequest(string registryNumberKey)
        {
            /*Integration.NsiCommon.exportNsiItemResult - Integration.Nsi.exportNsiItemResult - аналогичны*/
            if (!RegistryNumber.ContainsKey(registryNumberKey)) return new exportNsiItemResult
            {
                ErrorMessage = new ErrorMessageType{
                    Description = $"Запись [{registryNumberKey}] в словаре отсутствует."
                }
            };

            object request = null;

            switch (RegistryNumber[registryNumberKey])
            {
                case exportDataProviderNsiItemRequestRegistryNumber.Item1:
                case exportDataProviderNsiItemRequestRegistryNumber.Item51:
                        request = GenerateGenericType<exportDataProviderNsiItemRequest>();
                    break;
                case exportDataProviderNsiItemRequestRegistryNumber.Item59:
                        request = GenerateGenericType<exportDataProviderNsiPagingItemRequest>();
                        SetPropValue(request, "Page", 1);
                    break;
            }
            
            SetPropValue(request, "RegistryNumber", RegistryNumber[registryNumberKey]);

            return _nsiCommonService.ProcessRequest<exportNsiItemResult>(request);
        }

        public Dictionary<string, string> GetList<T>(string itemRegistryNumber, string strFilter)
        {
            var response = ProcessRequest(itemRegistryNumber);
            return null == response?.NsiItem ? null : GetNsiList(UtilMapper.Map<Integration.NsiCommon.exportNsiItemResult>(response), strFilter);
        }
    }
}

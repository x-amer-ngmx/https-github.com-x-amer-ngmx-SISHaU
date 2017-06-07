using System.Collections.Generic;
using System.Linq;
using Integration.Nsi;
using Integration.Base;

namespace SISHaU.Library.API.GisServiceModels.NsiService
{
    public class ImportAdditionalServicesModel : Requester<Integration.NsiService.NsiPortsTypeClient, Integration.NsiServiceAsync.NsiPortsTypeAsyncClient>
    {
        public ImportResult ImportAdditionalService()
        {
            var request = new importAdditionalServicesRequest
            {
                ImportAdditionalServiceType = new[]
                {
                    new importAdditionalServicesRequestImportAdditionalServiceType
                    {
                        TransportGUID = TransportGuid,
                        AdditionalServiceTypeName = "",
                        ElementGuid = "",
                        OKEI = "",
                        StringDimensionUnit = ""
                    }
                }
            };

            return ProcessRequest<ImportResult>(request);
        }

        public ImportResult RecoverAdditionalService(List<string> elementGuids)
        {
            var request = new importAdditionalServicesRequest
            {
                RecoverAdditionalServiceType = elementGuids.Select(t => new importAdditionalServicesRequestRecoverAdditionalServiceType{
                    TransportGUID = TransportGuid,
                    ElementGuid = t
                }).ToArray()
            };

            return ProcessRequest<ImportResult>(request);
        }

        public ImportResult DeleteAdditionalService(List<string> elementGuids)
        {
            var request = new importAdditionalServicesRequest{
                DeleteAdditionalServiceType = elementGuids.Select(t => new importAdditionalServicesRequestDeleteAdditionalServiceType{
                    TransportGUID = TransportGuid,
                    ElementGuid = t
                }).ToArray()
            };

            return ProcessRequest<ImportResult>(request);
        }
    }
}

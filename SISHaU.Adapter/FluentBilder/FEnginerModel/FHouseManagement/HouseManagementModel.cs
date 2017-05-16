using System.Collections.Generic;
using System.Linq;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.Account;
using SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.MeteringDevice;
using SISHaU.Library.API;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement
{
    public class HouseManagementModel : Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        public IList<importAccountRequestAccount> Accounts { get; set; }
        public IDictionary<string, importMeteringDeviceDataRequest> MeteringDeviceImport { get; set; }
        public IList<exportMeteringDeviceDataRequest> MeteringDeviceExport { get; set; }

        public ImportResult ImportAccountResult { get; set; }
        public IList<exportMeteringDeviceDataResult> MetringDeviceExportResult { get; set; }
        public IList<ImportResult> MetringDeviceImportResult { get; set; }

        public AccountModel Account()
        {
            return new AccountModel(this);
        }

        public MeteringDeviceModel MeteringDevice()
        {
            return new MeteringDeviceModel(this);
        }

        public HouseManagementModel Sync(bool clearPool = false)
        {
            if (null != Accounts && Accounts.Any())
            {
                var request = GenerateGenericType<importAccountRequest>();
                request.Account = Accounts.ToArray();
                ImportAccountResult = ProcessRequest<ImportResult>(request);
                if (clearPool)
                    Accounts.Clear();
            }

            if (null != MeteringDeviceExport && MeteringDeviceExport.Any())
            {
                if (null == MetringDeviceExportResult)
                    MetringDeviceExportResult = new List<exportMeteringDeviceDataResult>();
                foreach (var meteringDeviceHouseExport in MeteringDeviceExport)
                {
                    MetringDeviceExportResult.Add(ProcessRequest<exportMeteringDeviceDataResult>(meteringDeviceHouseExport));
                }

                if (clearPool)
                    MeteringDeviceExport.Clear();
            }

            if (null != MeteringDeviceImport && MeteringDeviceImport.Any())
            {
                if (null == MetringDeviceImportResult)
                    MetringDeviceImportResult = new List<ImportResult>();
                foreach (var importRequest in MeteringDeviceImport)
                {
                    MetringDeviceImportResult.Add(ProcessRequest<ImportResult>(importRequest.Value));
                }

                if (clearPool)
                    MeteringDeviceImport.Clear();
            }

            return this;
        }
    }
}

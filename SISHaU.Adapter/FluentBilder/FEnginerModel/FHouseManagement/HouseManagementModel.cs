using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement
{
    public class HouseManagementModel //: Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        //public IList<importAccountRequestAccount> Accounts { get; set; }
        //public IDictionary<string, IList<importMeteringDeviceDataRequestMeteringDevice>> MeteringDeviceHouses { get; set; }
        //public exportMeteringDeviceDataRequest MetringDeviceExport { get; set; }

        //public ImportResult ImportAccountResult { get; set; }
        //public ImportResult ImportMeteringDeviceResult { get; set; }
        //public exportMeteringDeviceDataResult MetringDeviceExportResult { get; set; }

        //public AccountModel Account()
        //{
        //    if (null == Accounts)
        //        Accounts = new List<importAccountRequestAccount>();

        //    return new AccountModel(this);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="fiasHouseGuid">Обрати внимание, что fiasHouseGuid всё-таки идёт на один дом, и если его в цикле поменять, 
        ///// то все приборы учёта тоже перейдут на новый дом. Чтобы в цикле изменить дом придётся отправлять запрос на сервер и очищать список приборов учёта.
        ///// Сделал словарик)))</param>
        ///// <returns></returns>
        //public MeteringDeviceModel MeteringDevice(string fiasHouseGuid)
        //{
        //    if (null == MeteringDeviceHouses)
        //        MeteringDeviceHouses = new Dictionary<string, IList<importMeteringDeviceDataRequestMeteringDevice>>();

        //    if (!MeteringDeviceHouses.ContainsKey(fiasHouseGuid))
        //        MeteringDeviceHouses.Add(fiasHouseGuid, new List<importMeteringDeviceDataRequestMeteringDevice>());

        //    return new MeteringDeviceModel(this, fiasHouseGuid);
        //}

        //public HouseManagementModel Sync(bool clearPool = false)
        //{
        //    if (null != Accounts && Accounts.Any())
        //    {
        //        var request = GenerateGenericType<importAccountRequest>();
        //        request.Account = Accounts.ToArray();
        //        ImportAccountResult = ProcessRequest<ImportResult>(request);
        //        if (clearPool)
        //            Accounts.Clear();
        //    }

        //    if (null != MeteringDeviceHouses && MeteringDeviceHouses.Any())
        //    {
        //        /*Здесь вообще можно очень гибко менять режимы работы с гис*/
        //        foreach (var meteringDeviceHouse in MeteringDeviceHouses)
        //        {
        //            var request = GenerateGenericType<importMeteringDeviceDataRequest>();
        //            request.FIASHouseGuid = meteringDeviceHouse.Key;
        //            request.MeteringDevice = meteringDeviceHouse.Value.ToArray();
        //            ImportMeteringDeviceResult = ProcessRequest<ImportResult>(request);
        //        }

        //        if (clearPool)
        //            MeteringDeviceHouses.Clear();
        //    }

        //    if (null != MetringDeviceExport)
        //    {
        //        MetringDeviceExportResult = ProcessRequest<exportMeteringDeviceDataResult>(MetringDeviceExport);
        //    }

        //    return this;
        //}
    }
}

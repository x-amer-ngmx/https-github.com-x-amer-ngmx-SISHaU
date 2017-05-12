using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement
{
    public class MeteringDeviceCreate : BaseModel<HouseManagementModel>
    {
        //private MeteringDeviceFullInformationType DeviceData { get; set; }
        //private importMeteringDeviceDataRequestMeteringDevice MeteringDevices { get; set; }

        public MeteringDeviceCreate(HouseManagementModel baseModel/*, importMeteringDeviceDataRequestMeteringDevice meteringDevices*/) : base(baseModel)
        {/*
            MeteringDevices = meteringDevices;
            DeviceData = new MeteringDeviceFullInformationType();*/
        }
    }
}

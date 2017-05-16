using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Integration.HouseManagement;
using Integration.NsiBase;
using SISHaU.Library.Util;
using SISHaU.Library.API;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel.FHouseManagement.MeteringDevice
{
    public class DeviceDataToCreate : GisUtil
    {
        private importMeteringDeviceDataRequestMeteringDevice MeteringDevice { get; set; }
        private NsiCommonDataModel NsiCommonModel { get; }
        public DeviceDataToCreate()
        {
            MeteringDevice = new importMeteringDeviceDataRequestMeteringDevice{
                TransportGUID = TransportGuid,
                DeviceDataToCreate = new MeteringDeviceFullInformationType
                {
                    NotLinkedWithMetering = true,
                    NotLinkedWithMeteringSpecified = true
                }
            };

            NsiCommonModel = new NsiCommonDataModel();
        }
        
        public DeviceDataToCreate SetBasicCharacteristics(BasicCharacteristics basicCharacteristics)
        {
            MeteringDevice.DeviceDataToCreate.BasicChatacteristicts = basicCharacteristics.Build();
            return this;
        }

        public importMeteringDeviceDataRequestMeteringDevice Build()
        {
            return MeteringDevice;
        }

        public DeviceDataToCreate SetMeteringBaseValue(string resourceName, string meteringValue)
        {
            MeteringDevice.DeviceDataToCreate.MunicipalResourceNotEnergy = new MunicipalResourceNotElectricType{
                MunicipalResource = NsiCommonModel.GetMeteringDeviceMunicipalResource(resourceName),
                MeteringValue = decimal.Parse(meteringValue)
            };
            return this;
        }
    }
}

using System;
using System.Linq.Expressions;
using System.Reflection;
using Integration.HouseManagement;
using SISHaU.Library.Util;

namespace SISHaU.Library.API.GisServiceModels.HouseManagement.MeteringDevice
{
    public class BasicCharacteristics : GisUtil
    {
        private NsiCommonDataModel NsiModel { get; }
        private MeteringDeviceBasicCharacteristicsType BasicCharacteristicsValues { get; }

        public BasicCharacteristics()
        {
            NsiModel = new NsiCommonDataModel();
            BasicCharacteristicsValues = new MeteringDeviceBasicCharacteristicsType();
        }

        public BasicCharacteristics SetVerificationInterval(sbyte verInterval)
        {
            
            BasicCharacteristicsValues.VerificationInterval = NsiModel.GetVerificationInterval(verInterval);
            return this;
        }

        public BasicCharacteristics SetResidentalPremiseDevice(ResidentialPremiseDevice rpDevice)
        {
            BasicCharacteristicsValues.ResidentialPremiseDevice = rpDevice.Buid();
            return this;
        }

        public BasicCharacteristics SetCollectiveDevice(CollectiveDevice cDevice)
        {
            BasicCharacteristicsValues.CollectiveDevice = cDevice.Build();
            return this;
        }

        public BasicCharacteristics Set(Expression<Func<BasicCharacteristicsSetValues, object>> property, object value)
        {
            PropertyInfo propertyInfo = null;

            var propertyMemberExpression = property.Body as MemberExpression;

            if (null != propertyMemberExpression)
            {
                propertyInfo = ((MemberExpression)property.Body).Member as PropertyInfo;
            }
            else
            {
                var operandMemberExpression = ((UnaryExpression)property.Body).Operand as MemberExpression;
                if (null != operandMemberExpression)
                {
                    propertyInfo = operandMemberExpression.Member as PropertyInfo;
                }
            }

            if (null == propertyInfo) return this;

            SetPropValue(BasicCharacteristicsValues, propertyInfo.Name, value);

            //propertyInfo.SetValue(BasicCharacteristicsValues, value, null);

            return this;
        }

        public MeteringDeviceBasicCharacteristicsType Build()
        {
            return BasicCharacteristicsValues;
        }
    }
}

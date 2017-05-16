using System.Collections.Generic;
using System.Linq;
using Integration.NsiBase;
using Integration.NsiCommon;
using Microsoft.Practices.ServiceLocation;
using SISHaU.Library.API.Interfaces;
using SISHaU.Library.Util;

namespace SISHaU.Library.API
{
    public class NsiCommonDataModel : NsiUtil
    {
        private readonly INsiCommonServiceCaching _nsiCommonService;

        public NsiCommonDataModel()
        {
            _nsiCommonService = ServiceLocator.Current.GetInstance<INsiCommonServiceCaching>();
        }

        private static bool IsNsiRao(string registryNumber)
        {
            return
                "67;70;97;98;99;100;101;102;103;105;106;107;108;109;110;111;112;113;114;115;116;117;118;119;120;121;122;123;124;125;126;127;128;129;130;131;132;133;134;135;136;137;138;139;140;141;142;143;145;146;147;148;149;150;151;152;153;154;155;156;157;158;159;160;161;162;163;164;165;166;167;168;169;170;171;172;173;174;175;176;177;178;179;180;181;182;183;184;185;186;187;188;189;190;191;192;193;195;196;197;205;208;209;210;246;248;249;250;251;252;253;254;255;256;257;258;259;261;265;266;273;284;285;287;288;289;300;299".Split(';')
                    .Contains(registryNumber);
        }

        private exportNsiItemResult ProcessRequest(string registryNumber)
        {
            return _nsiCommonService.ProcessRequest<exportNsiItemResult>(
                new exportNsiItemRequest
                {
                    Id = SignId,
                    RegistryNumber = registryNumber,
                    ListGroup = IsNsiRao(registryNumber) ? ListGroup.NSIRAO : ListGroup.NSI
                });
        }

        private nsiRef GetNsiRef(string itemRegistryNumber, string strFilterList, string strFilterItem)
        {
            var response = ProcessRequest(itemRegistryNumber);
            return null == response.NsiItem ? null : GetNsiRef(response, strFilterList, strFilterItem);
        }

        public Dictionary<string, nsiRef> GetNsiRefList(string itemRegistryNumber, string strFilter = "")
        {
            var response = ProcessRequest(itemRegistryNumber);
            return null == response.NsiItem ? null : GetNsiRefList(response, strFilter);
        }

        public Dictionary<string, string> GetList(string itemRegistryNumber, string strFilter = "")
        {
            var response = ProcessRequest(itemRegistryNumber);
            return null == response.NsiItem ? null : GetNsiList(response, strFilter);
        }

        /// <summary>
        /// Вид коммунальной услуги
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetServiceTypes(List<string> filter)
        {
            return GetList("3", "Вид коммунальной услуги").Where(t => filter.Contains(t.Key)).ToDictionary(t => t.Key, t => t.Value);
        }

        /// <summary>
        /// Тарифицируемый ресурс
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetMunicipalResources(List<string> filter)
        {
            return GetList("239", "Тарифицируемый ресурс").Where(t => filter.Contains(t.Key)).ToDictionary(t => t.Key, t => t.Value);
        }

        /// <summary>
        /// Часовые зоны по Olson
        /// </summary>
        /// <returns></returns>
        public nsiRef GetOlsonTimeZone()
        {
            return GetNsiRef("32", "Часовая зона", "Asia/Vladivostok");
        }

        /// <summary>
        /// Причина закрытия для лицевого
        /// </summary>
        /// <param name="reason"></param>
        /// <returns></returns>
        public nsiRef GetClosedReason(string reason)
        {
            return GetNsiRef("22", "Причина закрытия лицевого счета", reason);
        }

        public nsiRef GetAdditionalService(string serviceName)
        {
            var a1 = GetNsiRef("1", "Вид дополнительной услуги", serviceName);
            var a2 = GetNsiRef("51", "Главная коммунальная услуга", serviceName);

            return new nsiRef();
        }

        public nsiRef GetVerificationInterval(sbyte intervalValue)
        {
            return GetNsiRef("16", "Межповерочный интервал", intervalValue.ToString());
        }

        public nsiRef GetMeteringDeviceMunicipalResource(string resourceName)
        {
            return GetNsiRef("2", "Вид коммунального ресурса", resourceName);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Integration.Base;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using SISHaU.Library.API;

namespace ServiceHelperHost.Library.GisServiceModel.HouseManagement
{
    public class ImportSupplyResourceContractDataModel : Requester<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>
    {
        //Дата окончания действия
        private DateTime CompletionDate { get; }
        private DateTime StartSupplyDate { get; }
        private importSupplyResourceContractRequest GisRequest { get; }
        private List<SupplyResourceContractTypeContractSubject> ContractSubjects { get; }
        private Dictionary<string, string> ServiceType { get; }
        private Dictionary<string, string> MunicipalResource { get; }
        private SupplyResourceContractTypeObjectAddressPair [] Pair { get; set; }
        private List<SupplyResourceContractTypeObjectAddress> ObjectAddress { get; set; }

        public ImportSupplyResourceContractDataModel(exportSupplyResourceContractResultType mappingContractType = null)
        {
            GisRequest = GenerateGenericType<importSupplyResourceContractRequest>();
            GisRequest.Contract = new[] { new importSupplyResourceContractRequestContract{ TransportGUID = TransportGuid } };

            //Автоматом заполняю стандартные услуги и ресурсы для договоров
            var services = new List<string> { "Отопление", "Горячее водоснабжение", "Холодное водоснабжение", "Отведение сточных вод", "Электроснабжение" };
            var municipalResources = new List<string> { "Горячая вода", "Сточные воды", "Тепловая энергия", "Теплоноситель", "Техническая вода", "Питьевая вода", "Электрическая энергия" };
            var nsi = new NsiCommonDataModel();
            ServiceType = nsi.GetServiceTypes(services);
            MunicipalResource = nsi.GetMunicipalResources(municipalResources);

            if (mappingContractType != null)
            {
                var mapConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMissingTypeMaps = true;

                    cfg.CreateMap<exportSupplyResourceContractResultType, SupplyResourceContractType>();
                        
                    cfg.CreateMap<ExportSupplyResourceContractTypeApartmentBuildingOwner, SupplyResourceContractTypeApartmentBuildingOwner>();
                    cfg.CreateMap<ExportSupplyResourceContractTypeLivingHouseOwner, SupplyResourceContractTypeLivingHouseOwner>();

                    cfg.CreateMap<ExportSupplyResourceContractTypeObjectAddressPairHeatingSystemType, SupplyResourceContractTypeObjectAddressPairHeatingSystemType>();
                    cfg.CreateMap<ExportSupplyResourceContractTypeObjectAddressPair, SupplyResourceContractTypeObjectAddressPair>();
                    cfg.CreateMap<ExportSupplyResourceContractTypeObjectAddress, SupplyResourceContractTypeObjectAddress>();

                    cfg.CreateMap<ExportSupplyResourceContractTypeContractSubjectServiceType, ContractSubjectTypeServiceType>();
                    cfg.CreateMap<ExportSupplyResourceContractTypeContractSubjectMunicipalResource, ContractSubjectTypeMunicipalResource>();
                    cfg.CreateMap<ExportSupplyResourceContractTypeContractSubjectPlannedVolume, ContractSubjectTypePlannedVolume>();
                    cfg.CreateMap<ExportSupplyResourceContractTypeContractSubject, SupplyResourceContractTypeContractSubject>();

                    cfg.CreateMap<ExportSupplyResourceContractTypeIsContract, SupplyResourceContractTypeIsContract>();
                    cfg.CreateMap<ExportSupplyResourceContractTypeIsNotContract, SupplyResourceContractTypeIsNotContract>();

                    cfg.CreateMap<ExportSupplyResourceContractTypePeriodStart, SupplyResourceContractTypePeriodStart>();
                    cfg.CreateMap<ExportSupplyResourceContractTypePeriodEnd, SupplyResourceContractTypePeriodEnd>();
                    cfg.CreateMap<ExportSupplyResourceContractTypePeriod, SupplyResourceContractTypePeriod>();

                    cfg.CreateMap<ExportSupplyResourceContractTypeBillingDate, SupplyResourceContractTypeBillingDate>();
                    cfg.CreateMap<ExportSupplyResourceContractTypePaymentDate, SupplyResourceContractTypePaymentDate>();
                    cfg.CreateMap<ExportSupplyResourceContractTypeProvidingInformationDate, SupplyResourceContractTypeProvidingInformationDate>();
                });

                InitMapper(mapConfig);

                GisRequest.Contract.First().ContractGUID = mappingContractType.ContractGUID;
                GisRequest.Contract.First().SupplyResourceContract = UtilMapper.Map<SupplyResourceContractType>(mappingContractType);
                
                CompletionDate = GisRequest.Contract.First().SupplyResourceContract.IsNotContract.EffectiveDate;
                StartSupplyDate = GisRequest.Contract.First().SupplyResourceContract.IsNotContract.SigningDate;

                return;
            }

            CompletionDate = DateTime.Parse("31.12.3001");
            StartSupplyDate = DateTime.Now;

            ContractSubjects = new List<SupplyResourceContractTypeContractSubject>
            {
                GetContractSubject("Отопление", "Тепловая энергия"),
                GetContractSubject("Горячее водоснабжение", "Горячая вода"),
                GetContractSubject("Холодное водоснабжение", "Питьевая вода"),
                GetContractSubject("Отведение сточных вод", "Сточные воды"),
                GetContractSubject("Электроснабжение", "Электрическая энергия")
            };

            Pair = ContractSubjects.Select(t => new SupplyResourceContractTypeObjectAddressPair
            {
                StartSupplyDate = t.StartSupplyDate,
                EndSupplyDate = t.EndSupplyDate,
                HeatingSystemType = new SupplyResourceContractTypeObjectAddressPairHeatingSystemType
                {
                    CentralizedOrNot =
                        SupplyResourceContractTypeObjectAddressPairHeatingSystemTypeCentralizedOrNot.Centralized,
                    OpenOrNot = SupplyResourceContractTypeObjectAddressPairHeatingSystemTypeOpenOrNot.Closed
                },
                PairKey = t.TransportGUID
            }).ToArray();

            ObjectAddress = new List<SupplyResourceContractTypeObjectAddress>();
        }

        public void GenerateNewContract(string contractNumber)
        {
            GisRequest.Contract.First().SupplyResourceContract = new SupplyResourceContractType
            {
                IsNotContract = new SupplyResourceContractTypeIsNotContract {
                    SigningDate = StartSupplyDate,
                    EffectiveDate = StartSupplyDate,
                    ContractNumber = contractNumber
                },
                IsPlannedVolume = false,
                Offer = true,
                ContractSubject = ContractSubjects.ToArray(),
                Period = new SupplyResourceContractTypePeriod
                {
                    Start = new SupplyResourceContractTypePeriodStart {StartDate = 10},
                    End = new SupplyResourceContractTypePeriodEnd {EndDate = 26}
                },
                BillingDate = new SupplyResourceContractTypeBillingDate{
                    Date = 1,
                    DateType = SupplyResourceContractTypeBillingDateDateType.N
                },
                PaymentDate = new SupplyResourceContractTypePaymentDate{
                    Date = 10,
                    DateType = SupplyResourceContractTypePaymentDateDateType.N
                },
                ProvidingInformationDate = new SupplyResourceContractTypeProvidingInformationDate{
                    Date = 10,
                    DateType = SupplyResourceContractTypeProvidingInformationDateDateType.N
                },
                ComptetionDate = CompletionDate,
                CountingResource = SupplyResourceContractTypeCountingResource.R
            };

            ObjectAddress = new List<SupplyResourceContractTypeObjectAddress>();
        }

        public SupplyResourceContractTypeObjectAddress GetObjectAddress(string compositeAddressString)
        {
            var parts = compositeAddressString.Split('=');

            var address = new SupplyResourceContractTypeObjectAddress
            {
                TransportGUID = TransportGuid,
                FIASHouseGuid = parts[1],
                Pair = Pair
            };

            if (string.IsNullOrEmpty(parts[2]) || string.IsNullOrEmpty(parts[1]))
                return address;

            if (parts[0].Equals("МКД"))
                address.ApartmentNumber = parts[2];

            return address;
        }

        private SupplyResourceContractTypeContractSubject GetContractSubject(string serviceTypeKey, string municipalResourceKey)
        {
            return new SupplyResourceContractTypeContractSubject
            {
                TransportGUID = TransportGuid,
                StartSupplyDate = StartSupplyDate,
                EndSupplyDate = CompletionDate,
                ServiceType = new ContractSubjectTypeServiceType
                {
                    Code = "3", GUID = ServiceType[serviceTypeKey]
                },
                MunicipalResource = new ContractSubjectTypeMunicipalResource
                {
                    Code = "239", GUID = MunicipalResource[municipalResourceKey]
                }
            };
        }

        public importSupplyResourceContractRequest GetRequest()
        {
            GisRequest.Contract.First().SupplyResourceContract.ObjectAddress = ObjectAddress.ToArray();
            return GisRequest;
        }

        public void SetContracts(List<importSupplyResourceContractRequestContract> contracts)
        {
            GisRequest.Contract = contracts.ToArray();
        }

        public void AddObjectAddress(string compositeAddressString)
        {
            ObjectAddress.Add(GetObjectAddress(compositeAddressString));
        }

        public Integration.Base.ImportResult ProcessRequest()
        {
            return ProcessRequest<Integration.Base.ImportResult>(GetRequest());
        }

        public getStateRequest BeginProcessRequest() => BeginProcessRequest(GetRequest());
        
    }
}

    //cfg.AllowNullCollections = true;
    //cfg.AllowNullDestinationValues = true;
    //cfg.CreateMissingTypeMaps = false;
    //cfg.CreateMap<, >();
    //cfg.CreateMap<, >();
    //cfg.CreateMap<, >();
    /*cfg.CreateMap<ExportSupplyResourceContractTypeIsContract, SupplyResourceContractTypeIsContract>();
    cfg.CreateMap<ExportSupplyResourceContractTypeIsNotContract, SupplyResourceContractTypeIsNotContract>();
    cfg.CreateMap<ExportSupplyResourceContractTypePeriodStart, SupplyResourceContractTypePeriodStart> ();
    cfg.CreateMap<ExportSupplyResourceContractTypePeriodEnd, SupplyResourceContractTypePeriodEnd> ();
    cfg.CreateMap<ExportSupplyResourceContractTypePeriod, SupplyResourceContractTypePeriod> ();
    cfg.CreateMap<ExportSupplyResourceContractTypeApartmentBuildingOwner, SupplyResourceContractTypeApartmentBuildingOwner> ();
    cfg.CreateMap<ExportSupplyResourceContractTypeLivingHouseOwner, SupplyResourceContractTypeLivingHouseOwner> ();
    cfg.CreateMap<ExportSupplyResourceContractTypeOrganization, SupplyResourceContractTypeOrganization> ();
    cfg.CreateMap<ExportSupplyResourceContractTypeContractSubjectServiceType, ContractSubjectTypeServiceType> ();
    cfg.CreateMap<ExportSupplyResourceContractTypeContractSubjectMunicipalResource, ContractSubjectTypeMunicipalResource> ();
    cfg.CreateMap<ExportSupplyResourceContractTypeContractSubjectPlannedVolume, ContractSubjectTypePlannedVolume> ();
    cfg.CreateMap<ExportSupplyResourceContractTypeContractSubject, SupplyResourceContractTypeContractSubject>();
    cfg.CreateMap<ExportSupplyResourceContractTypeObjectAddressPairHeatingSystemType, SupplyResourceContractTypeObjectAddressPairHeatingSystemType>();
    cfg.CreateMap<ExportSupplyResourceContractTypeObjectAddressPair, SupplyResourceContractTypeObjectAddressPair>();
    cfg.CreateMap<ExportSupplyResourceContractTypeObjectAddress, SupplyResourceContractTypeObjectAddress>();
    cfg
        .CreateMap
        <ExportSupplyResourceContractTypeQualityIndicatorValue,
            SupplyResourceContractTypeQualityIndicatorValue>();
    cfg.CreateMap<ExportSupplyResourceContractTypeQuality, SupplyResourceContractTypeQuality>();
    cfg.CreateMap<ExportSupplyResourceContractTypeOtherQualityIndicator, SupplyResourceContractTypeOtherQualityIndicator>();
    cfg.CreateMap<ExportSupplyResourceContractTypeTemperatureChart, SupplyResourceContractTypeTemperatureChart>();
    cfg.CreateMap<ExportSupplyResourceContractTypeBillingDate, SupplyResourceContractTypeBillingDate>();
    cfg.CreateMap<ExportSupplyResourceContractTypePaymentDate, SupplyResourceContractTypePaymentDate>();
    cfg.CreateMap<ExportSupplyResourceContractTypeProvidingInformationDate, SupplyResourceContractTypeProvidingInformationDate>();

    cfg.CreateMap<exportSupplyResourceContractResultType, SupplyResourceContractType>();*/

    //3
    //ServiceType = new Dictionary<string, string>
    //{
    //    { "Отопление",              "74925764-ddf3-4b4b-b18d-85994187c13a"},
    //    { "Горячее водоснабжение",  "ee8c6a41-aaf8-41c8-a1f6-5832cc622f88"},
    //    { "Холодное водоснабжение", "78923e8e-10e0-45d7-8ef3-760bc5cfae50"},
    //    { "Отведение сточных вод",  "9d3abb9e-b0fc-4ae5-8cd4-693e4fce6ce6"},
    //    { "Электроснабжение",       "f7e7c7ca-78cf-41ba-9d13-622ee263f064"}
    //};

    //239
    //MunicipalResource = new Dictionary<string, string>
    //{
    //    { "Горячая вода",           "2a417c1b-00a2-48a1-a473-3cb5c2adc644"},
    //    { "Сточные воды",           "c7acc163-d436-423d-b523-a075490c943f"},
    //    { "Тепловая энергия",       "eec6e4b8-76c8-4fce-99b7-c95718edad19"},
    //    { "Теплоноситель",          "3944b8a7-7257-4a3a-a6ac-382e78914ecd"},
    //    { "Техническая вода",       "c8c2e260-14ca-4069-a116-09539fdca327"},
    //    { "Питьевая вода",          "b6352012-ecc8-4542-8dc9-66c319a143eb"},
    //    { "Электрическая энергия",  "b9f4e15e-9c64-4509-9bd1-669b5eac498e"}
    //};

    /*.ForMember(s => s.Quality, o => o.Ignore())
                        .ForMember(s => s.OtherQualityIndicator, o => o.Ignore())
                        .ForMember(s => s.TemperatureChart, o => o.Ignore())*/;

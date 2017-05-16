using Autofac;
using AutoMapper;
using Integration.Base;
using Integration.BaseDto;
using Integration.BillsService;
using Integration.BillsServiceAsync;
using Integration.DeviceMeteringService;
using Integration.DeviceMeteringServiceAsync;
using Integration.HouseManagement;
using Integration.HouseManagementService;
using Integration.HouseManagementServiceAsync;
using Integration.InfrastructureService;
using Integration.InfrastructureServiceAsync;
using Integration.NsiBase;
using Integration.NsiBaseDto;
using Integration.NsiCommon;
using Integration.NsiCommonDto;
using Integration.NsiCommonService;
using Integration.NsiCommonServiceAsync;
using Integration.PaymentServiceAsync;
using NHibernate;
using SISHaU.DataAccess;
using SISHaU.Library.API.Interfaces;
using SISHaU.Library.File;

namespace SISHaU.Library.API
{
    public static class ServiceRegistrator
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                /*cfg.CreateMap<getStateResult, exportHouseResult>()
                    .ForMember(d => d.exportHouseResultType, o => o.MapFrom(s => s.exportHouseResult));*/

                //cfg.CreateMissingTypeMaps = true;
                //cfg.AllowNullDestinationValues = true;
                //cfg.AllowNullCollections = true;
                //cfg.CreateMap<Integration.NsiCommon.getStateResult, exportNsiItemResult>();
                cfg.CreateMap<Integration.NsiCommon.getStateResult, exportNsiItemResultDto>();
                cfg.CreateMap<Integration.NsiCommon.getStateResult, exportNsiListResultDto>();

                cfg.CreateMap<Integration.Nsi.getStateResult, exportNsiItemResult>();
                cfg.CreateMap<Integration.NsiCommon.getStateResult, exportNsiItemResult>();
                cfg.CreateMap<Integration.NsiCommon.getStateResult, exportNsiListResult>();
                cfg.CreateMap<Integration.Nsi.getStateResult, Integration.Nsi.exportNsiItemResult>();

                cfg.CreateMap<exportNsiListResult, exportNsiListResultDto>()
                    .ForMember(o => o.Id, m => m.MapFrom(s => s.Id)).ReverseMap();

                cfg.CreateMap<NsiListType, NsiListTypeDto>().ReverseMap();

                cfg.CreateMap<NsiItemInfoType, NsiItemInfoTypeDto>().ReverseMap();

                cfg.CreateMap<exportNsiItemResult, exportNsiItemResultDto>()
                    .ForMember(o => o.Id, m => m.MapFrom(s => s.Id)).ReverseMap();

                cfg.CreateMap<Integration.Nsi.exportNsiItemResult, exportNsiItemResultDto>()
                    .ForMember(o => o.Id, m => m.MapFrom(s => s.Id)).ReverseMap(); ;

                cfg.CreateMap<NsiItemType, NsiItemTypeDto>().ReverseMap();

                cfg.CreateMap<NsiElementType, NsiElementTypeDto>().ReverseMap();

                cfg.CreateMap<NsiElementFieldType, NsiElementFieldTypeDto>().ReverseMap();

                cfg.CreateMap<NsiElementStringFieldType, NsiElementStringFieldTypeDto>().ReverseMap();
                cfg.CreateMap<NsiElementNsiRefFieldType, NsiElementNsiRefFieldTypeDto>().ReverseMap();
                cfg.CreateMap<NsiElementIntegerFieldType, NsiElementIntegerFieldTypeDto>().ReverseMap();
                cfg.CreateMap<NsiElementBooleanFieldType, NsiElementBooleanFieldTypeDto>().ReverseMap();
                cfg.CreateMap<NsiElementOkeiRefFieldType, NsiElementOkeiRefFieldTypeDto>().ReverseMap();

                cfg.CreateMap<NsiElementFieldType, NsiElementFieldTypeDto>()
                    .Include<NsiElementStringFieldType, NsiElementStringFieldTypeDto>()
                    .Include<NsiElementNsiRefFieldType, NsiElementNsiRefFieldTypeDto>()
                    .Include<NsiElementIntegerFieldType, NsiElementIntegerFieldTypeDto>()
                    .Include<NsiElementBooleanFieldType, NsiElementBooleanFieldTypeDto>()
                    .Include<NsiElementOkeiRefFieldType, NsiElementOkeiRefFieldTypeDto>().ReverseMap();

                cfg.CreateMap<NsiElementNsiRefFieldTypeNsiRef, NsiElementNsiRefFieldTypeNsiRefDto>().ReverseMap();
                cfg.CreateMap<nsiRef, nsiRefDto>().ReverseMap();

                cfg.CreateMap<ErrorMessageType, ErrorMessageTypeDto>().ReverseMap();

                /*cfg.CreateMap<Integration.API.HouseManagement.getStateResult, Im>()
                    .ForMember(d => d.Contract, o => o.MapFrom(t => t.ImportResult.CommonResult));*/

                cfg.CreateMap<Integration.HouseManagement.getStateResult, exportSupplyResourceContractResult>()
                    .ForMember(d => d.Contract, o => o.MapFrom(t => t.exportSupplyResourceContractResult));

                cfg.CreateMap<Integration.HouseManagement.getStateResult, exportHouseResult>()
                    .ForMember(d => d.exportHouseResultType, o => o.MapFrom(t => t.exportHouseResult));

                cfg
                    .CreateMap
                    <Integration.HouseManagement.getStateResult, exportAccountResult>()
                    .ForMember(d => d.Accounts, o => o.MapFrom(t => t.exportAccountResult));

                cfg.CreateMap<Integration.HouseManagement.getStateResult, Integration.HouseManagement.ImportResult>()
                    .ForMember(d => d.CommonResult, o => o.MapFrom(t => t.ImportResult.CommonResult))
                    .ForMember(d => d.ErrorMessage, o => o.MapFrom(t => t.ImportResult.ErrorMessage));
            });

            //mapConfig.AssertConfigurationIsValid();

            var nhSessionFactory = new SessionFactoryManager().CreateSessionFactory();
            builder.Register(c => nhSessionFactory).As<ISessionFactory>().SingleInstance();
            builder.Register(c => c.Resolve<ISessionFactory>().GetHashCode());
            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession());

            var fileExchangeBuilder = new Builder();
            builder.RegisterInstance(fileExchangeBuilder).As<IBuilder>().SingleInstance();

            var nsiCommonServiceCaching = new NsiCommonServiceCaсhing(mapConfig);

            var nsiCommonService = new GisIntegrationService<NsiCommonPortsTypeClient, NsiCommonPortsTypeAsyncClient>(mapConfig);
            var houseManagementService = new GisIntegrationService<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>(mapConfig);
            var billsService = new GisIntegrationService<BillsPortsTypeClient, BillsPortsTypeAsyncClient>(mapConfig);
            var deviceMeteringService = new GisIntegrationService<DeviceMeteringPortTypesClient, DeviceMeteringPortTypesAsyncClient>(mapConfig);
            var infrastructureService = new GisIntegrationService<InfrastructurePortsTypeClient, InfrastructurePortsTypeAsyncClient>(mapConfig);
            var nsiService = new GisIntegrationService<Integration.NsiService.NsiPortsTypeClient, Integration.NsiServiceAsync.NsiPortsTypeAsyncClient>(mapConfig);
            var paymentsServiceAsync = new GisIntegrationService<StubService, PaymentPortsTypeAsyncClient>(mapConfig);

            builder.RegisterInstance(nsiCommonServiceCaching)
                .As<INsiCommonServiceCaching>();

            builder.RegisterInstance(nsiCommonService)
                .As<IGisIntegrationService<NsiCommonPortsTypeClient, NsiCommonPortsTypeAsyncClient>>();
            builder.RegisterInstance(houseManagementService)
                .As<IGisIntegrationService<HouseManagementPortsTypeClient, HouseManagementPortsTypeAsyncClient>>();
            builder.RegisterInstance(billsService)
                .As<IGisIntegrationService<BillsPortsTypeClient, BillsPortsTypeAsyncClient>>();
            builder.RegisterInstance(deviceMeteringService)
                .As<IGisIntegrationService<DeviceMeteringPortTypesClient, DeviceMeteringPortTypesAsyncClient>>();
            builder.RegisterInstance(infrastructureService)
                .As<IGisIntegrationService<InfrastructurePortsTypeClient, InfrastructurePortsTypeAsyncClient>>();
            builder.RegisterInstance(nsiService)
                .As<IGisIntegrationService<Integration.NsiService.NsiPortsTypeClient, Integration.NsiServiceAsync.NsiPortsTypeAsyncClient>>();
            builder.RegisterInstance(paymentsServiceAsync)
                .As<IGisIntegrationService<StubService, PaymentPortsTypeAsyncClient>>();
        }
    }
}

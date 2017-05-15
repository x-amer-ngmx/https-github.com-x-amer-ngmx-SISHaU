using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Integration.Base;
using Integration.Nsi;
using Integration.NsiBaseDto;
using Integration.NsiCommonDto;
using Integration.NsiCommonService;
using Integration.NsiCommonServiceAsync;
using Integration.NsiService;
using Integration.NsiServiceAsync;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;
using SISHaU.Library.API.Interfaces;
using SISHaU.Library.Util;

namespace SISHaU.Library.API
{
    public class NsiCommonServiceCaсhing : GisUtil, INsiCommonServiceCaching
    {
        private Dictionary<string, string> Methods { get; }
        private Dictionary<exportDataProviderNsiItemRequestRegistryNumber, string> RegistryNumber { get; }

        public NsiCommonServiceCaсhing(MapperConfiguration mapConfig = null)
        {
            /*Комбинируем методы из двух сервисов*/
            var proxyMethods = typeof(NsiCommonPortsTypeClient).GetMethods().ToList();
            proxyMethods.AddRange(typeof(NsiPortsTypeClient).GetMethods());

            /*Выборка всех методов в словарь "имя request запроса/наименование метода выполнения"*/
            Methods = proxyMethods
                .Where(t => t.ReturnType.IsAssignableFrom(typeof(ResultHeader)))
                .Select(t => t)
                .ToDictionary(
                    k => k.GetParameters().ElementAt(1).ParameterType.Name,
                    k => k.Name
                );

            RegistryNumber = new Dictionary<exportDataProviderNsiItemRequestRegistryNumber, string>
            {
                {exportDataProviderNsiItemRequestRegistryNumber.Item1, "1" },
                {exportDataProviderNsiItemRequestRegistryNumber.Item51, "51" },
                {exportDataProviderNsiItemRequestRegistryNumber.Item59, "59" },
                {exportDataProviderNsiItemRequestRegistryNumber.Item219, "219" }
            };

            InitMapper(mapConfig);
        }

        public TS ProcessRequest<TS>(object request) where TS : class
        {
            switch (Methods[request.GetType().Name])
            {
                case "exportNsiList":
                    return ExportNsiList<TS>(request);
                case "exportNsiItem":/*Выбираем сервис для NsiCommon*/
                    return ExportNsiItem<TS, NsiCommonPortsTypeClient, NsiCommonPortsTypeAsyncClient>(request,
                        ServiceLocator.Current.GetInstance<IGisIntegrationService<NsiCommonPortsTypeClient, NsiCommonPortsTypeAsyncClient>>());
                case "exportDataProviderNsiItem":/*А тут уже просто Nsi сервис*/
                case "exportDataProviderPagingNsiItem":
                    return ExportNsiItem<TS, NsiPortsTypeClient, NsiPortsTypeAsyncClient>(request,
                        ServiceLocator.Current.GetInstance<IGisIntegrationService<NsiPortsTypeClient, NsiPortsTypeAsyncClient>>());
                default:
                    return null;
            }
        }

        private TS ExportNsiList<TS>(object request) where TS : class
        {
            var sessionFactory = ServiceLocator.Current.GetInstance<ISessionFactory>();
            var gisIntegrationService = ServiceLocator.Current.GetInstance<IGisIntegrationService<NsiCommonPortsTypeClient, NsiCommonPortsTypeAsyncClient>>();

            /*Здесь проверяется, что в базе уже есть данные по справочнику и выводим их*/
            /*Если данных в базе не имеется, делаем запрос по базовой ProcessRequest и заносим данный в базу*/
            using (var session = sessionFactory.OpenSession())
            {

                var queryResult = session.QueryOver<exportNsiListResultDto>()
                    .WhereRestrictionOn(t => t.version).IsLike(CurrentPlatform, MatchMode.Start)
                    .List();

                if (queryResult.Any())
                {
                    return UtilMapper.Map<TS>(queryResult.First());
                }

                var processObjects = gisIntegrationService.ProcessRequest<TS>(request);

                var responseExpr = UtilMapper.Map<exportNsiListResultDto>(processObjects);
                responseExpr.version = $"{CurrentPlatform}_{responseExpr.version}";

                foreach (var itemInfo in responseExpr.NsiList.NsiItemInfo)
                {
                    session.Save(itemInfo);
                }

                session.Save(responseExpr.NsiList);
                session.Save(responseExpr);
                session.Flush();
                session.Close();

                return processObjects;
            }
        }

        private TS ExportNsiItem<TS, T, TU>(object request, IGisIntegrationService<T, TU> gisIntegrationService)
            where TS : class
            where T : class
            where TU : class
        {
            var sessionFactory = ServiceLocator.Current.GetInstance<ISessionFactory>();

            string registryNumber;

            /*Не важно, что структуры на начальном этапе немного отличаются - главное выход один (аналогичный)*/
            switch (Methods[request.GetType().Name])
            {
                case "exportNsiItem":
                    registryNumber = GetPropValue<string>(request, "RegistryNumber");
                    break;
                case "exportDataProviderNsiItem":
                case "exportDataProviderPagingNsiItem":
                    registryNumber =
                        RegistryNumber[
                            GetPropValue<exportDataProviderNsiItemRequestRegistryNumber>(request, "RegistryNumber")];
                    break;

                default:/*Мало ли, что засунет сюда человек*/
                    return null;
            }

            using (var session = sessionFactory.OpenSession())
            {
                var queryResult = session.QueryOver<exportNsiItemResultDto>()
                    .WhereRestrictionOn(t => t.NsiItem).IsNotNull
                    .WhereRestrictionOn(t => t.version).IsInsensitiveLike(CurrentPlatform, MatchMode.Start)
                    .JoinQueryOver(t => t.NsiItem)
                    .WhereRestrictionOn(t => t.NsiItemRegistryNumber).IsLike(registryNumber, MatchMode.Exact)
                    .List();

                if (queryResult.Any())
                {
                    Log(queryResult.First());
                    return UtilMapper.Map<TS>(queryResult.First());
                }

                var processObjects = gisIntegrationService.ProcessRequest<TS>(request);

                Log(processObjects);

                if (null == processObjects) return null;
                var responseExpr = UtilMapper.Map<exportNsiItemResultDto>(processObjects);

                responseExpr.version = $"{CurrentPlatform}_{responseExpr.version}";

                if (responseExpr.NsiItem != null)
                {
                    foreach (var nsiElement in responseExpr.NsiItem.NsiElement)
                    {
                        ProcessChildElements(session, nsiElement);
                    }

                    session.Save(responseExpr.NsiItem);
                }
                else
                {
                    if (responseExpr.ErrorMessage != null)
                    {
                        session.Save(responseExpr.ErrorMessage);

                        /*responseExpr.NsiItem = new NsiItemTypeDto
                        {
                            Created = DateTime.Now,
                            NsiItemRegistryNumber = registryNumber
                        };
                        session.Save(responseExpr.NsiItem);*/
                    }
                }

                session.Save(responseExpr);
                session.Flush();
                session.Close();

                return processObjects;
            }
        }

        private static void ProcessChildElements(ISession session, NsiElementTypeDto nsiElement)
        {
            foreach (var nsiElementNsiElement in nsiElement.NsiElementField)
            {
                session.Save(nsiElementNsiElement);
            }

            foreach (var nsiElementChild in nsiElement.ChildElement)
            {
                ProcessChildElements(session, nsiElementChild);
                session.Save(nsiElementChild);
            }

            session.Save(nsiElement);
        }
    }
}

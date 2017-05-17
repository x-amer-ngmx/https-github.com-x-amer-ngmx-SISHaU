using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Integration.Base;
using Newtonsoft.Json;

namespace SISHaU.Library.Util
{
    public class GisUtil : TypeUtils
    {
        public IMapper UtilMapper;
        public static readonly string CurrentPlatform = ConfigurationManager.AppSettings["current_platform"];
        public static string OrgPPaidGuid = ConfigurationManager.AppSettings["org_ppaid_guid_" + CurrentPlatform];
        public static string RootOrgId = ConfigurationManager.AppSettings["root_org_id_" + CurrentPlatform];
        public static string OrgOgrn = ConfigurationManager.AppSettings["org_ogrn"];
        public static readonly bool IsLogging = ConfigurationManager.AppSettings["has_logging"] != null && Boolean.Parse(ConfigurationManager.AppSettings["has_logging"]);

        public void InitMapper(MapperConfiguration cfg)
        {
            if (null == cfg || null != UtilMapper) return;
            UtilMapper = cfg.CreateMapper();
        }

        /// <summary>
        /// Получает заголовок сообщения в зависимости от типа сервиса
        /// </summary>C:\INTEGRATION\ServiceHelperHost\GisLayer\GisUtil.cs
        /// <returns></returns>
        protected virtual object GetHeader(MethodInfo method)
        {
            var testType = method.GetParameters().First().ParameterType;

            return testType == typeof(HeaderType) ? new HeaderType
            {
                MessageGUID = TransportGuid,
                Date = DateTime.Now,
            } : testType == typeof(RequestHeader) ? new RequestHeader
            {
                orgPPAGUID = OrgPPaidGuid,
                MessageGUID = TransportGuid,
                Date = DateTime.Now,
                IsOperatorSignature = true
            } : null;
        }

        /// <summary>
        /// Обрабатывает стандартные транспортные ошибки в зависимости от типа сервиса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="structWithItem"></param>
        /// <returns></returns>
        public bool HandleError<T>(object structWithItem)
        {
            if (null == structWithItem)
            {
                return false;
            }

            var array = structWithItem as Array;
            if (array != null)
            {
                return array.Cast<object>().Any(HandleError<T>);
            }

            var props = structWithItem.GetType().GetProperties();

            foreach (var prop in props)
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                //TODO: Опять порно?
                switch (prop.Name)
                {
                    case "Item":
                        var item = prop.GetValue(structWithItem);

                        if (null == item) continue;

                        if (item is T)
                        {
                            DispatchError(item);
                            return true;
                        }
                        break;
                    case "Items":

                        var items = prop.GetValue(structWithItem) as Array;

                        if (null == items) continue;

                        for (var i = 0; i < items.Length; i++)
                        {
                            var element = items.GetValue(i);
                            if (!(element is T)) continue;
                            DispatchError(element);
                            return true;
                        }
                        break;
                }
            }
            return false;
        }

        /// <summary>
        /// Распаковывает ошибку для отображения в лог или в любое другое место
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        public void DispatchError<T>(T item)
        {

            var errorInstanceProps = item.GetType().GetProperties();

            foreach (var errorProp in errorInstanceProps)
            {
                //TODO: Ну сколько же можно уже же...
                switch (errorProp.Name)
                {
                    case "Description":
                        var description = errorProp.GetValue(item);
                        System.Diagnostics.Debug.WriteLine("Description = " + description);
                        /*System.Diagnostics.Debug.WriteLine("Description = " + description);
                        Console.WriteLine("Description = " + description);*/
                        break;
                    case "ErrorCode":
                        var errorCode = errorProp.GetValue(item);
                        System.Diagnostics.Debug.WriteLine("ErrorCode = " + errorCode);
                        /*System.Diagnostics.Debug.WriteLine("Error = " + errorCode);
                        Console.WriteLine("Error = " + errorCode);*/
                        break;
                    case "StackTrace":
                        var stackTrace = errorProp.GetValue(item);
                        System.Diagnostics.Debug.WriteLine("StackTrace = " + stackTrace);
                        /*System.Diagnostics.Debug.WriteLine("Error = " + errorCode);
                        Console.WriteLine("Error = " + errorCode);*/
                        break;
                }
            }
        }

        /// <summary>
        /// Формирование типа для справочника по словарю
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="code"></param>
        /// <param name="guid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetNsiRef<T>(string code, string guid, string name)
        {
            return FillInstance<T>(new Dictionary<string, object>
            {
                { "Code", code},
                { "Guid", guid},
                { "Name", name}
            });
        }

        public static T GetNsiRef<T, TS>(TS nsiRef)
        {
            var instanceTo = Activator.CreateInstance<T>();

            var props = typeof(TS).GetProperties();

            foreach (var prop in props)
            {
                var d = prop.GetValue(nsiRef);
                if (null == d) continue;
                prop.SetValue(instanceTo, d);
            }

            return instanceTo;
        }

        /// <summary>
        /// Выводи объект в JSON файл
        /// </summary>
        /// <param name="responseExpr"></param>
        /// <param name="append"></param>
        public void Log(object responseExpr, bool append = true)
        {
            if (!IsLogging) return;
            const string path = "c:\\log.json";
            var poJson = JsonConvert.SerializeObject(responseExpr);

            using (var myTextWriter = new JsonTextWriter(new StreamWriter(path, append)))
            {
                myTextWriter.WriteRaw(poJson);
                myTextWriter.WriteWhitespace(Environment.NewLine);
                myTextWriter.Flush();
                myTextWriter.Close();
            }
        }
    }
}

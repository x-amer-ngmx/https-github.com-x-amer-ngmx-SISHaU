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
    public class GisUtil
    {
        public IMapper UtilMapper;
        public static readonly string CurrentPlatform = ConfigurationManager.AppSettings["current_platform"];
        public string OrgPPaidGuid = ConfigurationManager.AppSettings["org_ppaid_guid_" + CurrentPlatform];
        public string RootOrgId = ConfigurationManager.AppSettings["root_org_id_" + CurrentPlatform];
        public string OrgOgrn = ConfigurationManager.AppSettings["org_ogrn"];
        public string SignId = ConfigurationManager.AppSettings["sign_id"];
        public readonly bool IsLogging = ConfigurationManager.AppSettings["has_logging"] != null && Boolean.Parse(ConfigurationManager.AppSettings["has_logging"]);

        public string NewTransportGuid => Guid.NewGuid().ToString();
        public void InitMapper(MapperConfiguration cfg)
        {
            if (null == cfg || null != UtilMapper) return;
            UtilMapper = cfg.CreateMapper();
        }

        public T GenerateGenericType<T>() where T : class
        {
            return FillInstance<T>(new Dictionary<string, object> {
                { "Id", SignId }
            });
        }

        /// <summary>
        /// Получает заголовок сообщения в зависимости от типа сервиса
        /// </summary>C:\INTEGRATION\ServiceHelperHost\GisLayer\GisUtil.cs
        /// <returns></returns>
        protected virtual object GetHeader(MethodInfo method)
        {
            switch (method.GetParameters().First().ParameterType.Name)
            {
                case "HeaderType":
                    return new HeaderType
                    {
                        MessageGUID = Guid.NewGuid().ToString(),
                        Date = DateTime.Now,
                    };
                case "RequestHeader":
                    return new RequestHeader
                    {
                        orgPPAGUID = OrgPPaidGuid,
                        MessageGUID = Guid.NewGuid().ToString(),
                        Date = DateTime.Now,
                        IsOperatorSignature = true
                    };
            }

            return null;
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
        /// Заполняет простую сущность данными по словарю
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T FillInstance<T>(Dictionary<string, object> data = null)
        {
            var instance = Activator.CreateInstance<T>();
            return null == data ? instance : FillInstance(instance, data);
        }

        /// <summary>
        /// Перегрузка
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T FillInstance<T>(T instance, Dictionary<string, object> data)
        {
            var props = typeof(T).GetProperties();
            foreach (var prop in props.Where(prop => data.ContainsKey(prop.Name)))
            {
                prop.SetValue(instance, data[prop.Name]);
            }
            return instance;
        }

        /// <summary>
        /// Возвращает словарь с данными для простого типа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetInstance<T>(Dictionary<string, object> data)
        {
            var instance = Activator.CreateInstance<T>();
            var props = typeof(T).GetProperties();
            return props.Where(prop => data.ContainsKey(prop.Name)).ToDictionary(prop => prop.Name, prop => prop.GetValue(instance));
        }

        public static object GetPropValue(object obj, string name)
        {
            foreach (var part in name.Split('.'))
            {
                if (obj == null) { return null; }

                var type = obj.GetType();
                var info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(object obj, string name)
        {
            var retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }
            return (T)retval;
        }

        public static void SetPropValue(object obj, string name, object val)
        {
            if (null == obj) return;
            var type = obj.GetType();
            var info = type.GetProperty(name);
            info?.SetValue(obj, val);
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

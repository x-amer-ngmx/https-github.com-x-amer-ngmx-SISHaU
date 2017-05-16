using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace SISHaU.Library.Util
{
    public class TypeUtils
    {
        public string SignId = ConfigurationManager.AppSettings["sign_id"];
        public string TransportGuid => Guid.NewGuid().ToString();

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

        public T GenerateGenericType<T>() where T : class
        {
            return FillInstance<T>(new Dictionary<string, object> {
                { "Id", SignId }
            });
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CryptoPro.Sharpei;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File
{
    public static class SingleMetods
    {
        public static string GetName(this Enum en)
        {
            var name = Enum.GetName(en.GetType(), en);
            var result = name?.Replace('_', '-');
            return en is Repo ? result?.ToLower() : result;
        }

        public static T ResultEnginer<T>(this HttpResponseMessage respons) where T : class
        {
            var result = Activator.CreateInstance(typeof(T));



            return (T) result;
        }

        public static byte[] FileMd5(this byte[] stream)
        {
            return MD5.Create().ComputeHash(stream);
        }

        public static string FileGost(this byte[] stream)
        {
            string result;
            var hash = new Gost3411CryptoServiceProvider().ComputeHash(stream);
            result = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();

            return result;
        }
    }
}

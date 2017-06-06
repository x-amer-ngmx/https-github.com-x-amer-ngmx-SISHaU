using System;
using System.Net;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File.Enginer
{
    public class HttpWebClient : WebClient
    {
        private HttpWebRequest _request;
        private RangeModel _range;
        private string _method;
        public void SetRange(RangeModel range)
        {
            _range = range;
        }

        public void SetMethod(string method)
        {
            _method = method;
        }

        public HttpStatusCode StatusCode()
        {
            HttpStatusCode result;

            if (_request == null)
            {
                throw (new InvalidOperationException("Гавно"));
            }


            if (GetWebResponse(_request) is HttpWebResponse response)
            {
                result = response.StatusCode;
            }
            else
            {
                throw (new InvalidOperationException("Совсем гавно"));
            }

            return result;
        }


        protected override WebRequest GetWebRequest(Uri address)
        {
            _request = (HttpWebRequest)base.GetWebRequest(address);
            if (_request == null)
            {
                throw new ArgumentNullException("Запрос не задан...");
            }
            _request.Timeout = 60 * 60 * 1000;
            if(string.IsNullOrEmpty(_method)) _request.Method = _method;

            if (_range?.From == null || _range?.To == null)
            {
                throw new ArgumentNullException("Диапазон байт не задан...");
            }

            if (_range != null) _request?.AddRange((int)_range.From, (int)_range.To);

            return _request;

        }


    }
}

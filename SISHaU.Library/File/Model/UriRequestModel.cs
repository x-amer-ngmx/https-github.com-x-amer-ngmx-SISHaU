using System;
using System.Net.Http;

namespace SISHaU.Library.File.Model
{
    public class UriRequestModel : IDisposable
    {
        private string _uri;

        public Repo Repository { get; set; }
        public HttpMethod Method { get; set; }
        public string UriRequest {
            get => _uri;
            set => _uri = $"{Config.ServerShare}{Repository.GetName()}/{value}";
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            UriRequest = string.Empty;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

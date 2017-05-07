using System.Net.Http;

namespace SISHaU.Library.File.Model
{
    public class UriRequestModel
    {
        private string _uri;

        public Repo Repository { get; set; }
        public HttpMethod Method { get; set; }
        public string UriRequest {
            get => _uri;
            set => _uri = $"{ConstantModel.ServerShare}{Repository.GetName()}/{value}";
        }
    }
}

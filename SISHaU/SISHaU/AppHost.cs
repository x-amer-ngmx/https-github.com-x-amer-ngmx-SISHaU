using System.ComponentModel;
using Funq;
using ServiceStack.WebHost.Endpoints;
using SISHaU.ServiceInterface;
using SISHaU.ServiceInterface.Services;
using SISHaU.ServiceModel;
using Container = Funq.Container;

namespace SISHaU
{
    //VS.NET Template Info: https://servicestack.net/vs-templates/EmptyAspNet
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Base constructor requires a Name and Assembly where web service implementation is located
        /// </summary>
        public AppHost()
            : base("SISHaU", typeof(FileExchangeService).Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        public override void Configure(Container container)
        {
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());
            //Routes.Add<Hello>("/hello/{Name}");
        }
    }
}
using System;

namespace SISHaU.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            HostUtils.InitHost<AppHost>();
        }
    }
}
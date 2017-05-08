using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Funq;
using ServiceStack.WebHost.Endpoints;
using SISHaU.ServiceInterface.Services;

namespace SISHaU.Tests
{
    public class TestAppHost : AppHostHttpListenerBase
    {
        public TestAppHost() : base("TestAppHost", typeof(FileExchangeService).Assembly) { }
        public override void Configure(Container container) { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.Library.API.model
{
    public class MainWorkerClass
    {
        private string order;
        private bool done = true;
        public bool DoWork()
        {
            //123
            order = "AD";
            return done;
        }
    }
}

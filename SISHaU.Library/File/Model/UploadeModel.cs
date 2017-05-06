using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SISHaU.Library.File.Model
{
    public class UploadeModel
    {
        public ResultModel FileInfo { get; set; }

        /// <summary>
        /// ExplodUnitModel or IEnumerable<ExplodUnitModel>
        /// </summary>
        public object Parts { get; set;  }
        public MarcerModel Marcer { get; set; }
    }
}

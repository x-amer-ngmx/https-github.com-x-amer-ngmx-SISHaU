using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISHaU.Library.File.Model;

namespace SISHaU.Library.File
{
    public static class SingleMetods
    {
        public static string GetName(this Enum en)
        {
            var t = en.GetType();
            var name = Enum.GetName(t, en);
            var result = name?.Replace('_', '-');
            return t.Name.Equals("Repo") ? result?.ToLower() : result;
        }
    }
}

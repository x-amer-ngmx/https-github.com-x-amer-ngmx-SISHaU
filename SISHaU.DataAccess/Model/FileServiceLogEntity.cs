using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISHaU.DataAccess.Definition;

namespace SISHaU.DataAccess.Model
{
    public class FileServiceLogEntity : EntityDto
    {
    }

    public class FileServiceLogEntityMap : MapAction<FileServiceLogEntity>
    {
        public FileServiceLogEntityMap() : base("", "", id => id.Id)
        {
            
        }
    }
}

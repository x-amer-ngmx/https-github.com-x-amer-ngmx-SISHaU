using SISHaU.Library.File.Model;
using System;
using System.Collections.Generic;

namespace SISHaU.Library.File
{
    public interface IUploadeResultRepository
    {
        void Save(UploadeResultModel upload);
        IList<UploadeResultModel> GetUploadeInfo(Guid uid);
    }
}

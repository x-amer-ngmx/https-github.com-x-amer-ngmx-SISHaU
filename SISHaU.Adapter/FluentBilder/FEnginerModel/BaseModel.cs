using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SISHaU.Adapter.FluentBilder.FInterface;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel
{
    public class BaseModel<T> : IBaseModelPooler<T>
    {
        public T BaseModelEntity { get; set; }
        public virtual T Pool()
        {
            return BaseModelEntity;
        }

        public BaseModel(T baseModel)
        {
            BaseModelEntity = baseModel;
        }
    }
}

using SISHaU.Adapter.FluentBilder.FInterface;
using SISHaU.Library.Util;

namespace SISHaU.Adapter.FluentBilder.FEnginerModel
{
    public class BaseModel<T> : GisUtil, IBaseModelPooler<T>
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

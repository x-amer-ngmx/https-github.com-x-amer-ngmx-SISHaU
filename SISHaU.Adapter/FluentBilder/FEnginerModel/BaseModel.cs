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

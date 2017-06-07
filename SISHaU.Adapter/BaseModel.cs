using System.Collections.Generic;
using SISHaU.Library.Util;

namespace SISHaU.Adapter
{
    public class BaseModel<T, TU, TS> : BaseModelBehavior<TS> where T : class
    {
        public IDictionary<T, TU> Operation { get; set; }
        public T Request { get; set; }
        public BaseModel(TS baseModel, IDictionary<T, TU> modelRequest) : base(baseModel)
        {
            Operation = modelRequest;
            Request = GenerateGenericType<T>();
            Operation?.Add(Request, default(TU));
        }

        public bool Is<TA>() => typeof (TA) == typeof (T);

        public TP Convert<TP>() where TP : class
        {
            return Request as TP;
        }
    }

    public class BaseModelBehavior<T> : TypeUtils, IBaseModelBehavior<T>
    {
        public BaseModelBehavior(T baseModel)
        {
            BaseModelEntity = baseModel;
        }
        public T BaseModelEntity { get; set; }
        public virtual T Push()
        {
            return BaseModelEntity;
        }
    }
}
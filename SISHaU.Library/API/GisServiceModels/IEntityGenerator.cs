using System;
using System.Linq.Expressions;

namespace SISHaU.Library.API.GisServiceModels
{
    public interface IEntityGenerator<T, TU>
    {
        EntityGenerator<T, TU> Set(Expression<Func<T, object>> property, object value);
    }
}

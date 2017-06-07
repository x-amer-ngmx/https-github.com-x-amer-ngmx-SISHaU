using System;
using System.Linq.Expressions;

namespace SISHaU.Adapter
{
    public interface IEntityGenerator<T, TU>
    {
        EntityGenerator<T, TU> Set(Expression<Func<T, object>> property, object value);
    }
}

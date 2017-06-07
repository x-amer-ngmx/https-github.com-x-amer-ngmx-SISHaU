using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SISHaU.Library.API.GisServiceModels
{
    public class EntityGenerator<T, TU> : BaseModelBehavior<TU>, IEntityGenerator<T, TU>
    {
        private readonly T _entity;

        public EntityGenerator(T entity, TU baseModel) : base(baseModel){
            _entity = entity;
        }

        public EntityGenerator<T, TU> Set(Expression<Func<T, object>> property, object value)
        {
            PropertyInfo propertyInfo = null;

            var propertyMemberExpression = property.Body as MemberExpression;

            if (null != propertyMemberExpression)
            {
                propertyInfo = ((MemberExpression)property.Body).Member as PropertyInfo;
            }
            else
            {
                var operandMemberExpression = ((UnaryExpression)property.Body).Operand as MemberExpression;
                if (null != operandMemberExpression)
                {
                    propertyInfo = operandMemberExpression.Member as PropertyInfo;
                }
            }

            if (null == propertyInfo) return this;

            SetPropValue(_entity, propertyInfo.Name, value);
            /*ToDo добавить проверку на specified - если устанавливаю значение, то устанавливать в TRUE*/

            return this;
        }
    }
}

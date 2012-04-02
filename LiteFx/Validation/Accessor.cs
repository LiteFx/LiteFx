using System;
using System.Linq.Expressions;

namespace LiteFx.Validation
{
    public class Accessor<T, TProperty>
    {
        public Expression<Func<T, TProperty>> ExpressionAccessor { get; protected set; }

        private Func<T, TProperty> compiledAccessor;
        public Func<T, TProperty> CompiledAccessor
        {
            get { return compiledAccessor ?? (compiledAccessor = ExpressionAccessor.Compile()); }
        }

        public Accessor(Expression<Func<T, TProperty>> accessor)
        {
            if (accessor == null)
                throw new ArgumentNullException("accessor");

            if (accessor.Body.NodeType != ExpressionType.MemberAccess && accessor.Body.NodeType != ExpressionType.Parameter)
                throw new ArgumentException("accessor should be ExpressionType.MemberAccess or ExpressionType.Parameter");

            ExpressionAccessor = accessor;
        }

        public string MemberName
        {
            // TODO: Incluir referencias para o displayname do data annotations
            get
            {
                if (ExpressionAccessor.Body is MemberExpression)
                    return ((MemberExpression)ExpressionAccessor.Body).Member.Name;

                return string.Empty;
            }
        }

        public Type MemberType 
        {
            get
            {
                if (ExpressionAccessor.Body is MemberExpression)
                    return ((MemberExpression)ExpressionAccessor.Body).Member.DeclaringType;

                return null;
            }
        }
    }

}

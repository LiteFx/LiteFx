using System;
using System.Linq.Expressions;

namespace LiteFx.Bases.Validation
{
    public abstract class Assertion
    {
        public abstract bool IsValid(object obj);
        internal abstract string MemberName { get; }
        internal abstract string ValidationMessage { get; set; }
    }

    public class Assertion<T, TProperty> : Assertion
    {
        public Expression<Func<T, TProperty>> Accessor { get; protected set; }

        private Func<T, TProperty> compiledAccessor;
        private Func<T, TProperty> CompiledAccessor
        {
            get { return compiledAccessor ?? (compiledAccessor = Accessor.Compile()); }
        }

        public Func<TProperty, bool> Predicate { get; internal set; }

        public bool AccessorCanBeNull { get; set; }

        public Assertion(Expression<Func<T, TProperty>> accessor) : this(accessor, null) { }

        public Assertion(Expression<Func<T, TProperty>> accessor, Func<TProperty, bool> evalExpression)
        {
            if (accessor == null)
                throw new ArgumentNullException("accessor");

            if (accessor.Body.NodeType != ExpressionType.MemberAccess && accessor.Body.NodeType != ExpressionType.Parameter)
                throw new ArgumentException("accessor should be ExpressionType.MemberAccess or ExpressionType.Parameter");

            Accessor = accessor;
            Predicate = evalExpression;
            AccessorCanBeNull = true;
        }

        public override bool IsValid(object obj)
        {
            TProperty property = CompiledAccessor((T)obj);

            if (!AccessorCanBeNull && property == null)
                return true;

            return Predicate(property);
        }

        internal override string MemberName
        {
            get { return ((MemberExpression)Accessor.Body).Member.Name; }
        }

        internal override string ValidationMessage
        {
            get;
            set;
        }
    }
}

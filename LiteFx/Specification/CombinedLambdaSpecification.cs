using System;
using System.Linq.Expressions;

namespace LiteFx.Specification
{
    public class CombinedLambdaSpecification<T> : LambdaSpecification<T>
    {
        public CombinedLambdaSpecification(Expression<Func<T, bool>> predicate)
        {
            this.predicate = predicate;
        }

        private Expression<Func<T, bool>> predicate;

        public override Expression<Func<T, bool>> Predicate
        {
            get { return predicate; }
        }
    }
}

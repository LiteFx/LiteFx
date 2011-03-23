using System;
using System.Linq;
using System.Linq.Expressions;

namespace LiteFx.Bases.Specification
{
    /// <summary>
    /// Specification pattern implementation using lambda expressions.
    /// </summary>
    /// <typeparam name="T">Type that will be evaluated.</typeparam>
    public abstract class LambdaSpecification<T> : ILambdaSpecification<T>
    {
        /// <summary>
        /// The predicated expression.
        /// </summary>
        public abstract Expression<Func<T, bool>> Predicate { get; }

        /// <summary>
        /// Cached compiled predicate.
        /// </summary>
        private Func<T, bool> compiledPredicate;

        /// <summary>
        /// Cached compiled predicate.
        /// </summary>
        protected Func<T, bool> CompiledPredicate
        {
            get { return compiledPredicate ?? (compiledPredicate = Predicate.Compile()); }
        }

        /// <summary>
        /// Combine two specifications using the AndAlso (&&) operator.
        /// </summary>
        /// <param name="leftSide">Specification that will be in the left side of the operation.</param>
        /// <param name="rightSide">Specification that will be in the left side of the operation.</param>
        /// <returns>The new combined specification.</returns>
        public static LambdaSpecification<T> operator &(LambdaSpecification<T> leftSide, LambdaSpecification<T> rightSide)
        {
            Expression<Func<T, bool>> newExpression = (T t) => leftSide.IsSatisfiedBy(t) && rightSide.IsSatisfiedBy(t);

            return new CombinedLambdaSpecification<T>(newExpression);
        }

        /// <summary>
        /// Combine two specifications using the OrElse (||) operator.
        /// </summary>
        /// <param name="leftSide">Specification that will be in the left side of the operation.</param>
        /// <param name="rightSide">Specification that will be in the left side of the operation.</param>
        /// <returns>The new combined specification.</returns>
        public static LambdaSpecification<T> operator |(LambdaSpecification<T> leftSide, LambdaSpecification<T> rightSide)
        {
            Expression<Func<T, bool>> newExpression = (T t) => leftSide.IsSatisfiedBy(t) || rightSide.IsSatisfiedBy(t);

            return new CombinedLambdaSpecification<T>(newExpression);
        }

        #region ISpecification<T> Members

        /// <summary>
        /// Verifies the entity over the predicate.
        /// </summary>
        /// <param name="entity">Entity to be verified.</param>
        /// <returns>True if the specification is satisfied and false if it is not.</returns>
        public bool IsSatisfiedBy(T entity)
        {
            return CompiledPredicate(entity);
        }

        #endregion
    }
}
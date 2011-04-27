using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace LiteFx.Specification
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
            return And(leftSide, rightSide);
        }

        public static LambdaSpecification<T> And(LambdaSpecification<T> leftSide, LambdaSpecification<T> rightSide) 
        {
            Expression<Func<T, bool>> left = leftSide.Predicate;

            IEnumerable<ParameterExpression> parameters = left.Parameters;

            InvocationExpression right = Expression.Invoke(rightSide.Predicate, parameters);

            BinaryExpression andAlso = Expression.AndAlso(left.Body, right);

            Expression<Func<T, bool>> newExpression = Expression.Lambda<Func<T, bool>>(andAlso, parameters);

            return new CombinedLambdaSpecification<T>(newExpression);
        }

        public LambdaSpecification<T> And(LambdaSpecification<T> other) 
        {
            return And(this, other);
        }

        /// <summary>
        /// Combine two specifications using the OrElse (||) operator.
        /// </summary>
        /// <param name="leftSide">Specification that will be in the left side of the operation.</param>
        /// <param name="rightSide">Specification that will be in the left side of the operation.</param>
        /// <returns>The new combined specification.</returns>
        public static LambdaSpecification<T> operator |(LambdaSpecification<T> leftSide, LambdaSpecification<T> rightSide)
        {
            return Or(leftSide, rightSide);
        }

        public static LambdaSpecification<T> Or(LambdaSpecification<T> leftSide, LambdaSpecification<T> rightSide) 
        {
            Expression<Func<T, bool>> left = leftSide.Predicate;

            IEnumerable<ParameterExpression> parameters = left.Parameters;

            InvocationExpression right = Expression.Invoke(rightSide.Predicate, parameters);

            BinaryExpression orElse = Expression.OrElse(left.Body, right);

            Expression<Func<T, bool>> newExpression = Expression.Lambda<Func<T, bool>>(orElse, parameters);

            return new CombinedLambdaSpecification<T>(newExpression);
        }

        public LambdaSpecification<T> Or(LambdaSpecification<T> other) 
        {
            return Or(this, other);
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
using System;
using System.Linq.Expressions;

namespace LiteFx.Specification
{
    /// <summary>
    /// Lambda specification interface.
    /// </summary>
    /// <typeparam name="T">Type of the entity that will be verified.</typeparam>
    public interface ILambdaSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// Predicate to be used in specification validation.
        /// </summary>
        Expression<Func<T, bool>> Predicate { get; }
    }
}
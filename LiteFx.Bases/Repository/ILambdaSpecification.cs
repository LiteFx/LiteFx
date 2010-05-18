using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace LiteFx.Bases.Repository
{
    public interface ILambdaSpecification<T> : ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }
    }
}
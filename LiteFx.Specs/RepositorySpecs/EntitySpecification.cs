using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteFx.Specification;

namespace LiteFx.Specs.RepositorySpecs
{
    public class EntitySpecification : LambdaSpecification<Entity>
    {
        public override System.Linq.Expressions.Expression<Func<Entity, bool>> Predicate
        {
            get { return e => e.Id == 1; }
        }
    }
}

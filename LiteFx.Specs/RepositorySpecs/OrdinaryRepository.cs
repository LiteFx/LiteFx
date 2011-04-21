using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteFx.Repository;

namespace LiteFx.Specs.RepositorySpecs
{
    public interface IOrdinaryEntityRepository : IRepository<Entity, int> { }

    public class OrdinaryEntityRepository : RepositoryBase<Entity, int, IOrdinaryContext>, IOrdinaryEntityRepository
    {
        protected override IOrdinaryContext Context
        {
            get { return RepositoryAndContextStepDefinition.context; }
        }
    }
}

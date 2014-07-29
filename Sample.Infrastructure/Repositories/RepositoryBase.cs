using LiteFx.Repository;
using Microsoft.Practices.ServiceLocation;
using Sample.Domain;
using Sample.Domain.Repositories;

namespace Sample.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, int, ISampleContext>, IRepository<TEntity>
            where TEntity : EntityBase
    {
        public RepositoryBase(ISampleContext context) : base(context) { }
    }
}

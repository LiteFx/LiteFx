using LiteFx.Repository;
using Microsoft.Practices.ServiceLocation;
using Sample.Domain;
using Sample.Domain.Repositories;

namespace Sample.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> : RepositoryBase<TEntity, int, ISampleContext>, IRepository<TEntity>
            where TEntity : EntityBase
    {
        private ISampleContext _context;
        protected override ISampleContext Context
        {
            get { return _context ?? (_context = ServiceLocator.Current.GetInstance<ISampleContext>()); }
        }
    }
}

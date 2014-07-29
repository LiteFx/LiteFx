using LiteFx.Repository;

namespace Sample.Domain.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, int>
    {
    }
}
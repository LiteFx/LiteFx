using Sample.Domain;
using Sample.Domain.Repositories;

namespace Sample.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository { }
}
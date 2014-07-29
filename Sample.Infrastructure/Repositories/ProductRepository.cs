using Sample.Domain;
using Sample.Domain.Repositories;

namespace Sample.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ISampleContext context) : base(context) { }
    }
}
using Sample.Domain;
using Sample.Domain.Repositories;

namespace Sample.Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ISampleContext context) : base(context) { }
    }
}
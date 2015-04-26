using LiteFx;
using Sample.Domain;
using System.Linq;

namespace Sample.Infrastructure
{
    public interface ISampleContext : IContext
    {
        IQueryable<Product> Products { get; }
    }
}

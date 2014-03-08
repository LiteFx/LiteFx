using LiteFx;
using Sample.Domain;
using System.Linq;

namespace Sample.Infrastructure
{
    public interface ISampleContext : IContext<int>
    {
        IQueryable<Product> Products { get; }
    }
}

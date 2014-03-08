using LiteFx.Context.NHibernate;
using Sample.Domain;
using System.Linq;

namespace Sample.Infrastructure
{
    public class SampleContext : NHibernateContextAdapter<int>, ISampleContext
    {
        public IQueryable<Product> Products
        {
            get { return GetQueryableObject<Product>(); }
        }
    }
}
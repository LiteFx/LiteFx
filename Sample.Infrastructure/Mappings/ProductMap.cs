using Sample.Domain;

namespace Sample.Infrastructure.Mappings
{
    public class ProductMap : EntityBaseMap<Product>
    {
        public ProductMap()
        {
            Map(p => p.Name).Not.Nullable().Length(100);
            Map(p => p.Price);
            Map(p => p.Discontinued);
            Map(p => p.Details).Length(1000);
        }
    }
}
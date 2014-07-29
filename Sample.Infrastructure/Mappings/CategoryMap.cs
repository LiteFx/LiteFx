using Sample.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure.Mappings
{
    public class CategoryMap : EntityBaseMap<Category>
    {
        public CategoryMap()
        {
            Map(c => c.Name);
            HasMany(c => c.Products);
        }
    }
}

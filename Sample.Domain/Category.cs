using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sample.Domain
{
    public class Category : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual IList<Product> Products { get; set; }

        public Category() { }

        public Category(string name)
        {
            Name = name;
        }

        public override void ConfigureValidation()
        {
            Assert<Category>();
        }
    }
}

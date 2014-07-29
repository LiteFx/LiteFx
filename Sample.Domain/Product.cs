using LiteFx.Validation;

namespace Sample.Domain
{
    public class Product : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        public virtual bool Discontinued { get; set; }
        public virtual string Details { get; set; }
        public virtual double Quantity { get; set; }
        public virtual Category Category { get; set; }

        public Product() { }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
            Discontinued = false;
        }

        public override void ConfigureValidation()
        {
            Assert<Product>()
                .That(p => p.Name)
                    .Required()
                    .MaxLength(100);

            Assert<Product>()
                .That(p => p.Details)
                    .MaxLength(1000);
        }
    }
}
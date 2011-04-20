using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteFx.Validation;

namespace LiteFx.Specs.ValidationSpecs
{
    class Category : EntityBaseWithValidation<int>
    {
        public string Name { get; set; }

        public override void ConfigureValidation()
        {
            Assert<Category>().That(c => c.Name).IsNotNullOrEmpty();
        }
    }

    class SuperCategory : Category
    {
        public int Rank { get; set; }

        public override void ConfigureValidation()
        {
            Assert<SuperCategory>().That(s => s.Rank).Min(1);
            
            base.ConfigureValidation();
        }
    }
}

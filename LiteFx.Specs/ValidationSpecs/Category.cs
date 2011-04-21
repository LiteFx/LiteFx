using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteFx.Validation.PtBr;

namespace LiteFx.Specs.ValidationSpecs
{
    class Category : EntityBaseWithValidation<int>
    {
        public string Name { get; set; }

        public override void ConfigureValidation()
        {
            Assert<Category>().Que(c => c.Name).NaoSejaNuloOuVazio();
        }
    }

    class SuperCategory : Category
    {
        public int Rank { get; set; }

        public override void ConfigureValidation()
        {
            Assert<SuperCategory>().Que(s => s.Rank).Minimo(1);
            
            base.ConfigureValidation();
        }
    }
}

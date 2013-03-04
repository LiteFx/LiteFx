using System;
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

	class DerivedCategory : Category
	{
		public int Rank { get; set; }

		public int MinRank { get; set; }

		public DerivedCategory()
		{
			MinRank = 1;
		}

		public override void ConfigureValidation()
		{
			Assert<DerivedCategory>()
				.Que(s => s.Rank)
				.MaiorQueOuIgual(() => MinRank);

			base.ConfigureValidation();
		}
	}

	class CategoryWithNullableMember : Category
	{
		public DateTime? Creation { get; set; }

		public override void ConfigureValidation()
		{
			Assert<CategoryWithNullableMember>()
				.Que(c => c.Creation)
				.Maximo(new DateTime(2050, 1, 1));

			base.ConfigureValidation();
		}
	}
}

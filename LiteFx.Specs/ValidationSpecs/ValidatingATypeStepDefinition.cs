using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace LiteFx.Specs.ValidationSpecs
{
	[Binding]
	public class ValidatingATypeStepDefinition
	{
		Category type;
		IEnumerable<ValidationResult> results;

		[Given(@"a valid Type")]
		public void GivenAValidType()
		{
			type = new Category() { Name = "Valid Type" };
		}

		[Given(@"a invalid Type")]
		public void GivenAInvalidType()
		{
			type = new Category();
		}

		[Given(@"a valid Derived Type")]
		public void GivenAValidDerivedType()
		{
			type = new DerivedCategory() { Name = "Valid Type", Rank = 1 };
		}

		[Given(@"a invalid Derived Type")]
		public void GivenAInvalidDerivedType()
		{
			type = new DerivedCategory();
		}

		[Given(@"a valid Type with a Nullable member")]
		public void GivenAValidTypeWithANullableMember()
		{
			type = new CategoryWithNullableMember() { Name = "Valid Type", Creation = DateTime.Today };
		}

		[Given(@"a invalid Type with a Nullable member")]
		public void GivenAInvalidTypeWithANullableMember()
		{
			type = new CategoryWithNullableMember() { Creation = DateTime.Today.AddYears(50) };
		}

		[Given(@"a valid Type with a Nullable member set to null")]
		public void GivenAValidTypeWithANullableMemberSetToNull()
		{
			type = new CategoryWithNullableMember() { Name = "Valid Type", Creation = null };
		}

		[When(@"I call the validate method")]
		public void WhenICallTheValidateMethod()
		{
			results = type.Validate();
		}

		[Then(@"the count of validationResult collection should be (.*)")]
		public void ThenTheCountOfValidationResultCollectionShouldBe(int count)
		{
			Assert.AreEqual(count, results.Count());
		}
	}
}

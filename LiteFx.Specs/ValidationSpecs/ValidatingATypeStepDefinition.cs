using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using LiteFx.Validation.ClientValidationRules;

namespace LiteFx.Specs.ValidationSpecs
{
	[Binding]
	public class ValidatingATypeStepDefinition
	{
		Category type;
		IEnumerable<ValidationResult> results;
        IEnumerable<ClientValidationRule> clientValidationRules;

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

        [Given(@"a new derived Type")]
        public void GivenANewDerivedType()
        {
            type = new CategoryWithSubCategory();
        }

        [Given]
        public void Given_I_call_skip_validation_method()
        {
            type.SkipValidation();
        }

		[When(@"I call the validate method")]
		public void WhenICallTheValidateMethod()
		{
			results = type.Validate();
		}

        [When(@"I call the GetClientValidationData method passing the property (.*)")]
        public void WhenICallTheGetClientValidationDataMethodPassingThePropertyName(string propertyName)
        {
            clientValidationRules = type.GetClientValidationData(propertyName);
        }

        [Then(@"the client validation rule collection should be have the (.*) rule")]
        public void ThenTheClientValidationRuleCollectionShouldBeHaveThe_Rule(string rule)
        {
            Assert.IsTrue(clientValidationRules.Any(c => c.ValidationType == rule));
        }

		[Then(@"the count of validationResult collection should be (.*)")]
		public void ThenTheCountOfValidationResultCollectionShouldBe(int count)
		{
			Assert.AreEqual(count, results.Count());
		}
	}
}

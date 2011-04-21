using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

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

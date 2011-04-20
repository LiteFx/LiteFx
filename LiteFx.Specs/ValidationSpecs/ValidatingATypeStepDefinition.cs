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

        [Given(@"a valid Super Type")]
        public void GivenAValidSuperType()
        {
            type = new SuperCategory() { Name = "Valid Type", Rank = 1 };
        }

        [Given(@"a invalid Super Type")]
        public void GivenAInvalidSuperType()
        {
            type = new SuperCategory();
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

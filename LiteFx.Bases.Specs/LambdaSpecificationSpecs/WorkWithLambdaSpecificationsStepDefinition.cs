using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Bases.Specs.LambdaSpecificationSpecs
{
    [Binding]
    public class WorkWithLambdaSpecificationsStepDefinition
    {
        protected Product givenProduct;
        protected ProductDiscontinuedSpec givenProductDiscontinuedSpec;
        protected bool result;

        [Given(@"I have a discontinued Product")]
        public void GivenIHaveADiscontinuedProduct()
        {
            givenProduct = new Product() { Nome = "Test Product", Quantity = 0, Discontinued = true };
        }

        [Given(@"I have a not discontinued Product")]
        public void GivenIHaveANotDiscontinuedProduct()
        {
            givenProduct = new Product() { Nome = "Test Product", Quantity = 5, Discontinued = false };
        }

        [Given(@"I have Product Discontinued Specification")]
        public void GivenIHaveProductDiscontinuedSpecification()
        {
            givenProductDiscontinuedSpec = new ProductDiscontinuedSpec();
        }

        [When(@"I check if the product is discontinued")]
        public void WhenICheckIfTheProductIsDiscontinued()
        {
            result = givenProductDiscontinuedSpec.IsSatisfiedBy(givenProduct);
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(bool expected)
        {
            Assert.AreEqual(expected, result);
        }
    }
}

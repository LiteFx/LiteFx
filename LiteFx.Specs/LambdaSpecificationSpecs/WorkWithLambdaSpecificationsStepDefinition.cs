using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Specs.LambdaSpecificationSpecs
{
    [Binding]
    public class WorkWithLambdaSpecificationsStepDefinition
    {
        protected Product givenProduct;
        protected ProductDiscontinuedSpec givenProductDiscontinuedSpec;
        protected PriceSpec givenPriceSpec;
        protected bool result;

        [Given(@"I have a discontinued Product")]
        public void GivenIHaveADiscontinuedProduct()
        {
            givenProduct = new Product() { Nome = "Test Product", Quantity = 0, Discontinued = true, Price = 1.99m };
        }

        [Given(@"I have a not discontinued Product")]
        public void GivenIHaveANotDiscontinuedProduct()
        {
            givenProduct = new Product() { Nome = "Test Product", Quantity = 5, Discontinued = false, Price = 1.99m };
        }

        [Given(@"I have a discontinued Derived Product")]
        public void GivenIHaveADiscontinuedDerivedProduct()
        {
            givenProduct = new DerivedProduct() { Nome = "Test Product", Quantity = 0, Discontinued = true, Price = 1.99m };
        }

        [Given(@"I have Product Discontinued Specification")]
        public void GivenIHaveProductDiscontinuedSpecification()
        {
            givenProductDiscontinuedSpec = new ProductDiscontinuedSpec();
        }

        [Given(@"a Price Specification")]
        public void GivenAPriceSpecification()
        {
            givenPriceSpec = new PriceSpec();
        }

        [When(@"I check if the product is discontinued")]
        public void WhenICheckIfTheProductIsDiscontinued()
        {
            result = givenProductDiscontinuedSpec.IsSatisfiedBy(givenProduct);
        }

        [When(@"I check if the product satisfy the two specifications")]
        public void WhenICheckIfTheProductSatisfyTheTwoSpecifications()
        {
            result = (givenProductDiscontinuedSpec & givenPriceSpec).IsSatisfiedBy(givenProduct);
        }

        [When(@"I check if the product satisfy one of the two specifications")]
        public void WhenICheckIfTheProductSatisfyOneOfTheTwoSpecifications()
        {
            result = (givenProductDiscontinuedSpec | givenPriceSpec).IsSatisfiedBy(givenProduct);
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(bool expected)
        {
            Assert.AreEqual(expected, result);
        }
    }
}

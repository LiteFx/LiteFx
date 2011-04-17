using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Specs.EntitySpecs
{
    [Binding]
    class EntitiesEqualityStepDefinition
    {
        EntityBase<int> entity;
        EntityBase<int> anotherEntity;

        bool equality;

        [Given(@"I have a product instance with the id (.*)")]
        public void GivenIHaveAProductInstanceWithTheId(int id)
        {
            entity = new Product() { Id = id };
        }

        [Given(@"I have another product instance with the id (.*)")]
        public void GivenIHaveAnotherProductInstanceWithTheId(int id)
        {
            anotherEntity = new Product() { Id = id };
        }

        [Given(@"I have a category instance with the id (.*)")]
        public void GivenIHaveACategoryInstanceWithTheId(int id)
        {
            anotherEntity = new Category() { Id = id };
        }

        [When(@"I compare the two instances")]
        public void WhenICompareTheTwoInstances()
        {
            equality = entity == anotherEntity;
        }

        [Then(@"the equality should be (.*)")]
        public void ThenTheEqualityShouldBe(bool result)
        {
            Assert.AreEqual(result, this.equality);
        }
    }
}

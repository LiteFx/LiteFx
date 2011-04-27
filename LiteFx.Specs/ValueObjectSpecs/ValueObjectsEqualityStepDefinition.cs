using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Specs.ValueObjectSpecs
{
    [Binding]
    public class ValueObjectsEqualityStepDefinition
    {
        ImmutableValueObject valueObject;
        ImmutableValueObject anotherValueObject;

        bool equality = false;

        [Given(@"I have a value object instance")]
        public void GivenIHaveAValueObjectInstance()
        {
            valueObject = new ImmutableValueObject("same");
        }

        [Given(@"I have another value object instance")]
        public void GivenIHaveAnotherValueObjectInstance()
        {
            anotherValueObject = new ImmutableValueObject("same");
        }

        [Then(@"the equality of the value objects should be true")]
        public void ThenTheEqualityOfTheValueObjectsShouldBeTrue()
        {
            Assert.AreEqual(true, equality);
        }

        [When(@"I compare the two value objects instances")]
        public void WhenICompareTheTwoValueObjectsInstances()
        {
            equality = valueObject.Equals(anotherValueObject);
        }
    }
}

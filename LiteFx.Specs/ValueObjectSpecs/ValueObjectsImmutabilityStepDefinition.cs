using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Specs
{
    [Binding]
    public class ValueObjectsImmutabilityStepDefinition
    {
        private MutableValueObject mutableValueObject;
        private Exception exception;

        [Given("a mutable ValueObject")]
        public void GivenAMutableValueObject()
        {
            mutableValueObject = null;
        }

        [When("I instantiate this mutable object")]
        public void WhenIInstantiateThisMutableObject()
        {
            try
            {
                mutableValueObject = new MutableValueObject();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        [Then("a exception need to be throw")]
        public void ThenAExceptionNeedToBeThrow()
        {
            Assert.IsNotNull(exception);
        }

        private ImmutableValueObject immutableValueObject;

        [Given("a immutable ValueObject")]
        public void GivenAImmutableValueObject()
        {
            immutableValueObject = null;
        }

        [When("I instantiate this immutable object")]
        public void WhenIInstantiateThisImmutableObject()
        {
            immutableValueObject = new ImmutableValueObject();
        }

        [Then("it can not be null")]
        public void ThenItCanNotBeNull()
        {
            Assert.IsNotNull(immutableValueObject);
        }
    }
}

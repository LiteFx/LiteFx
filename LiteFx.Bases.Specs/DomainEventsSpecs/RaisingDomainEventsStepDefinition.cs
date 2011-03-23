using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace LiteFx.Bases.Specs.DomainEventsSpecs
{
    [Binding]
    public class RaisingDomainEventsStepDefinition
    {
        [Given(@"I have registered a ordinary domain event into DomainEvent static class")]
        public void GivenIHaveRegisteredAOrdinaryDomainEventIntoDomainEventStaticClass()
        {
            DomainEvents.DomainEvents.RegisterDomainEventHandler(new OrdinaryEventHandler());
        }

        [Then(@"my handler should be called")]
        public void ThenMyHandlerShouldBeCalled()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"the ordinary event happen")]
        public void WhenTheOrdinaryEventHappen()
        {
            ScenarioContext.Current.Pending();
        }
    }
}

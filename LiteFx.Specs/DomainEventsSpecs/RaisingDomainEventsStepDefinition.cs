using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiteFx.Specs.DomainEventsSpecs
{
    [Binding]
    public class RaisingDomainEventsStepDefinition
    {
        protected OrdinarySubject subject;
        public static bool DomainEventHandlerWasCalled { get; set; }

        [Given(@"I have registered a ordinary domain event handler into DomainEvent static class")]
        public void GivenIHaveRegisteredAOrdinaryDomainEventHandlerIntoDomainEventStaticClass()
        {
            DomainEvents.DomainEvents.RegisterDomainEventHandler(new OrdinaryEventHandler());
        }

        [Given(@"I have registered a async domain event handler into DomainEvent static class")]
        public void GivenIHaveRegisteredAAsyncDomainEventHandlerIntoDomainEventStaticClass()
        {
            DomainEvents.DomainEvents.RegisterAsyncDomainEventHandler(new AsyncEventHandler());
        }

        [Given(@"a ordinary subject")]
        public void GivenAOrdinarySubject()
        {
            subject = new OrdinarySubject();
        }

        [When(@"I set a value in the ordinary subject")]
        public void WhenISetAValueInTheOrdinarySubject()
        {
            subject.OrdinaryValue = 5;
        }

        [When(@"the ordinary event happen")]
        public void WhenTheOrdinaryEventHappen()
        {
            DomainEvents.DomainEvents.Raise(new OrdinaryEvent(subject));
        }

        [When(@"the ordinary event happen asynchronously")]
        public void WhenTheOrdinaryEventHappenAsynchronously()
        {
            DomainEvents.DomainEvents.Raise(new OrdinaryEvent(subject));
        }

        [Then(@"my handler should be called")]
        public void ThenMyHandlerShouldBeCalled()
        {
            Assert.IsTrue(DomainEventHandlerWasCalled);
        }

        [Given(@"I have registered a ordinary action into DomainEvent static class")]
        public void GivenIHaveRegisteredAOrdinaryActionIntoDomainEventStaticClass()
        {
            Action<OrdinaryEvent> callback = (OrdinaryEvent @event) => DomainEventHandlerWasCalled = true;
            DomainEvents.DomainEvents.RegisterCallback<OrdinaryEvent>(callback);
        }

        [Then(@"my action should be called")]
        public void ThenMyActionShouldBeCalled()
        {
            Assert.IsTrue(DomainEventHandlerWasCalled);
        }
    }
}

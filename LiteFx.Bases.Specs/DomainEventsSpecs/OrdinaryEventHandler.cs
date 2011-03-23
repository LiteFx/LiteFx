using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases.Specs.DomainEventsSpecs
{
    public class OrdinaryEventHandler : DomainEvents.IDomainEventHandler<OrdinaryEvent>
    {
        public void HandleDomainEvent(OrdinaryEvent domainEvent)
        {
            RaisingDomainEventsStepDefinition.DomainEventHandlerWasCalled = true;
        }
    }
}

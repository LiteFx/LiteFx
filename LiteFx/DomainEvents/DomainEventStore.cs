using System;
using System.Collections.Generic;
using System.Linq;

namespace LiteFx.DomainEvents
{
    public class DomainEventStore : IDomainEventStore
    {
        List<Action> domainEventsDispatchers;

        public DomainEventStore()
        {
            domainEventsDispatchers = new List<Action>();
        }

        public void Save(IEnumerable<Action> domainEventDispatcher)
        {
            domainEventsDispatchers.AddRange(domainEventDispatcher);
        }

        public IEnumerable<Action> GetAll()
        {
            return domainEventsDispatchers.AsEnumerable();
        }
    }
}

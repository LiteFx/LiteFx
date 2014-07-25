using System.Collections.Generic;
using System.Linq;

namespace LiteFx.DomainEvents
{
    public class DomainEventStore : IDomainEventStore
    {
        List<IDomainEvent> domainEvents;

        public DomainEventStore()
        {
            domainEvents = new List<IDomainEvent>();
        }

        public void Save(IDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

        public IEnumerable<IDomainEvent> GetAll()
        {
            return domainEvents.AsEnumerable();
        }
    }
}

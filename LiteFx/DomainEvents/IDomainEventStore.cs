using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.DomainEvents
{
    public interface IDomainEventStore
    {
        void Save(IDomainEvent domainEvent);
        IEnumerable<IDomainEvent> GetAll();
    }
}

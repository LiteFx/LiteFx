using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.DomainEvents
{
    public interface IDomainEventStore
    {
        IEnumerable<Action> GetAll();
        void Save(IEnumerable<Action> domainEventDispatcher);
    }

}

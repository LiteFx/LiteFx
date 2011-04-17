using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Specs.DomainEventsSpecs
{
    public class OrdinaryEvent : DomainEvents.IDomainEvent<OrdinarySubject>
    {
        public OrdinaryEvent(OrdinarySubject subject)
        {
            this.subject = subject;
        }

        private OrdinarySubject subject;

        public OrdinarySubject Subject
        {
            get { return subject; }
        }
    }
}

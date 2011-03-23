using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases.Specs.DomainEventsSpecs
{
    public class OrdinarySubject
    {
        public OrdinarySubject()
        {
            ordinaryValue = 1;
        }
        private int ordinaryValue;
        public int OrdinaryValue { get { return ordinaryValue; } set { ordinaryValue = value; } }
    }
}

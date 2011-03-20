using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases.Specs
{
    public class ImmutableValueObject : ValueObjectBase
    {
        public string ImmutableProperty { get; private set; }
    }
}

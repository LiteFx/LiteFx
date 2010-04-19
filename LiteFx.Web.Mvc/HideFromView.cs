using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Web.Mvc
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class HideFromViewAttribute : Attribute
    {
        public HideFromViewAttribute() { }
    }
}

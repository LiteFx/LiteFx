using System;

namespace LiteFx.Web.Mvc
{
    [Obsolete("Use the System.ComponentModel.DataAnnotations.ScaffoldColumnAttribute(false) instead.")]
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class HideFromViewAttribute : Attribute
    {
    }
}

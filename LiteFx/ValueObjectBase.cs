using System;
using System.Linq;

namespace LiteFx
{
	public class ValueObjectBase : IEquatable<ValueObjectBase>
	{
		protected ValueObjectBase()
		{
			var properties = GetType().GetProperties();

			if (properties.Any(prop => prop.CanWrite && (prop.GetSetMethod() != null)))
			{
				throw new Exception("ValueObject must be immutable.");
			}
		}

		public override bool Equals(object obj)
		{
			ValueObjectBase valueObjectBase = obj as ValueObjectBase;
			if (valueObjectBase != null)
				return Equals(valueObjectBase);

			return false;
		}

		public virtual bool Equals(ValueObjectBase other)
		{
			if (ReferenceEquals(this, other))
				return true;

			return AllPropertiesAreEquals(other);
		}

		private bool AllPropertiesAreEquals(ValueObjectBase other)
		{
			var properties = other.GetType().GetProperties();

			return properties.All(prop => prop.GetValue(this, null) == prop.GetValue(other, null));
		}
	}
}

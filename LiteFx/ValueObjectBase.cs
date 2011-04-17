using System;

namespace LiteFx
{
    public class ValueObjectBase : IEquatable<ValueObjectBase>
    {
        protected ValueObjectBase()
        {
            var properties = GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.CanWrite && (prop.GetSetMethod() != null))
                        throw new Exception("ValueObject must be immutable.");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ValueObjectBase)
                return Equals((ValueObjectBase)obj);
            else
                return false;
        }

        public virtual bool Equals(ValueObjectBase other)
        {
            if (ReferenceEquals(this, other))
                return true;
            else
                return AllPropertiesAreEquals(other);
        }

        private bool AllPropertiesAreEquals(ValueObjectBase other) 
        {
            var properties = other.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.GetValue(this, null) != prop.GetValue(other, null))
                    return false;
            }

            return true;
        }
    }
}

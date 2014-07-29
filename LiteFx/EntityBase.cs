using System;
using System.ComponentModel.DataAnnotations;

namespace LiteFx
{
	public abstract class EntityBase { }

	/// <summary>
	/// Base class for entities.
	/// </summary>
	/// <typeparam name="TId">Type of id.</typeparam>
	public abstract class EntityBase<TId> : EntityBase, IEquatable<EntityBase<TId>>
		where TId : IEquatable<TId>
	{
        private int? hashcodeCache;

		/// <summary>
		/// Entity id.
		/// </summary>
		[ScaffoldColumn(false)]
		public virtual TId Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual bool Transient { get { return Id.Equals(default(TId)); } }

		public virtual bool Equals(EntityBase<TId> other)
		{
			if (ReferenceEquals(other, null)) return false;

            if (ReferenceEquals(this, other)) return true;

            if (this.Transient || other.Transient) return false;

			if (!isSameTypeOf(other)) return false;

			return Id.Equals(other.Id);
		}

		private bool isSameTypeOf(EntityBase<TId> other)
		{
			return GetType().Equals(other.GetType());
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return base.Equals(obj);

			if (!(obj is EntityBase<TId>))
				return false;
			else
				return Equals(obj as EntityBase<TId>);
		}

		public override int GetHashCode()
		{
            if (hashcodeCache.HasValue) return hashcodeCache.Value;

			if (Transient)
				hashcodeCache = base.GetHashCode();
            else
			    hashcodeCache = GetType().GetHashCode() + Id.GetHashCode();

            return hashcodeCache.Value;
		}

		public static bool operator ==(EntityBase<TId> left, EntityBase<TId> right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
				return true;
			if (ReferenceEquals(left, null) && !ReferenceEquals(right, null))
				return false;

			return left.Equals(right);
		}

		public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
		{
			if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
				return false;
			if (ReferenceEquals(left, null) && !ReferenceEquals(right, null))
				return true;

			return !left.Equals(right);
		}
	}
}
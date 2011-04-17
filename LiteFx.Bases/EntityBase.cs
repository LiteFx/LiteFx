using System;
using System.ComponentModel.DataAnnotations;

namespace LiteFx.Bases
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    /// <typeparam name="TId">Type of id.</typeparam>
    public abstract class EntityBase<TId> : IEquatable<EntityBase<TId>>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// Entity id.
        /// </summary>
        [ScaffoldColumn(false)]
        public virtual TId Id { get; set; }



        public bool Equals(EntityBase<TId> other)
        {
            if (!AreSameType(other)) return false;

            return this.Id.Equals(other.Id);
        }

        private bool AreSameType(EntityBase<TId> other)
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

        public static bool operator ==(EntityBase<TId> left, EntityBase<TId> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
        {
            return !left.Equals(right);
        }
    }
}

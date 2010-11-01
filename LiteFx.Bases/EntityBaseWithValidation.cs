using System;
using LiteFx.Bases.Validation;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LiteFx.Bases
{
    /// <summary>
    /// EntityBase implementation that supports validation.
    /// </summary>
    /// <typeparam name="T">Type of the entity. It will help the validation engine to discover wich object it will handle.</typeparam>
    /// <typeparam name="TId">Type of id.</typeparam>
    public abstract class EntityBaseWithValidation<T, TId> : EntityBase<TId>, IValidatableEntity
        where T : EntityBaseWithValidation<T, TId>
        where TId : IEquatable<TId>
    {

        private static Assert<T> assert;
        /// <summary>
        /// Instance of assertion class to perform validations.
        /// </summary>
        protected static Assert<T> Assert
        {
            get
            {
                return (assert ?? (assert = new Assert<T>()));
            }
        }

        #region IValidation Members
        

        /// <summary>
        /// Verify if the entity is valid, if it is not valid throws an <see cref="LiteFx.Bases.BusinessException"/>.
        /// </summary>
        /// <exception cref="LiteFx.Bases.BusinessException">This exception was throw if the Entity is not valid.</exception>
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (assert == null)
                this.ConfigureValidation();

            return Assert.Validate((T)this);
        }

        public abstract void ConfigureValidation();
        #endregion

        
    }
}

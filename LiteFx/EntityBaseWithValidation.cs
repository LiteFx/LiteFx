using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiteFx.Validation;

namespace LiteFx
{
    /// <summary>
    /// EntityBase implementation that supports validation.
    /// </summary>
    /// <typeparam name="TId">Type of id.</typeparam>
    public abstract class EntityBaseWithValidation<TId> : EntityBase<TId>, IValidatableEntity
        where TId : IEquatable<TId>
    {
        private static Assert assert;

        /// <summary>
        /// Instance of assertion class to perform validations.
        /// </summary>
        protected static Assert<T> Assert<T>()
        {
            if (assert == null)
                assert = new Assert<T>();

            return (Assert<T>)assert;
        }

        public EntityBaseWithValidation()
        {
            if (assert != null)
                assert.AssertionsExecuted = false;
        }

        #region IValidation Members
        public virtual IEnumerable<ValidationResult> Validate()
        {
            ValidationContext validationContext = new ValidationContext(this, null, null);

            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (assert == null)
                this.ConfigureValidation();

            if (!Validator.TryValidateObject(this, validationContext, validationResults, true))
            {
                assert.Validate(this, validationResults);
            }

            return validationResults;
        }

        /// <summary>
        /// Verify if the entity is valid, if it is not valid throws an <see cref="LiteFx.Bases.BusinessException"/>.
        /// </summary>
        /// <exception cref="LiteFx.Bases.BusinessException">This exception was throw if the Entity is not valid.</exception>
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (assert == null)
                this.ConfigureValidation();

            List<ValidationResult> validationResults = new List<ValidationResult>();

            return assert.Validate(this, validationResults);
        }

        public abstract void ConfigureValidation();
        #endregion
    }
}

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
        private IAssert assert;

        /// <summary>
        /// Instance of assertion class to perform validations.
        /// </summary>
        protected IAssert<T> Assert<T>()
        {
            if (assert == null)
                assert = new Assert<T>();

            return (IAssert<T>)assert;
        }

        #region IValidation Members
        public virtual IEnumerable<ValidationResult> Validate()
        {
            ValidationContext validationContext = new ValidationContext(this, null, null);

            List<ValidationResult> validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(this, validationContext, validationResults, true);

            return validationResults;
        }

        /// <summary>
        /// Verify if the entity is valid."/>.
        /// </summary>
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

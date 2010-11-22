using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LiteFx.Bases.Validation;

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
                Assert.Validate((T)this, validationResults);
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

            return Assert.Validate((T)this, validationResults);
        }

        public abstract void ConfigureValidation();
        #endregion
    }
}

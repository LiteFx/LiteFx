using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LiteFx.Validation;
using LiteFx.Validation.ClientValidationRules;

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
		protected virtual IAssert<T> Assert<T>()
		{
			if (assert == null)
				assert = new Assert<T>();

			return (IAssert<T>)assert;
		}

		#region IValidation Members
        /// <summary>
        /// Verify the entity against the domain rules described in ConfigureValidation method.
        /// </summary>
        /// <returns>Collection of <see cref="ValidationResults"/>.</returns>
		public virtual IEnumerable<ValidationResult> Validate()
		{
			ValidationContext validationContext = new ValidationContext(this, null, null);

			List<ValidationResult> validationResults = new List<ValidationResult>();

			Validator.TryValidateObject(this, validationContext, validationResults, true);

			return validationResults;
		}

		/// <summary>
		/// Verify if the entity is valid.
		/// </summary>
		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (skipValidation) return Enumerable.Empty<ValidationResult>();

			if (assert == null)
				ConfigureValidation();

			List<ValidationResult> validationResults = new List<ValidationResult>();

			return assert.Validate(this, validationResults);
		}

		private bool skipValidation = false;

        /// <summary>
        /// When called every call of Validate method will always return an empty collection of <see cref="ValidationResults"/>, even if the entity is in an invalid state.
        /// </summary>
		public virtual void SkipValidation()
		{
			skipValidation = true;
		}

		/// <summary>
		/// Implement this method to configure the fluent validation for the entity.
		/// </summary>
		/// <example>
		/// <code lang="cs">
		/// <![CDATA[
		/// public class MyEntity : EntityBaseWithValidation<long>
		/// {
		///     public override void ConfigureValidation()
		///     {
		///         Assert<MyEntity>()
		///             .That(e => e.Name)
		///             .IsRequired();
		///     }
		/// }
		/// ]]>
		/// </code>
		/// </example>
		public abstract void ConfigureValidation();

        /// <summary>
        /// Returns client validation rule to help client validation libraries.
        /// </summary>
        /// <param name="propertyName">Property name reference to get the client validation rules.</param>
        /// <returns><see cref="ClientValidationRule"/></returns>
		public virtual IEnumerable<ClientValidationRule> GetClientValidationData(string propertyName)
		{
			if (assert == null)
				ConfigureValidation();

            return assert.GetClientValidationData(propertyName, this);
		}
		#endregion
	}
}

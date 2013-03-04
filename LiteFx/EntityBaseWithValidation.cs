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
			if (skipValidation) return Enumerable.Empty<ValidationResult>();

			if (assert == null)
				ConfigureValidation();

			List<ValidationResult> validationResults = new List<ValidationResult>();

			return assert.Validate(this, validationResults);
		}

		private bool skipValidation = false;

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
		///             .IsNotNullOrEmpty();
		///     }
		/// }
		/// ]]>
		/// </code>
		/// </example>
		public abstract void ConfigureValidation();

		public virtual IEnumerable<ClientValidationRule> GetClientValidationData(string propertyName)
		{
			if (assert == null)
				ConfigureValidation();

			var assertions = assert.Assertions.Where(a => a.AccessorMemberNames.Contains(propertyName)).Select(a => a);
			foreach (var assertion in assertions)
			{
				foreach (var predicate in assertion.BasePredicates)
				{
					if (predicate.ClienteValidationRule != null && assertion.WhenAssertion == null)
					{
						predicate.ClienteValidationRule.ErrorMessage = string.Format(predicate.ValidationMessage, ValidationHelper.ResourceManager.GetString(propertyName));
						yield return predicate.ClienteValidationRule;
					}
				}
			}
		}
		#endregion
	}
}

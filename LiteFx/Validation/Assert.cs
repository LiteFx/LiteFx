using LiteFx.Validation.ClientValidationRules;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LiteFx.Validation
{
    public class Assert : IAssert
    {
        private List<Assertion> assertions;

        /// <summary>
        /// Collection of assertions that will be check against the object.
        /// </summary>
        public List<Assertion> Assertions
        {
            get { return assertions ?? (assertions = new List<Assertion>()); }
        }

        /// <summary>
        /// Check all assertions against the instanceReference.
        /// </summary>
        /// <param name="instanceReference">Instance that will be validated.</param>
        /// <param name="validationResults">Validation results reference to be incremented if any errors are found.</param>
        /// <returns>The validation results reference collection.</returns>
        public IEnumerable<ValidationResult> Validate(object instanceReference, IList<ValidationResult> validationResults)
        {
            foreach (var item in Assertions)
            {
                item.Evaluate(instanceReference, validationResults);
            }

            return validationResults;
        }

        /// <summary>
        /// Returns client validation rule to help client validation libraries.
        /// </summary>
        /// <param name="propertyName">Property name reference to get the client validation rules.</param>
        /// <returns><see cref="ClientValidationRule"/></returns>
        public IEnumerable<ClientValidationRule> GetClientValidationData(string propertyName, EntityBase entity)
        {
            var predicates = Assertions
                .Where(a => a.AccessorMemberNames.Contains(propertyName) && a.WhenAssertion == null)
                    .SelectMany(a => a.BasePredicates)
                        .Where(p => p.ClienteValidationRule != null);

            foreach (var predicate in predicates)
            {
                predicate.ClienteValidationRule.ErrorMessage = string.Format(predicate.ValidationMessage, ValidationHelper.ResourceManager.GetString(propertyName));
                yield return predicate.ClienteValidationRule;
            }

			var assertionsWithWhen = Assertions
				.Where(a => a.AccessorMemberNames.Contains(propertyName) && a.WhenAssertion != null && a.BasePredicates.Any(p => p.ClienteValidationRule != null));

			foreach (var assertion in assertionsWithWhen)
			{
				if (assertion.WhenAssertion.Evaluate(entity, null))
				{
					var predicatesWithWhen = assertion.BasePredicates.Where(p => p.ClienteValidationRule != null);

					foreach (var predicate in predicatesWithWhen)
					{
						predicate.ClienteValidationRule.ErrorMessage = string.Format(predicate.ValidationMessage, ValidationHelper.ResourceManager.GetString(propertyName));
						yield return predicate.ClienteValidationRule;
					}
				}
			}
        }
    }

    public class Assert<T> : Assert, IAssert<T> { }
}

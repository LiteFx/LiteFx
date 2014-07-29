using LiteFx.Validation.ClientValidationRules;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LiteFx.Validation
{
	public interface IAssert
	{
		List<Assertion> Assertions { get; }
		IEnumerable<ValidationResult> Validate(object instanceReference, IList<ValidationResult> validationContext);
        IEnumerable<ClientValidationRule> GetClientValidationData(string propertyName);
	}

	public interface IAssert<out T> : IAssert
	{
	}
}

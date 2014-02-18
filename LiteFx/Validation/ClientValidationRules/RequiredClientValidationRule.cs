
using LiteFx.Properties;
namespace LiteFx.Validation.ClientValidationRules
{
	public class RequiredClientValidationRule : ClientValidationRule
	{
		public RequiredClientValidationRule()
		{
			ValidationType = "required";
			ErrorMessage = Resources.TheFieldXIsRequired;
		}
	}
}

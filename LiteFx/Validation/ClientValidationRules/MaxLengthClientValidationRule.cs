using LiteFx.Properties;

namespace LiteFx.Validation.ClientValidationRules
{
    public class MaxLengthClientValidationRule : ClientValidationRule
    {
        public MaxLengthClientValidationRule(int maxLength)
        {
            ValidationType = "length";
            ValidationParameters.Add("max", maxLength);
            ErrorMessage = string.Format(Resources.TheFieldXCanNotHaveMoreThanYCharacters, "{0}", maxLength);
        }
    }
}

using LiteFx.Properties;

namespace LiteFx.Validation.ClientValidationRules
{
    /// <summary>
    /// Rule to express the maximum field length.
    /// </summary>
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

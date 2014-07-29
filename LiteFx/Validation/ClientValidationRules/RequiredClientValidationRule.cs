using LiteFx.Properties;

namespace LiteFx.Validation.ClientValidationRules
{
    /// <summary>
    /// Rule to express that the field is required.
    /// </summary>
    public class RequiredClientValidationRule : ClientValidationRule
    {
        private static RequiredClientValidationRule _rule = new RequiredClientValidationRule();

        public static RequiredClientValidationRule Rule { get { return _rule; } }

        public RequiredClientValidationRule()
        {
            ValidationType = "required";
            ErrorMessage = Resources.TheFieldXIsRequired;
        }
    }
}
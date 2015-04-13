using LiteFx.Properties;
using LiteFx.Validation.ClientValidationRules;

namespace LiteFx.Validation
{
    public static partial class ValidationHelper
    {
        public static Validator<T, string> IsNullOrEmpty<T>(this Validator<T, string> validator)
        {
            return IsSatisfied(validator, string.IsNullOrEmpty, Resources.TheFieldXMustBeNullOrEmpty);
        }

        public static Validator<T, string> IsRequired<T>(this Validator<T, string> validator)
        {
            return IsNotNullOrEmpty(validator);
        }

        public static Validator<T, string> Required<T>(this Validator<T, string> validator)
        {
            return IsNotNullOrEmpty(validator);
        }

        public static Validator<T, string> IsNotNullOrEmpty<T>(this Validator<T, string> validator)
        {
            return IsSatisfied(validator, p => !string.IsNullOrEmpty(p), Resources.TheFieldXIsRequired, RequiredClientValidationRule.Rule);
        }

        public static Validator<T, string> IsEmpty<T>(this Validator<T, string> validator)
        {
            return IsSatisfied(validator, p => string.Empty == p, Resources.TheFieldXMustBeEmpty);
        }

        public static Validator<T, string> IsNotEmpty<T>(this Validator<T, string> validator)
        {
            RequiredClientValidationRule requiredClientValidationRule = new RequiredClientValidationRule();
            return IsSatisfied(validator, p => string.Empty != p, Resources.TheFieldXIsRequired, RequiredClientValidationRule.Rule);
        }

        public static Validator<T, string> MaxLength<T>(this Validator<T, string> validator, int maxLength)
        {
            MaxLengthClientValidationRule maxLengthClientValidationRule = new MaxLengthClientValidationRule(maxLength);
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.Trim().Length <= maxLength;
            }, string.Format(Resources.TheFieldXCanNotHaveMoreThanYCharacters, "{0}", maxLength), maxLengthClientValidationRule);
        }

        public static Validator<T, string> MinLength<T>(this Validator<T, string> validator, int minLength)
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.Trim().Length >= minLength;
            }, string.Format(Resources.TheFieldXCanNotHaveLessThanYCharacters, "{0}", minLength));
        }

        public static Validator<T, string> RangeLength<T>(this Validator<T, string> validator, int minLength, int maxLength)
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.Trim().Length >= minLength && p.Trim().Length <= maxLength;
            }, string.Format(Resources.TheFieldXCanNotHaveLessThanYCharacters, "{0}", minLength, maxLength));
        }

        public static Validator<T, string> Length<T>(this Validator<T, string> validator, int length)
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.Trim().Length == length;
            }, string.Format(Resources.TheFieldXMustHaveYCharacters, "{0}", length));
        }
    }
}

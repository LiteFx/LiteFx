using LiteFx.Properties;
using System.Text.RegularExpressions;

namespace LiteFx.Validation
{
    public static partial class ValidationHelper
    {
        public static Validator<T, string> IsSatisfiedByRegex<T>(this Validator<T, string> validator, Regex regex, string message)
        {
            return IsSatisfied(validator, p => regex.IsMatch(p), message);
        }

        public static Validator<T, string> ShouldBeAnEmail<T>(this Validator<T, string> validator, string message)
        {
            return IsSatisfiedByRegex(validator, new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"), Resources.TheFieldXShouldBeAnEmail);
        }

        public static Validator<T, string> ShouldBeALink<T>(this Validator<T, string> validator, string message)
        {
            return IsSatisfiedByRegex(validator, new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$"), Resources.TheFieldXShouldBeALink);
        }
    }
}

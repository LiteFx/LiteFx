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

        /// <summary>
        /// Validates string against this regex: /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator"></param>
        /// <returns></returns>
        public static Validator<T, string> ShouldBeAnEmail<T>(this Validator<T, string> validator)
        {
            return IsSatisfiedByRegex(validator, new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"), ResourceHelper.GetString("TheFieldXShouldBeAnEmail"));
        }

        /// <summary>
        /// Validates string against this regex: /^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$/.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator"></param>
        /// <returns></returns>
        public static Validator<T, string> ShouldBeALink<T>(this Validator<T, string> validator)
        {
            return IsSatisfiedByRegex(validator, new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$"), ResourceHelper.GetString("TheFieldXShouldBeALink"));
        }

        /// <summary>
        /// Validates string against this regex: /^[a-z0-9-]+$/.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="validator"></param>
        /// <returns></returns>
        public static Validator<T, string> ShouldBeASlug<T>(this Validator<T, string> validator)
        {
            return IsSatisfiedByRegex(validator, new Regex(@"^[a-z0-9-]+$"), ResourceHelper.GetString("TheFieldXShouldBeASlug"));
        }
    }
}

using LiteFx.Properties;
using LiteFx.Validation.ClientValidationRules;
using System;
using System.Linq.Expressions;
using System.Resources;

namespace LiteFx.Validation
{

	public static partial class ValidationHelper
	{
		private static ResourceManager resourceManager;
		public static ResourceManager ResourceManager
		{
			get { return resourceManager ?? (resourceManager = Resources.ResourceManager); }
			set
			{
				resourceManager = value;
			}
		}

		public static Validator<T, TResult> That<T, TResult>(this IAssert<T> assert, Expression<Func<T, TResult>> accessor)
		{
			var assertion = new Assertion<T, TResult>();
			assertion.Accessors.Add(new Accessor<T, TResult>(accessor));
			assert.Assertions.Add(assertion);
			return new Validator<T, TResult>(assertion);
		}

		public static Validator<T, TResult> And<T, TResult>(this Validator<T, TResult> validator, Expression<Func<T, TResult>> accessor)
		{
			validator.Assertion.Accessors.Add(new Accessor<T, TResult>(accessor));
			return validator;
		}

		public static Validator<T, TResult2> When<T, TResult1, TResult2>(this Validator<T, TResult1> validator, Expression<Func<T, TResult2>> accessor)
		{
			var whenAssertion = new Assertion<T, TResult2>();
			whenAssertion.Accessors.Add(new Accessor<T, TResult2>(accessor));
			validator.Assertion.WhenAssertion = whenAssertion;
			return new Validator<T, TResult2>(whenAssertion);
		}

		public static Validator<T, TResult> IsSatisfied<T, TResult>(this Validator<T, TResult> validator, Func<TResult, bool> expression, string message)
		{
			validator.Assertion.Predicates.Add(new Predicate<TResult>(expression, message));
			return validator;
		}

		public static Validator<T, TResult> IsSatisfied<T, TResult>(this Validator<T, TResult> validator, Func<TResult, bool> expression, string message, ClientValidationRule clientValidationRule)
		{
			validator.Assertion.Predicates.Add(new Predicate<TResult>(expression, message, clientValidationRule));
			return validator;
		}

		public static Validator<T, TResult> IsNull<T, TResult>(this Validator<T, TResult> validator)
		{
			return IsSatisfied(validator, p => p == null, ResourceHelper.GetString("TheFieldXMustBeNull"));
		}

        public static Validator<T, TResult> Required<T, TResult>(this Validator<T, TResult> validator)
        {
            return IsNotNull(validator);
        }

        public static Validator<T, TResult> IsRequired<T, TResult>(this Validator<T, TResult> validator)
        {
            return IsNotNull(validator);
        }

		public static Validator<T, TResult> IsNotNull<T, TResult>(this Validator<T, TResult> validator)
		{
            return IsSatisfied(validator, p => p != null, ResourceHelper.GetString("TheFieldXIsRequired"), RequiredClientValidationRule.Rule);
		}

		public static Validator<T, bool> IsTrue<T>(this Validator<T, bool> validator)
		{
			return IsSatisfied(validator, p => p, ResourceHelper.GetString("TheFieldXMustBeTrue"));
		}

		public static Validator<T, bool> IsFalse<T>(this Validator<T, bool> validator)
		{
			return IsSatisfied(validator, p => !p, ResourceHelper.GetString("TheFieldXMustBeFalse"));
		}

		public static Validator<T, double> IsNaN<T>(this Validator<T, double> validator)
		{
			return IsSatisfied(validator, p => double.IsNaN(p), ResourceHelper.GetString("TheFieldXCanNotBeANumber"));
		}

		public static Validator<T, double> IsaNumber<T>(this Validator<T, double> validator)
		{
			return IsSatisfied(validator, p => !double.IsNaN(p), ResourceHelper.GetString("TheFieldXMustBeANumber"));
		}
	}
}
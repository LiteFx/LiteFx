using System;
using System.Linq.Expressions;
using LiteFx.Specification;

namespace LiteFx.Validation.PtBr
{
	public static partial class ValidationHelperPtBr
	{
		public static Validator<T, TResult> Que<T, TResult>(this IAssert<T> assert, Expression<Func<T, TResult>> accessor)
		{
			return ValidationHelper.That(assert, accessor);
		}

		public static Validator<T, TResult> E<T, TResult>(this Validator<T, TResult> validator, Expression<Func<T, TResult>> accessor)
		{
			return ValidationHelper.And(validator, accessor);
		}

		public static Validator<T, TResult2> Quando<T, TResult1, TResult2>(this Validator<T, TResult1> validator, Expression<Func<T, TResult2>> accessor)
		{
			return ValidationHelper.When(validator, accessor);
		}

		public static Validator<T, TResult> Satisfaz<T, TResult>(this Validator<T, TResult> validator, Func<TResult, bool> expression, string message)
		{
			return ValidationHelper.IsSatisfied(validator, expression, message);
		}

		public static Validator<T, TResult> SejaNulo<T, TResult>(this Validator<T, TResult> validator)
		{
			return ValidationHelper.IsNull(validator);
		}

        public static Validator<T, TResult> Requerido<T, TResult>(this Validator<T, TResult> validator)
        {
            return ValidationHelper.Required(validator);
        }

        public static Validator<T, TResult> EhRequerido<T, TResult>(this Validator<T, TResult> validator)
        {
            return ValidationHelper.IsRequired(validator);
        }

		public static Validator<T, TResult> NaoSejaNulo<T, TResult>(this Validator<T, TResult> validator)
		{
			return ValidationHelper.IsNotNull(validator);
		}

		public static Validator<T, bool> SejaVerdadeiro<T>(this Validator<T, bool> validator)
		{
			return ValidationHelper.IsTrue(validator);
		}

		public static Validator<T, bool> SejaFalso<T>(this Validator<T, bool> validator)
		{
			return ValidationHelper.IsFalse(validator);
		}

		public static Validator<T, double> NaoSejaUmNumero<T>(this Validator<T, double> validator)
		{
			return ValidationHelper.IsNaN(validator);
		}

		public static Validator<T, double> SejaUmNumero<T>(this Validator<T, double> validator)
		{
			return ValidationHelper.IsaNumber(validator);
		}
	}
}

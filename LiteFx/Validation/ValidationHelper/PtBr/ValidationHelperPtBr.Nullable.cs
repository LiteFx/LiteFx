using System;

namespace LiteFx.Validation.PtBr
{
	public static partial class ValidationHelperPtBr
	{
		public static Validator<T, TResult?> Maximo<T, TResult>(this Validator<T, TResult?> validator, TResult max)
			where TResult : struct, IComparable<TResult>
		{
			return ValidationHelper.Max(validator, max);
		}

        public static Validator<T, TResult?> Maximo<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
            where TResult : struct, IComparable<TResult>
        {
            return ValidationHelper.Max(validator, value);
        }

        public static Validator<T, TResult?> MenorQue<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
            where TResult : struct, IComparable<TResult>
        {
            return ValidationHelper.LessThan(validator, value);
        }

        public static Validator<T, TResult?> MenorQueOuIgual<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
            where TResult : struct, IComparable<TResult>
        {
            return ValidationHelper.LessThanOrEqual(validator, value);
        }

        public static Validator<T, TResult?> Minimo<T, TResult>(this Validator<T, TResult?> validator, TResult min)
            where TResult : struct, IComparable<TResult>
        {
            return ValidationHelper.Min(validator, min);
        }

        public static Validator<T, TResult?> Minimo<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
            where TResult : struct, IComparable<TResult>
        {
            return ValidationHelper.Min(validator, value);
        }

        public static Validator<T, TResult?> MaiorQue<T, TResult>(this Validator<T, TResult?> validator, TResult value)
            where TResult : struct, IComparable<TResult>
        {
            return ValidationHelper.GreaterThan(validator, value);
        }

        public static Validator<T, TResult?> MaiorQue<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
            where TResult : struct, IComparable<TResult>
        {
            return ValidationHelper.GreaterThan(validator, value);
        }

        public static Validator<T, TResult?> MaiorQueOuIgual<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
            where TResult : struct, IComparable<TResult>
        {
            return ValidationHelper.GreaterThanOrEqual(validator, value);
        }

        public static Validator<T, TResult?> Entre<T, TResult>(this Validator<T, TResult?> validator, TResult min, TResult max)
            where TResult : struct, IComparable<TResult>
        {
            return ValidationHelper.Range(validator, min, max);
        }
	}
}

using System;

namespace LiteFx.Validation.PtBr
{
	public static class NullableValidationHelperPtBr
	{
		public static Validator<T, TResult?> Maximo<T, TResult>(this Validator<T, TResult?> validator, TResult max)
			where TResult : struct, IComparable<TResult>
		{
			return NullableValidationHelper.Max(validator, max);
		}
	}
}

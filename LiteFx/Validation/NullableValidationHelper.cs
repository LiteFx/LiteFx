using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteFx.Properties;

namespace LiteFx.Validation
{
	public static class NullableValidationHelper
	{
		public static Validator<T, TResult> IsSatisfied<T, TResult>(this Validator<T, TResult> validator, Func<TResult, bool> expression, string message)
		{
			return ValidationHelper.IsSatisfied(validator, expression, message);
		}

		#region IComparable
		public static Validator<T, TResult?> Max<T, TResult>(this Validator<T, TResult?> validator, TResult max)
			where TResult : struct, IComparable<TResult>
		{
			return IsSatisfied(validator, p =>
			{
				if (p == null) return true;
				return p.Value.CompareTo(max) <= 0;
			}, string.Format(Resources.TheFieldXCanNotBeGreaterThanY, "{0}", max));
		}
		
		public static Validator<T, TResult?> Max<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
			where TResult : struct, IComparable<TResult>
		{
			return IsSatisfied(validator, p =>
			{
				if (p == null) return true;
				return p.Value.CompareTo(value()) <= 0;
			}, string.Format(Resources.TheFieldXCanNotBeGreaterThanY, "{0}", value()));
		}

		public static Validator<T, TResult?> LessThan<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
			where TResult : struct, IComparable<TResult>
		{
			return IsSatisfied(validator, p =>
			{
				if (p == null) return true;
				return p.Value.CompareTo(value()) < 0;
			}, string.Format(Resources.TheFieldXCanNotBeGreaterThanY, "{0}", value()));
		}

		public static Validator<T, TResult?> LessThanOrEqual<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
			where TResult : struct, IComparable<TResult>
		{
			return Max(validator, value);
		}

		public static Validator<T, TResult?> Min<T, TResult>(this Validator<T, TResult?> validator, TResult min)
			where TResult : struct, IComparable<TResult>
		{
			return IsSatisfied(validator, p =>
			{
				if (p == null) return true;
				return p.Value.CompareTo(min) >= 0;
			}, string.Format(Resources.TheFieldXCanNotBeLessThanY, "{0}", min));
		}

		public static Validator<T, TResult?> Min<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
			where TResult : struct, IComparable<TResult>
		{
			return IsSatisfied(validator, p =>
			{
				if (p == null) return true;
				return p.Value.CompareTo(value()) >= 0;
			}, string.Format(Resources.TheFieldXCanNotBeLessThanY, "{0}", value()));
		}

		public static Validator<T, TResult?> GreaterThan<T, TResult>(this Validator<T, TResult?> validator, TResult value)
			where TResult : struct, IComparable<TResult>
		{
			return GreaterThan(validator, () => value);
		}

		public static Validator<T, TResult?> GreaterThan<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
			where TResult : struct, IComparable<TResult>
		{
			return IsSatisfied(validator, p =>
			{
				if (p == null) return true;
				return p.Value.CompareTo(value()) > 0;
			}, string.Format(Resources.TheFieldXShouldBeGreaterThanY, "{0}", value()));
		}

		public static Validator<T, TResult?> GreaterThanOrEqual<T, TResult>(this Validator<T, TResult?> validator, Func<TResult> value)
			where TResult : struct, IComparable<TResult>
		{
			return Min(validator, value);
		}

		public static Validator<T, TResult?> Range<T, TResult>(this Validator<T, TResult?> validator, TResult min, TResult max)
			where TResult : struct, IComparable<TResult>
		{
			return IsSatisfied(validator, p =>
			{
				if (p == null) return true;
				return p.Value.CompareTo(min) >= 0 && p.Value.CompareTo(max) <= 0;
			}, string.Format(Resources.TheFieldXMustBeBetweenYandZ, "{0}", min, max));
		}
		#endregion

	}
}

using LiteFx.Properties;
using System;

namespace LiteFx.Validation
{
    public static partial class ValidationHelper
    {
        public static Validator<T, TResult> Max<T, TResult>(this Validator<T, TResult> validator, TResult max)
            where TResult : IComparable<TResult>
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.CompareTo(max) <= 0;
            }, string.Format(Resources.TheFieldXCanNotBeGreaterThanY, "{0}", max));
        }

        public static Validator<T, TResult> Max<T, TResult>(this Validator<T, TResult> validator, Func<TResult> value)
            where TResult : IComparable<TResult>
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.CompareTo(value()) <= 0;
            }, string.Format(Resources.TheFieldXCanNotBeGreaterThanY, "{0}", value()));
        }

        public static Validator<T, TResult> LessThan<T, TResult>(this Validator<T, TResult> validator, Func<TResult> value)
            where TResult : IComparable<TResult>
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.CompareTo(value()) < 0;
            }, string.Format(Resources.TheFieldXCanNotBeLessThanY, "{0}", value()));
        }

        public static Validator<T, TResult> LessThanOrEqual<T, TResult>(this Validator<T, TResult> validator, Func<TResult> value)
            where TResult : IComparable<TResult>
        {
            return Max(validator, value);
        }

        public static Validator<T, TResult> Min<T, TResult>(this Validator<T, TResult> validator, TResult min)
            where TResult : IComparable<TResult>
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.CompareTo(min) >= 0;
            }, string.Format(Resources.TheFieldXCanNotBeLessThanY, "{0}", min));
        }

        public static Validator<T, TResult> Min<T, TResult>(this Validator<T, TResult> validator, Func<TResult> value)
            where TResult : IComparable<TResult>
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.CompareTo(value()) >= 0;
            }, string.Format(Resources.TheFieldXCanNotBeLessThanY, "{0}", value()));
        }

        public static Validator<T, TResult> GreaterThan<T, TResult>(this Validator<T, TResult> validator, TResult value)
            where TResult : IComparable<TResult>
        {
            return GreaterThan(validator, () => value);
        }

        public static Validator<T, TResult> GreaterThan<T, TResult>(this Validator<T, TResult> validator, Func<TResult> value)
            where TResult : IComparable<TResult>
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.CompareTo(value()) > 0;
            }, string.Format(Resources.TheFieldXShouldBeGreaterThanY, "{0}", value()));
        }

        public static Validator<T, TResult> GreaterThanOrEqual<T, TResult>(this Validator<T, TResult> validator, Func<TResult> value)
            where TResult : IComparable<TResult>
        {
            return Min(validator, value);
        }

        public static Validator<T, TResult> Range<T, TResult>(this Validator<T, TResult> validator, TResult min, TResult max)
            where TResult : IComparable<TResult>
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.CompareTo(min) >= 0 && p.CompareTo(max) <= 0;
            }, string.Format(Resources.TheFieldXMustBeBetweenYandZ, "{0}", min, max));
        }
    }
}

using System;
using LiteFx.Bases.Specification;
using System.Linq.Expressions;
using LiteFx.Bases.Properties;

namespace LiteFx.Bases.Validation
{

    public static class ValidationHelper
    {
        public static Validator<T, TResult> That<T, TResult>(this Assert<T> assert, Expression<Func<T, TResult>> accessor)
        {
            var assertion = new Assertion<T, TResult>();
            assertion.Accessors.Add(new Accessor<T, TResult>(accessor));
            assert.Assertions.Add(assertion);
            return new Validator<T,TResult>(assertion);
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

        public static Validator<T, TResult> IsNull<T, TResult>(this Validator<T, TResult> validator)
        {
            return IsSatisfied(validator, p => p == null, Resources.TheFieldXMustBeNull);
        }

        public static Validator<T, TResult> IsNotNull<T, TResult>(this Validator<T, TResult> validator)
        {
            return IsSatisfied(validator, p => p != null, Resources.TheFieldXMustBeNull);
        }

        public static Validator<T, bool> IsTrue<T>(this Validator<T, bool> validator)
        {
            return IsSatisfied(validator, p => p, Resources.TheFieldXMustBeTrue);
        }

        public static Validator<T, bool> IsFalse<T>(this Validator<T, bool> validator)
        {
            return IsSatisfied(validator, p => !p, Resources.TheFieldXMustBeFalse);
        }

        public static Validator<T, double> IsNaN<T>(this Validator<T, double> validator)
        {
            return IsSatisfied(validator, p => double.IsNaN(p), Resources.TheFieldXCanNotBeANumber);
        }

        public static Validator<T, double> IsNotNaN<T>(this Validator<T, double> validator)
        {
            return IsSatisfied(validator, p => !double.IsNaN(p), Resources.TheFieldXMustBeANumber);
        }

        public static Validator<T, string> IsNullOrEmpty<T>(this Validator<T, string> validator)
        {
            return IsSatisfied(validator, p => string.IsNullOrEmpty(p), Resources.TheFieldXMustBeNullOrEmpty);
        }

        public static Validator<T, string> IsNotNullOrEmpty<T>(this Validator<T, string> validator)
        {
            return IsSatisfied(validator, p => !string.IsNullOrEmpty(p), Resources.TheFieldXCanNotBeNullOrEmpty);
        }

        public static Validator<T, string> IsEmpty<T>(this Validator<T, string> validator)
        {
            return IsSatisfied(validator, p => string.Empty == p, Resources.TheFieldXMustBeEmpty);
        }

        public static Validator<T, string> IsNotEmpty<T>(this Validator<T, string> validator)
        {
            return IsSatisfied(validator, p => string.Empty != p, Resources.TheFieldXCanNotBeEmpty);
        }

        public static Validator<T, string> MaxLength<T>(this Validator<T, string> validator, int maxLength)
        {
            return IsSatisfied(validator, p => 
            { 
                if (p == null) return true; 
                return p.Trim().Length <= maxLength; 
            }, string.Format(Resources.TheFieldXCanNotHaveMoreThanYCharacters, "{0}", maxLength));
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

        public static Validator<T, TResult> Max<T, TResult>(this Validator<T, TResult> validator, TResult max)
            where TResult : IComparable<TResult>
        {
            return IsSatisfied(validator, p =>
                {
                    if(p == null) return true;
                    return p.CompareTo(max) <= 0;
                }, string.Format(Resources.TheFieldXCanNotBeGreaterThanY, "{0}", max));
        }

        public static Validator<T, TResult> Min<T, TResult>(this Validator<T, TResult> validator, TResult min)
            where TResult : IComparable<TResult>
        {
            return IsSatisfied(validator, p =>
                {
                    if(p == null) return true;
                    return p.CompareTo(min) >= 0;
                }, string.Format(Resources.TheFieldXCanNotBeLessThanY, "{0}", min));
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

        public static Validator<T, TResult> AreEquals<T, TResult>(this Validator<T, TResult> validator, TResult other)
            where TResult : IEquatable<TResult>
        {
            return IsSatisfied(validator, p => p == null || p.Equals(other), string.Format(Resources.TheFieldXMustBeEqualsY, "{0}", other));
        }

        public static Validator<T, TResult> NotEquals<T, TResult>(this Validator<T, TResult> validator, TResult other)
            where TResult : IEquatable<TResult>
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return !p.Equals(other);
            }, string.Format(Resources.TheFieldXMustBeDifferentThanY, "{0}", other));
        }

        public static Validator<T, T> IsSatisfiedBy<T>(this Validator<T, T> validator, ISpecification<T> spec) 
        {
            return IsSatisfied(validator, spec.IsSatisfiedBy, "Spec Failure.");
        }
    }
}
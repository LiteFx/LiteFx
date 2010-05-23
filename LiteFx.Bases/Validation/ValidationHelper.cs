using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using LiteFx.Bases.Repository;
using LiteFx.Bases.Specification;
using System.Linq.Expressions;
using LiteFx.Bases.Properties;

namespace LiteFx.Bases.Validation
{

    public static class ValidationHelper
    {
        public static Validator<T, TResult> That<T, TResult>(this Assert<T> assert, Expression<Func<T, TResult>> accessor)
        {
            var assertion = new Assertion<T, TResult>(accessor);
            return new Validator<T,TResult>(assertion, assert);
        }

        public static Validator<T, TResult> IsSatisfied<T, TResult>(this Validator<T, TResult> validator, Func<TResult, bool> expression, string message)
        {
            return IsSatisfied(validator, expression, message, true);
        }

        public static Validator<T, TResult> IsSatisfied<T, TResult>(this Validator<T, TResult> validator, Func<TResult, bool> expression, string message, bool accessorCanBeNull)
        {
            validator.Assertion.Predicate = expression;
            validator.Assertion.ValidationMessage = message;
            validator.Assertion.AccessorCanBeNull = accessorCanBeNull;
            validator.EndValidation();
            return That(validator.AssertReference, validator.Assertion.Accessor);
        }

        public static Validator<T, TResult> IsNull<T, TResult>(this Validator<T, TResult> validator)
        {
            return IsSatisfied(validator, p => p == null, Resources.TheFieldXMustBeNull);
        }

        public static Validator<T, TResult> IsNotNull<T, TResult>(this Validator<T, TResult> validator)
        {
            return IsSatisfied(validator, p => p != null, Resources.TheFieldXMustBeNull);
        }

        public static Validator<T, string> IsNullOrEmpty<T>(this Validator<T, string> validator)
        {
            return IsSatisfied(validator, p => string.IsNullOrEmpty(p), Resources.TheFieldXMustBeNullOrEmpty);
        }

        public static Validator<T, string> IsNotNullOrEmpty<T>(this Validator<T, string> validator)
        {
            return IsSatisfied(validator, p => !string.IsNullOrEmpty(p), Resources.TheFieldXCanNotBeNullOrEmpty);
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

        public static Validator<T, TResult> Equals<T, TResult>(this Validator<T, TResult> validator, TResult other)
            where TResult : IEquatable<TResult>
        {
            return IsSatisfied(validator, p =>
            {
                if (p == null) return true;
                return p.Equals(other);
            }, string.Format(Resources.TheFieldXMustBeEqualsY, "{0}", other));
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
            return IsSatisfied(validator, p => spec.IsSatisfiedBy(p), "Spec Failure.");
        }
    }
}
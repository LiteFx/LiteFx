using System;
using System.Linq.Expressions;
using LiteFx.Specification;

namespace LiteFx.Validation.PtBr
{
    public static class ValidationHelperPtBr
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
            return ValidationHelper.IsNotNaN(validator);
        }

        #region String
        public static Validator<T, string> SejaNuloOuVazio<T>(this Validator<T, string> validator)
        {
            return ValidationHelper.IsNullOrEmpty(validator);
        }

        public static Validator<T, string> NaoSejaNuloOuVazio<T>(this Validator<T, string> validator)
        {
            return ValidationHelper.IsNotNullOrEmpty(validator);
        }

        public static Validator<T, string> SejaVazio<T>(this Validator<T, string> validator)
        {
            return ValidationHelper.IsEmpty(validator);
        }

        public static Validator<T, string> NaoSejaVazio<T>(this Validator<T, string> validator)
        {
            return ValidationHelper.IsNotEmpty(validator);
        }

        public static Validator<T, string> TamanhoMaximo<T>(this Validator<T, string> validator, int maxLength)
        {
            return ValidationHelper.MaxLength(validator, maxLength);
        }

        public static Validator<T, string> TamanhoMinimo<T>(this Validator<T, string> validator, int minLength)
        {
            return ValidationHelper.MinLength(validator, minLength);
        }

        public static Validator<T, string> TamanhoEntre<T>(this Validator<T, string> validator, int minLength, int maxLength)
        {
            return ValidationHelper.RangeLength(validator, minLength, maxLength);
        }

        public static Validator<T, string> Tamanho<T>(this Validator<T, string> validator, int length)
        {
            return ValidationHelper.Length(validator, length);
        }
        #endregion
        
        public static Validator<T, TResult> Maximo<T, TResult>(this Validator<T, TResult> validator, TResult max)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.Max(validator, max);
        }

        public static Validator<T, TResult> Minimo<T, TResult>(this Validator<T, TResult> validator, TResult min)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.Min(validator, min);
        }

        public static Validator<T, TResult> Entre<T, TResult>(this Validator<T, TResult> validator, TResult min, TResult max)
            where TResult : IComparable<TResult>
        {
            return ValidationHelper.Range(validator, min, max);
        }

        public static Validator<T, TResult> SaoIguais<T, TResult>(this Validator<T, TResult> validator, TResult other)
            where TResult : IEquatable<TResult>
        {
            return ValidationHelper.AreEquals(validator, other);
        }

        public static Validator<T, TResult> NaoSaoIguais<T, TResult>(this Validator<T, TResult> validator, TResult other)
            where TResult : IEquatable<TResult>
        {
            return ValidationHelper.NotEquals(validator, other);
        }

        public static Validator<T, T> SejaSatisfeitoPor<T>(this Validator<T, T> validator, ISpecification<T> spec)
        {
            return ValidationHelper.IsSatisfiedBy(validator, spec);
        }
    }
}

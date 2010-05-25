using System;
using System.Linq.Expressions;

namespace LiteFx.Bases.Validation.PtBr
{
    public static class ValidationHelperPtBr
    {
        public static Validator<T, TResult> Que<T, TResult>(this Assert<T> assert, Expression<Func<T, TResult>> accessor)
        {
            return ValidationHelper.That(assert, accessor);
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

        public static Validator<T, string> NaoSejaNuloOuVazio<T>(this Validator<T, string> validator)
        {
            return ValidationHelper.IsNotNullOrEmpty(validator);
        }

        public static Validator<T, string> TamanhoMaximo<T>(this Validator<T, string> validator, int maxLength)
        {
            return ValidationHelper.Length(validator, maxLength);
        }
    }
}

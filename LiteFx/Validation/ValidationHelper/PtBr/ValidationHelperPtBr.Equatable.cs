using System;

namespace LiteFx.Validation.PtBr
{
    public static partial class ValidationHelperPtBr
    {
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
    }
}

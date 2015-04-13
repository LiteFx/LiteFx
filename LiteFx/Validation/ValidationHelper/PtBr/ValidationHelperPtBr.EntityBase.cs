using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Validation.PtBr
{
    public static partial class ValidationHelperPtBr
    {
        public static Validator<T, EntityBase<TId>> Requerido<T, TId>(this Validator<T, EntityBase<TId>> validator)
            where TId : IEquatable<TId>
        {
            return ValidationHelper.Required(validator);
        }

        public static Validator<T, EntityBase<TId>> EhRequerido<T, TId>(this Validator<T, EntityBase<TId>> validator)
            where TId : IEquatable<TId>
        {
            return ValidationHelper.IsRequired(validator);
        }

        public static Validator<T, EntityBase<TId>> NaoPodeSerVazio<T, TId>(this Validator<T, EntityBase<TId>> validator)
            where TId : IEquatable<TId>
        {
            return ValidationHelper.CanNotBeEmpty(validator);
        }
    }
}

namespace LiteFx.Validation.PtBr
{
    public static partial class ValidationHelperPtBr
    {
        public static Validator<T, string> SejaNuloOuVazio<T>(this Validator<T, string> validator)
        {
            return ValidationHelper.IsNullOrEmpty(validator);
        }

        public static Validator<T, string> EhObrigatorio<T>(this Validator<T, string> validator)
        {
            return ValidationHelper.IsNotNullOrEmpty(validator);
        }

        public static Validator<T, string> Obrigatorio<T>(this Validator<T, string> validator)
        {
            return ValidationHelper.IsNotNullOrEmpty(validator);
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
    }
}

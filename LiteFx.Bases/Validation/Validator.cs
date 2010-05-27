namespace LiteFx.Bases.Validation
{
    public class Validator<T, TResult>
    {
        public Assertion<T, TResult> Assertion { get; set; }

        public Validator(Assertion<T, TResult> assertion)
        {
            Assertion = assertion;
        }
    }
}

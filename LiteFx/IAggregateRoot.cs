namespace LiteFx
{
    public interface IAggregateRoot<TId>
    {
        TId GlobalIdentity { get; }
    }
}

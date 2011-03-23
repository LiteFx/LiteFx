
namespace LiteFx.Bases.DomainEvents
{
    /// <summary>
    /// Domain event interface.
    /// </summary>
    public interface IDomainEvent { }

    /// <summary>
    /// Domain event with subject interface.
    /// </summary>
    /// <typeparam name="T">Subject Type.</typeparam>
    public interface IDomainEvent<T> : IDomainEvent 
    {
        /// <summary>
        /// Domain event subject.
        /// </summary>
        T Subject { get; }
    }
}

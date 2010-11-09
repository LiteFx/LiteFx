
namespace LiteFx.Bases.DomainEvents
{

    public interface IDomainEventHandler
    {
    }

    public interface IDomainEventHandler<TDomainEvent> : IDomainEventHandler
        where TDomainEvent : IDomainEvent
    {
        void HandleDomainEvent(TDomainEvent domainEvent);
    }
}
namespace LiteFx.DomainEvents
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDomainEventHandler
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public interface IDomainEventHandler<TDomainEvent> : IDomainEventHandler
        where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainEvent"></param>
        void HandleDomainEvent(TDomainEvent domainEvent);
    }
}
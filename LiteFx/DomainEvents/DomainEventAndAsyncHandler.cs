namespace LiteFx.DomainEvents
{
    public abstract class DomainEventAndAsyncHandler
    {
        public abstract void Execute();
    }

    public class DomainEventAndAsyncHandler<T> : DomainEventAndAsyncHandler
        where T : IDomainEvent
    {
        private T _domainEvent;
        private IAsyncDomainEventHandler<T> _asyncHandler;

        public DomainEventAndAsyncHandler(T domainEvent, IAsyncDomainEventHandler<T> asyncHandler)
        {
            _domainEvent = domainEvent;
            _asyncHandler = asyncHandler;
        }

        public override void Execute()
        {
            _asyncHandler.HandleDomainEvent(_domainEvent);
        }
    }
}
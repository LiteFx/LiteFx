using System;
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

        /// <summary>
        /// Execute the domain event handler.
        /// </summary>
        public override void Execute()
        {
            try
            {
                _asyncHandler.HandleDomainEvent(_domainEvent);
                DomainEvents.OnAsyncDomainEventHandlerExecuted(_domainEvent, _asyncHandler);
            }
            catch (Exception ex) 
            {
                DomainEvents.OnAsyncDomainEventHandlerError(ex, _domainEvent, _asyncHandler);
            }
        }
    }
}
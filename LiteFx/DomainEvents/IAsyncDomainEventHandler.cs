using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteFx.DomainEvents
{
    public interface IAsyncDomainEventHandler
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public interface IAsyncDomainEventHandler<TDomainEvent> : IAsyncDomainEventHandler
        where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainEvent"></param>
        void HandleDomainEvent(TDomainEvent domainEvent);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public interface IAsyncDomainEventHandlerInTransactionScope<TDomainEvent> : IAsyncDomainEventHandler
        where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domainEvent"></param>
        void HandleDomainEvent(TDomainEvent domainEvent);
    }
}

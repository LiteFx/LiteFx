using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases.DomainEvents
{
    /// <summary>
    /// This class is responsible 
    /// </summary>
    public static class DomainEventsHandler
    {
        private static IList<Delegate> callbacks;
        private static IList<IDomainEventHandler> domainEventHandlers;

        public static void RegisterCallback<T>(Action<T> callback) where T : IDomainEvent
        {
            if (callbacks == null)
                callbacks = new List<Delegate>();

            callbacks.Add(callback);
        }

        public static void RegisterDomainEventHandler<T>(IDomainEventHandler<T> domainEventHandler) where T : IDomainEvent
        {
            if (domainEventHandlers == null)
                domainEventHandlers = new List<IDomainEventHandler>();

            domainEventHandlers.Add(domainEventHandler);
        }

        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            if(callbacks != null)
                foreach(var callback in callbacks.OfType<Action<T>>())
                    callback(domainEvent);

            if (domainEventHandlers != null)
                foreach (var domainEventHandler in domainEventHandlers.OfType<IDomainEventHandler<T>>())
                    domainEventHandler.HandleDomainEvent(domainEvent);
        }
    }
}

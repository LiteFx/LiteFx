using System;
using System.Collections.Generic;
using System.Linq;

namespace LiteFx.Bases.DomainEvents
{
    /// <summary>
    /// This class is responsible for Register the event handlers and call these handlers when a event is raised.
    /// </summary>
    public static class DomainEvents
    {
        [ThreadStatic]
        private static IList<Delegate> callbacks;

        [ThreadStatic]
        private static IList<IDomainEventHandler> domainEventHandlers;

        private static IList<Delegate> Callbacks
        {
            get
            {
                return callbacks ?? (callbacks = new List<Delegate>());
            }
        }

        private static IList<IDomainEventHandler> DomainEventHandlers
        {
            get
            {
                return domainEventHandlers ?? (domainEventHandlers = new List<IDomainEventHandler>());
            }
        }

        /// <summary>
        /// Register a Action Callback that will handle a event.
        /// </summary>
        /// <typeparam name="T">Type that will be handled.</typeparam>
        /// <param name="callback">Callback method that will handle the event.</param>
        public static void RegisterCallback<T>(Action<T> callback) where T : IDomainEvent
        {
            Callbacks.Add(callback);
        }

        /// <summary>
        /// Register a domain event handler.
        /// </summary>
        /// <typeparam name="T">Type that will be handled.</typeparam>
        /// <param name="domainEventHandler">The object that will handle the event.</param>
        public static void RegisterDomainEventHandler<T>(IDomainEventHandler<T> domainEventHandler) where T : IDomainEvent
        {
            DomainEventHandlers.Add(domainEventHandler);
        }

        /// <summary>
        /// Raises a domain event.
        /// It forces all registered callbacks and event handlers to be triggered.
        /// </summary>
        /// <typeparam name="T">Type that will be handled.</typeparam>
        /// <param name="domainEvent">The event that will be raised.</param>
        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            foreach (var callback in Callbacks.OfType<Action<T>>())
                callback(domainEvent);

            foreach (var domainEventHandler in DomainEventHandlers.OfType<IDomainEventHandler<T>>())
                domainEventHandler.HandleDomainEvent(domainEvent);
        }
    }
}

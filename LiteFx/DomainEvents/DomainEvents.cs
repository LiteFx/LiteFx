using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace LiteFx.DomainEvents
{
    /// <summary>
    /// IAsyncDomainEventHandler executed event handler.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    /// <param name="asyncDomainEventHandler">The IAsyncDomainEventHandler executed.</param>
    public delegate void AsyncDomainEventHandlerExecutedEventHandler(IDomainEvent domainEvent, IAsyncDomainEventHandler asyncDomainEventHandler);

    /// <summary>
    /// IAsyncDomainEventHandler executed event handler.
    /// </summary>
    /// <param name="domainEvent">Domain event.</param>
    /// <param name="asyncDomainEventHandler">The IAsyncDomainEventHandler executed.</param>
    public delegate void AsyncDomainEventHandlerErrorEventHandler(Exception exception, IDomainEvent domainEvent, IAsyncDomainEventHandler asyncDomainEventHandler);

    /// <summary>
    /// This class is responsible for Register the event handlers and call these handlers when a event is raised.
    /// </summary>
    public static class DomainEvents
    {
        /// <summary>
        /// Raised when a async domain event handler is executed.
        /// </summary>
        public static event AsyncDomainEventHandlerExecutedEventHandler AsyncDomainEventHandlerExecuted;

        /// <summary>
        /// Raised when a async domain event handler get an error.
        /// </summary>
        public static event AsyncDomainEventHandlerErrorEventHandler AsyncDomainEventHandlerError;

        private static IList<Delegate> callbacks;
        private static IList<Delegate> Callbacks
        {
            get
            {
                return callbacks ?? (callbacks = new List<Delegate>());
            }
        }

        private static IList<IDomainEventHandler> domainEventHandlers;
        private static IList<IDomainEventHandler> DomainEventHandlers
        {
            get
            {
                return domainEventHandlers ?? (domainEventHandlers = new List<IDomainEventHandler>());
            }
        }

        private static IList<IAsyncDomainEventHandler> asyncDomainEventHandlers;
        private static IList<IAsyncDomainEventHandler> AsyncDomainEventHandlers
        {
            get
            {
                return asyncDomainEventHandlers ?? (asyncDomainEventHandlers = new List<IAsyncDomainEventHandler>());
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
        /// Register an async domain event handler.
        /// </summary>
        /// <typeparam name="T">Type that will be handled.</typeparam>
        /// <param name="domainEventHandler">The object that will handle the event.</param>
        public static void RegisterAsyncDomainEventHandler<T>(IAsyncDomainEventHandler<T> domainEventHandler) where T : IDomainEvent
        {
            AsyncDomainEventHandlers.Add(domainEventHandler);
        }

        /// <summary>
        /// Register all classes that implements <see cref="IDomainEventHandler"/> in the assembly passed by parameter.
        /// </summary>
        /// <param name="assembly">Assembly to find classes that implements <see cref="IDomainEventHandler"/>.</param>
        public static void RegisterAllDomainEventHandlers(Assembly assembly)
        {
            var eventHandlerType = typeof(IDomainEventHandler);
            var eventHandlerTypes = assembly.GetTypes().Where(t => eventHandlerType.IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var eventHandler in eventHandlerTypes)
            {
                DomainEventHandlers.Add((IDomainEventHandler)Activator.CreateInstance(eventHandler));
            }

            eventHandlerType = typeof(IAsyncDomainEventHandler);
            eventHandlerTypes = assembly.GetTypes().Where(t => eventHandlerType.IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var eventHandler in eventHandlerTypes)
            {
                AsyncDomainEventHandlers.Add((IAsyncDomainEventHandler)Activator.CreateInstance(eventHandler));
            }
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

            if (AsyncDomainEventHandlers.OfType<IAsyncDomainEventHandler<T>>().Any())
            {
                IDomainEventStore domainEventStore = ServiceLocator.Current.GetInstance<IDomainEventStore>();
                domainEventStore.Save(mountAsyncDispatchers(domainEvent));
            }
        }

        /// <summary>
        /// Execute all async domain event handlers.
        /// </summary>
        public static void DispatchAsyncEvents()
        {
            IDomainEventStore domainEventStore = ServiceLocator.Current.GetInstance<IDomainEventStore>();

            foreach (var domainEventDispatcher in domainEventStore.GetAll())
            {
                domainEventDispatcher();
            }
        }

        /// <summary>
        /// Raises a domain event.
        /// It forces all registered async event handlers to be triggered in the <see cref="ThreadPool"/>.
        /// </summary>
        /// <typeparam name="T">Type that will be handled.</typeparam>
        /// <param name="domainEvent">The event that will be raised.</param>
        private static IEnumerable<Action> mountAsyncDispatchers<T>(T domainEvent) where T : IDomainEvent
        {
            foreach (var asyncDomainEventHandler in AsyncDomainEventHandlers.OfType<IAsyncDomainEventHandler<T>>())
            {
                yield return () => ThreadPool.QueueUserWorkItem(new WaitCallback(handleTarget), new DomainEventAndAsyncHandler<T>(domainEvent, asyncDomainEventHandler));
            }
        }

        private static void handleTarget(Object stateInfo)
        {
            DomainEventAndAsyncHandler domainEventAndAsyncHandler = (DomainEventAndAsyncHandler)stateInfo;
            domainEventAndAsyncHandler.Execute();
        }

        public static void OnAsyncDomainEventHandlerExecuted(IDomainEvent domainEvent, IAsyncDomainEventHandler asyncHandler)
        {
            AsyncDomainEventHandlerExecuted(domainEvent, asyncHandler);
        }

        public static void OnAsyncDomainEventHandlerError(Exception exception,IDomainEvent domainEvent, IAsyncDomainEventHandler asyncHandler)
        {
            AsyncDomainEventHandlerError(exception, domainEvent, asyncHandler);
        }
    }
}
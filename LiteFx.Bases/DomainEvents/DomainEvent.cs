using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.Bases.DomainEvents
{
    public static class DomainEvent
    {
        private static IList<Delegate> callbacks;

        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (callbacks == null)
                callbacks = new List<Delegate>();

            callbacks.Add(callback);
        }

        public static void Raise<T>(T domainEvent) where T : IDomainEvent
        {
            if(callbacks != null && callbacks.Count > 0)
                foreach(var callback in callbacks.OfType<Action<T>>())
                    callback(domainEvent);
        }
    }
}

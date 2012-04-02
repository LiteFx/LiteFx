using Microsoft.Practices.ServiceLocation;
using Raven.Client;
using Raven.Client.Document;

namespace LiteFx.Raven
{
    public abstract class SessionFactoryManager
    {
        private static SessionFactoryManager current;
        public static SessionFactoryManager Current
        {
            get
            {
                return current ?? (current = ServiceLocator.Current.GetInstance<SessionFactoryManager>());
            }
        }

        protected abstract DocumentStore DocumentStore { get; }
        protected IDocumentSession Session { get; set; }

        public IDocumentSession GetCurrentSession() 
        {
            if (Session == null) 
            {
                Session = DocumentStore.OpenSession();
            }

            return Session;
        }

        public virtual void CommitTransaction()
        {
            if (Session != null && Session.Advanced.HasChanges)
            {
                Session.SaveChanges();
            }
        }
    }
}

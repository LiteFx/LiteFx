using Microsoft.Practices.ServiceLocation;
using Raven.Client;
using Raven.Client.Document;

namespace LiteFx.Raven
{
    public class SessionFactoryManager
    {
        protected static SessionFactoryManager current;
        public static SessionFactoryManager Current
        {
            get
            {
                //return current ?? (current = ServiceLocator.Current.GetInstance<SessionFactoryManager>());
                return current ?? (current = new SessionFactoryManager());
            }
        }

        protected static DocumentStore documentStore;
        protected virtual DocumentStore DocumentStore
        {
            get
            {
                if (documentStore == null)
                {
                    documentStore = new DocumentStore();
                    documentStore.ConnectionStringName = "RavenDb";
                    documentStore.Initialize();
                }

                return documentStore;
            }
        }


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
                Session.Dispose();
            }
        }

        public virtual void RollbackTransaction()
        {
            DisposeSession();
        }

        public virtual void DisposeSession()
        {
            if (Session != null && Session.Advanced.HasChanges)
            {
                Session.Dispose();
            }
        }
    }
}

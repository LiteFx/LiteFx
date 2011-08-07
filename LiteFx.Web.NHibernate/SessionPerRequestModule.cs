using System;
using System.Web;
using LiteFx.Context.NHibernate;

namespace LiteFx.Web.NHibernate
{
    public class SessionPerRequestModule : IHttpModule
    {
        public void Dispose()
        { }

        public void Init(HttpApplication context)
        {
            context.EndRequest += new EventHandler(context_EndRequest);
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;

            if (application.Context.Error == null)
            {
                try
                {
                    SessionFactoryManager.Current.CommitTransaction();
                }
                catch(Exception ex)
                {
                    SessionFactoryManager.Current.RollbackTransaction();
                    throw;
                }
                finally
                {
                    SessionFactoryManager.Current.DisposeSession();
                }
            }
            else
            {
                SessionFactoryManager.Current.RollbackTransaction();
                SessionFactoryManager.Current.DisposeSession();
            }
        }
    }
}

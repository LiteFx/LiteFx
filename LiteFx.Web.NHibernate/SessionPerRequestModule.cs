using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using LiteFx.Context.NHibernate;
using NHibernate.Context;

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
                SessionFactoryManager.CommitTransaction();

            SessionFactoryManager.DisposeSession();
            CurrentSessionContext.Unbind(SessionFactoryManager.SessionFactory);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using LiteFx.Bases.Context.NHibernate;
using NHibernate.Context;

namespace LiteFx.Web.Mvc.NHibernate
{
    public class SessionPerRequestActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
                SessionFactoryManager.CommitTransaction();

            SessionFactoryManager.DisposeSession();
            CurrentSessionContext.Unbind(SessionFactoryManager.SessionFactory);
        }
    }
}

using System.Web.Mvc;
using LiteFx.Context.NHibernate;

namespace LiteFx.Web.Mvc.NHibernate
{
    public class SessionPerRequestActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
                SessionFactoryManager.Current.CommitTransaction();

            SessionFactoryManager.Current.DisposeSession();
        }
    }
}

using System.Web.Mvc;
using LiteFx.Context.NHibernate;

namespace LiteFx.Web.Mvc.NHibernate
{
    public class SessionPerRequestActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod == "GET")
                SessionFactoryManager.Current.ReadOnly = true;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Exception == null && filterContext.Controller.ViewData.ModelState.IsValid)
            {
                try
                {
                    SessionFactoryManager.Current.CommitTransaction();
                }
                catch
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

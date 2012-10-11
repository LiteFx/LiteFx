using System.Web.Mvc;
using LiteFx.Context.NHibernate;

namespace LiteFx.Web.Mvc.NHibernate
{
    public class SessionPerRequestActionFilter : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
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

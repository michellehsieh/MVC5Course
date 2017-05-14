using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class LocalOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //AOP 面向側面的設計模式
            //只要不是Local電腦，遠端連入，則會直接跳回首頁
            if (!filterContext.RequestContext.HttpContext.Request.IsLocal) {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}
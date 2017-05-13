using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5Course
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //路由比對：執行時排除比對aspx
            routes.IgnoreRoute("{anything}/{resource}.aspx/{*pathInfo}");

            //新增一組路由(較不建議使用)
            //routes.MapRoute(
            //    name: "Next",
            //    url: "{controller}/{*params}",
            //    defaults: new { controller = "Home", action = "Default", param = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                // 傳入多個路由id
                //url: "{controller}/{action}/{*id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

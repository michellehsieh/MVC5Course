using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    //抽象類別不可被引用
    public abstract class BaseController: Controller
    {
       protected FabricsEntities db = new FabricsEntities();

        [LocalOnly]
        public ActionResult Debug()
        {
            return Content("Hello");
        }

        //HandleUnknowAction
        //protected override void HandleUnknownAction(string actionName)
        //{
        //    //當頁面不存在或找不到頁面時，強迫回到首頁
        //    this.RedirectToAction("Index", "Home").ExecuteResult(this.ControllerContext);
        //}
    }
}
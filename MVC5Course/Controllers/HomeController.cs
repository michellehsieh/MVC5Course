using System;
using System.Collections.Generic;
//using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //return new ViewResult()
            //{
            //    ViewName = "About"
            //};
            // return View("About");

            return View();
        }

        public ActionResult Unknow()
        {
            return View();
        }

        [SharedViewBag(MyProperty = "")]
        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        //執行時不套用Layout
        public ActionResult PartialAbout()
        {
            ViewBag.Message = "Your application description page.";

            if (Request.IsAjaxRequest())
            {
                return PartialView("About");
            }
            else {
                return View("About");
            }

            // return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        //直接執行特定View，並取得執行結果的方法
        //public string RenderRazorViewToString(string viewName, object model)
        //{
        //    ViewData.Model = model;
        //    using (var sw = new StringWriter())
        //    {
        //        var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
        //                                                                 viewName);
        //        var viewContext = new ViewContext(ControllerContext, viewResult.View,
        //                                     ViewData, TempData, sw);
        //        viewResult.View.Render(viewContext, sw);
        //        viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
        //        return sw.GetStringBuilder().ToString();
        //    }
        //}


        
        public ActionResult SomeAction()
        {
            // Content此建議功能由View來做
            //Response.Write("<script>alert('建立成功！'); location.href='/';</script>");
            //return "<script>alert('建立成功！'); location.href='/';</script>";
            //return Content("<script>alert('建立成功！'); location.href='/';</script>");
            
            return PartialView("SuccessRedirect", "/");
        }


        public ActionResult GetFile()
        {
            //檔案格式ContentType查詢網址：http://www.freeformatter.com/mime-types-list.html

            //網頁瀏覽
            return File(Server.MapPath("~/Content/500-527.jpg"), "image/jpeg");

            //強制下載，並存為NewName.jpg
            //return File(Server.MapPath("~/Content/500-527.jpg"), "image/jpeg", "NewName.jpg");
        }

        //Json格式 
        public ActionResult GetJson()
        {
            db.Configuration.LazyLoadingEnabled = false;
            return Json(db.Product.Take(5), JsonRequestBehavior.AllowGet);
        }

        //RedirectToAction  暫時轉址
        //RedirectToActionPermanent  永久轉址
        //只對搜尋引擎有差

    }
}
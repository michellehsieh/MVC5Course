using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index(bool Active = true)
        {
            //var repo = new ProductRepository();
            //repo.UnitOfWork = new EFUnitOfWork();
            var data = repo.getProduct列表頁所有資料(Active, showAll: false);

            //強型別的傳值方式
            //ViewData.Model = data;
            //return View();

            //弱型別的傳值方式(傳入view的值為object)
            //ViewData["ppp"] = data;

            //弱型別的傳值方式(同ViewData)
            //ViewBag.qqq = data;

            //弱型別的傳值方式(暫存 - 寫入的資料被讀過一次就會被刪除)
            //TempData類似Session，但Session需人工刪除，TempData會自動刪除
            //TempData未被讀過，則會一直存在
            //TempData["xxx"] = data;

            return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.get單筆資料ByProductId(id.Value);
            //Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                //db.Product.Add(product);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Product product = db.Product.Find(id);
            Product product = repo.get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FormCollection form)
        {
            // Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product

            // Model Binding導致資料意外update錯誤(當傳入欄位值更新時，原值會被以預設值update)
            var product = repo.get單筆資料ByProductId(id);

            // TryUpdateModel其他用法：
            // 1. TryUpdateModel(product, 
            //                   new string[] {"ProductId", "ProductName", "Price", "Active", "Stock"})
            // 2. TryUpdateModel(searchCondition, "searchCondition") ，prefix需自訂名稱

            //只有傳入的欄位會被執行更新，不想被使用者更新的欄位，則不做更新
            if (TryUpdateModel<Product>(product,
                new string[] {"ProductId", "ProductName", "Price", "Active", "Stock"})) {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();

                //repo.Update(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Product product = db.Product.Find(id);
            Product product = repo.get單筆資料ByProductId(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Product product = db.Product.Find(id);
            Product product = repo.get單筆資料ByProductId(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            
            repo.Delete(product);

            // Product與OrderLines共用同一UnitOfWork，任一Commit，將一併執行
            //var repoOrderLines = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork);
            //foreach (var item in product.OrderLine) {
            //  repoOrderLine.Delete(product);
            //}

            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }


        public ActionResult ListProducts(ProductListSearchVM searchCondition)
        {
            var data = repo.getProduct列表頁所有資料(true);

            //表單送出只要有 Model Binding 就會有ModelState
            
            if (!String.IsNullOrEmpty(searchCondition.q)) {
                data = data.Where(p => p.ProductName.Contains(searchCondition.q));
            }                    
            

            if (searchCondition.from != 0) {
                data = data.Where(p => p.Stock > searchCondition.from);
            }

            if (searchCondition.to != 0)
            {
                data = data.Where(p => p.Stock < searchCondition.to);
            }

            ViewData.Model = data
                  .Select(p => new ProductLiteVM()
                  {
                      ProductId = p.ProductId,
                      ProductName = p.ProductName,
                      Price = p.Price,
                      Stock = p.Stock
                  });
            return View();
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductLiteVM data)
        {
            if (ModelState.IsValid)
            {
                // TODO: 儲存資料進資料庫
                TempData["CreateProduct_Result"] = "商品新增成功";

                return RedirectToAction("ListProducts");
            }
            // 驗證失敗，繼續顯示原本的表單
            return View();
        }
    }
}

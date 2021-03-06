using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Course.Models
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        public override IQueryable<Product> All() {
            return base.All().Where(p => !p.Is刪除);
        }

        public IQueryable<Product> All(bool showAll)
        {
            if (showAll)
            {
                return base.All();
            }
            else {
                return this.All();
            }
        }

        public Product get單筆資料ByProductId(int id) {            
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public override void Delete(Product entity)
        {
            //刪除時，強迫關閉驗證
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;

            entity.Is刪除 = true;
        }

        public IQueryable<Product> getProduct列表頁所有資料(bool Active, bool? showAll = false)
        {
            IQueryable<Product> all = this.All();
            if (showAll.HasValue && showAll.Value)
            {
                all = base.All();
            }
            return all
                .Where(p => p.Active.HasValue && p.Active.Value == Active)
                .OrderByDescending(p => p.ProductId).Take(10);
        }

        public void Update(Product product) {
            this.UnitOfWork.Context.Entry(product).State = EntityState.Modified;
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}
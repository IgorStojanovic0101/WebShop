using WebShop.Data;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace WebShop.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
      /*  public IEnumerable<Product> GetAllwithProperties()
        {
            IQueryable<Product> query = dbSet;
            query = query.Include(nameof(Category));
            query = query.Include(nameof(CoverType));
            return query.ToList();
        }
		public Product GetFirstOrDefaultWithProperties(Expression<Func<Product, bool>> filter)
        {
			IQueryable<Product> query = dbSet;
			query = query.Where(filter);
			query = query.Include(nameof(Category));
			query = query.Include(nameof(CoverType));
			return query.FirstOrDefault();
		}*/

		public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(x => x.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title= obj.Title;
                objFromDb.Description= obj.Description;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price100= obj.Price100;
                objFromDb.Price50= obj.Price50;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price = obj.Price;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Author = obj.Author;
                objFromDb.CoverTypeId= obj.CoverTypeId;
                if(obj.ImageUrl !=null)
                {
                    objFromDb.ImageUrl= obj.ImageUrl;
                }
            }
         
        }

    }
}

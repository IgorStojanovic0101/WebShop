using WebShop.Data;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Model.ViewModel;
using System.Linq.Expressions;
using WebShop.Model.Models;

namespace WebShop.DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCart
    {
        private ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }


        public int DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }

        public int IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }
		/*public IEnumerable<ShoppingCart> GetAllwithProperties(Expression<Func<ShoppingCart, bool>>? filter = null)
        {
			IQueryable<ShoppingCart> query = dbSet;
            query = query.Where(filter);
			query = query.Include(nameof(Product));
			query = query.Include(nameof(ApplicationUser));
			return query.ToList();
		}*/


	}
}

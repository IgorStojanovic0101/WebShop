using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Model.ViewModel;
using System.Linq.Expressions;

namespace WebShop.DataAccess.Repository.IRepository
{
    public interface IShoppingCart : IRepository<ShoppingCart>
    {
        int IncrementCount(ShoppingCart shoppingCart, int count);
        int DecrementCount(ShoppingCart shoppingCart, int count);

		//IEnumerable<ShoppingCart> GetAllwithProperties(Expression<Func<ShoppingCart, bool>>? filter = null);

	}
}

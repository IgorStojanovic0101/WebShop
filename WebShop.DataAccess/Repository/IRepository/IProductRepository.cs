using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace WebShop.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);

       // IEnumerable<Product> GetAllwithProperties();

       // Product GetFirstOrDefaultWithProperties(Expression<Func<Product, bool>> filter);
	}
}

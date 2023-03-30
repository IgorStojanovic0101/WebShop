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
using WebShop.Model.Models;

namespace WebShop.DataAccess.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetailModel>, IOrderDetailsRepository
    {
        private ApplicationDbContext _db;

        public OrderDetailsRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

    /*    public IEnumerable<OrderDetail> GetAllWithProperties(Expression<Func<OrderDetail, bool>>? filter = null)
        {
            IQueryable<OrderDetail> query = dbSet;
            query = query.Where(filter);
            query = query.Include(nameof(Product));
            return query.AsEnumerable();
        }*/

        public void Update(OrderDetailModel obj)
        {
           _db.Update(obj);
        }

    }
}

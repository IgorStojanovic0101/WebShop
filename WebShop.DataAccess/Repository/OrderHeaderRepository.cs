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
    public class OrderHeaderRepository : Repository<OrderHeaderModel>, IOrderHeaderRepository
	{
        private ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

	/*	public IEnumerable<OrderHeader> GetAllWithProperties(Expression<Func<OrderHeader, bool>>? filter = null)
		{
			IQueryable<OrderHeader> query = dbSet;
			query = query.Where(filter ?? (x => true));
			query = query.Include(nameof(ApplicationUser));
			return query.AsEnumerable();
		}

		public OrderHeader GetFirstOrDefaultWithProperies(Expression<Func<OrderHeader, bool>> filter)
		{
			IQueryable<OrderHeader> query = dbSet;
			query = query.Where(filter ?? (x => true));
			query = query.Include(nameof(ApplicationUser));
			return query.FirstOrDefault();
		}*/

		public void Update(OrderHeaderModel obj)
        {
           _db.Update(obj);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
			if (orderFromDb != null)
			{
				orderFromDb.OrderStatus = orderStatus;
				if (paymentStatus != null)
				{
					orderFromDb.PaymentStatus = paymentStatus;
				}
			}
		}
		public void UpdateStripePaymentID(int id, string sessionId, string paymentItentId)
		{
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
			orderFromDb.PaymentDate = DateTime.Now;
			orderFromDb.SessionId = sessionId;
			orderFromDb.PaymentIntentId = paymentItentId;
		}
	}
}

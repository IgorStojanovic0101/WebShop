using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace WebShop.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeaderModel>
    {
        void Update(OrderHeaderModel obj);

        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);

		void UpdateStripePaymentID(int id, string sessionId, string paymentItentId);

	/*	IEnumerable<OrderHeader> GetAllWithProperties(Expression<Func<OrderHeader, bool>>? filter = null);


		OrderHeader GetFirstOrDefaultWithProperies(Expression<Func<OrderHeader, bool>> filter);*/

	}
}

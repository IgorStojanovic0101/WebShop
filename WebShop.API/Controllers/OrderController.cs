using Microsoft.AspNetCore.Mvc;
using WebShop.API.Business;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

namespace WebShop.API.Controllers
{
    public class OrderController : Base<Order>
    {


        public OrderController(IUnitOfWork unitOfWork) : base(new List<object>() { unitOfWork })
        { }

        [HttpGet]
        public IEnumerable<OrderHeaderModel> GetOrders() => Call(x => x.GetOrders());


    }
}

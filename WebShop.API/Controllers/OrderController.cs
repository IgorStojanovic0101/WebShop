using Microsoft.AspNetCore.Mvc;
using WebShop.API.Business;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Model.ViewModel;
using WebShop.Models;

namespace WebShop.API.Controllers
{
    public class OrderController : Base<Order>
    {


        public OrderController(IUnitOfWork unitOfWork) : base(new List<object>() { unitOfWork })
        { }

        [HttpGet]
        public IEnumerable<OrderHeaderModel> GetOrders() => Call(x => x.GetOrders());
      

        [HttpPost]
        public IEnumerable<OrderDetailModel> GetOrderDetailsListById([FromBody] int id) => Call(x => x.GetOrderDetailsListById(id));

        [HttpPost]
        public OrderHeaderModel CreateOrderHeader([FromBody] OrderHeaderModel model) => Call(x => x.CreateOrderHeader(model));

        [HttpPost]
        public OrderHeaderModel GetOrderHeaderById([FromBody] int id) => Call(x => x.GetOrderHeaderById(id));

        [HttpPost]
        public OrderHeaderModel UpdateOrderDetails([FromBody] OrderVM obj) => Call(x => x.UpdateOrderDetails(obj));

        [HttpPost]
        public ReturnModel UpdateStatus([FromBody] UpdateStatusModel obj) => Call(x => x.UpdateStatus(obj));


        [HttpPost]
        public ReturnModel ShipOrder([FromBody] OrderVM model) => Call(x => x.ShipOrder(model));

        [HttpPost]
        public ReturnModel UpdateStripePaymentID([FromBody] UpdateStripePaymentModel model) => Call(x => x.UpdateStripePaymentID(model));

        
    }
}

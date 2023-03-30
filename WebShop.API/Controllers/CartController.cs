using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebShop.API.Business;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Model.ViewModel;
using WebShop.Models;

namespace WebShop.API.Controllers
{
    public class CartController : Base<Cart>
    {

        public CartController(IUnitOfWork unitOfWork) : base(new List<object>() { unitOfWork })
        { }

     

        [HttpPost]
        public IEnumerable<ShoppingCart> GetCartListByApplicationUserId([FromBody] string ApplicationUserId) => Call(x => x.GetCartListByApplicationUserId(ApplicationUserId));

        [HttpPost]
        public ReturnModel Plus([FromBody] int cartId) => Call(x => x.Plus(cartId));

        [HttpPost]
        public ReturnModel Minus([FromBody] int cartId) => Call(x => x.Minus(cartId));
      
        [HttpPost]
        public ReturnModel AddOrderDetails([FromBody] OrderDetailsAddModel model) => Call(x => x.AddOrderDetails(model));

        [HttpPost]
        public ReturnModel RemoveCarts([FromBody] string ApplicationUserId) => Call(x => x.RemoveCarts(ApplicationUserId));

        [HttpPost]
        public ShoppingCart GetShoppingCartBySearchModel([FromBody] ShoppingCartSearchModel model) => Call(x => x.GetShoppingCartBySearchModel(model));

        [HttpPost]
        public ReturnModel AddShoppingCart([FromBody] ShoppingCart model) => Call(x => x.AddShoppingCart(model));

        [HttpPost]
        public ReturnModel IncrementCount([FromBody] ShoppingCartSearchModel model) => Call(x => x.IncrementCount(model));
    }
}

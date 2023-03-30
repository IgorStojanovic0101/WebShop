using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShop.DataAccess.Repository;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Model.ViewModel;
using WebShop.Models;


namespace WebShop.API.Business
{
    public class Cart
    {

        protected readonly IUnitOfWork _unitOfWork;

        public Cart() { }


        public Cart(List<object> objs)
        {
            _unitOfWork = objs[0] as IUnitOfWork;
        }

        public ReturnModel RemoveCarts(string ApplicationUserId)
        {
            var returnModel = new ReturnModel();
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCarts.GetAll(u => u.ApplicationUserId == ApplicationUserId).ToList();
           
            _unitOfWork.ShoppingCarts.RemoveRange(shoppingCarts);
            _unitOfWork.Save();

            return returnModel;
        }
        public ReturnModel AddOrderDetails(OrderDetailsAddModel model)
        {
            var returnModel = new ReturnModel();
            foreach (var cart in model.CartList)
            {
                OrderDetailModel orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = model.OrderId,
                    Price = cart.Price,
                    Count = cart.Count
                };
                _unitOfWork.OrderDetails.Add(orderDetail);

            }
            _unitOfWork.Save();

            return returnModel;
        }

        public IEnumerable<ShoppingCart> GetCartListByApplicationUserId(string ApplicationUserId) => _unitOfWork.ShoppingCarts.GetAll(u => u.ApplicationUserId == ApplicationUserId);


        public ReturnModel Plus(int cartId)
        {
            var returnModel = new ReturnModel();
            var cart = _unitOfWork.ShoppingCarts.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.ShoppingCarts.IncrementCount(cart, 1);
            _unitOfWork.Save();

            return returnModel;
        }
        public ReturnModel Minus(int cartId)
        {
            var returnModel = new ReturnModel();
            var cart = _unitOfWork.ShoppingCarts.GetFirstOrDefault(u => u.Id == cartId);
            if (cart.Count <= 1)
            {
                _unitOfWork.ShoppingCarts.Remove(cart);
                var count = _unitOfWork.ShoppingCarts.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count - 1;
                //	HttpContext.Session.SetInt32(SD.SessionCart, count);
            }
            else
            {
                _unitOfWork.ShoppingCarts.DecrementCount(cart, 1);
            }
            _unitOfWork.Save();

            return returnModel;
        }

        public ShoppingCart GetShoppingCartBySearchModel(ShoppingCartSearchModel model) => _unitOfWork.ShoppingCarts.GetFirstOrDefault(
                u => u.ApplicationUserId == model.ApplicationUserId && u.ProductId == model.ProductId);


        public ReturnModel AddShoppingCart(ShoppingCart model)
        {
            var returnModel = new ReturnModel();
            _unitOfWork.ShoppingCarts.Add(model);

            _unitOfWork.Save();

            return returnModel;
        }
        public ReturnModel IncrementCount(ShoppingCartSearchModel model)
        {
            var returnModel = new ReturnModel();

            var cartFromDb = GetShoppingCartBySearchModel(model);
            _unitOfWork.ShoppingCarts.IncrementCount(cartFromDb, model.IncrementCount);
            _unitOfWork.Save();

            return returnModel;
        }
    }
}

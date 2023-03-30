using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Model.ViewModel;
using WebShop.Models;
using WebShop.Utility;

namespace WebShop.Areas.Consumer.Controllers
{
    [Area("Consumer")]

    public class HomeController : Base
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductModel> productList = await wsGet<List<ProductModel>>(SystemUrls.Product.GetProducts);  //_unitOfWork.Products.GetAll();
            return View(productList);
        }
        public async Task<IActionResult> Details(int productId)
        {
            var cartObj = new ShoppingCart()
            {
				Count = 1,
				ProductId = productId,
				Product = await wsPost<ProductModel, int>(SystemUrls.Product.GetProductById, productId)
        };
            return View(cartObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public  async  Task<IActionResult> Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;
            ShoppingCart cartFromDb = await wsPost<ShoppingCart, ShoppingCartSearchModel>(SystemUrls.ShoppingCart.GetShoppingCartBySearchModel,new ShoppingCartSearchModel
            {
                ApplicationUserId = claim.Value,
                ProductId = shoppingCart.ProductId
            });

             if (cartFromDb == null)
            {
                await wsPost<ReturnModel, ShoppingCart>(SystemUrls.ShoppingCart.AddShoppingCart, shoppingCart);

             }
            else
            {
                await wsPost<ReturnModel, ShoppingCartSearchModel>(SystemUrls.ShoppingCart.IncrementCount, new ShoppingCartSearchModel
                {
                    ApplicationUserId = claim.Value,
                    ProductId = shoppingCart.ProductId,
                    IncrementCount = shoppingCart.Count

                });
           }


            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
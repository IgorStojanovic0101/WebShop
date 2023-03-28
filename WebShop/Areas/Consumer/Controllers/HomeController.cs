using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.ViewModel;
using WebShop.Models;
using WebShop.Utility;

namespace WebShop.Areas.Consumer.Controllers
{
    [Area("Consumer")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork,ILogger<HomeController> logger)
        {
            _logger = logger;
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> productList = _unitOfWork.Products.GetAll();
            return View(productList);
        }
        public IActionResult Details(int productId)
        {
            var cartObj = new ShoppingCart()
            {
				Count = 1,
				ProductId = productId,
				Product = _unitOfWork.Products.GetFirstOrDefault(x => x.Id == productId)
            };
            return View(cartObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;            
          
            ShoppingCart cartFromDb = _unitOfWork.ShoppingCarts.GetFirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);

      
            if (cartFromDb == null)
            {

                
                _unitOfWork.ShoppingCarts.Add(shoppingCart);

                _unitOfWork.Save();
             //   HttpContext.Session.SetInt32(SD.SessionCart,
                  //  _unitOfWork.ShoppingCarts.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
            }
            else
            {
                _unitOfWork.ShoppingCarts.IncrementCount(cartFromDb, shoppingCart.Count);
                _unitOfWork.Save();
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
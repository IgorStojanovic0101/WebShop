using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Model.ViewModel;
using WebShop.Models;
using WebShop.Utility;

namespace WebShop.Areas.Consumer.Controllers
{
    [Area("Consumer")]
    [Authorize]
    public class CartController : Base
    {
		[BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }
		

	
		public async  Task<IActionResult> Index()
        {
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = await wsPost<IEnumerable<ShoppingCart>, string>(SystemUrls.ShoppingCart.GetCartListByApplicationUserId, claim.Value),
				OrderHeader = new()
			};
			foreach (var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,
					cart.Product.Price50, cart.Product.Price100);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingCartVM);
        }

		public async Task<IActionResult> Plus(int cartId)
		{

			await wsPost<ReturnModel, int>(SystemUrls.ShoppingCart.Plus, cartId);

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Minus(int cartId)
		{

            await wsPost<ReturnModel, int>(SystemUrls.ShoppingCart.Minus, cartId);


            return RedirectToAction(nameof(Index));
		}


		public async Task<IActionResult> Summary()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM = new ShoppingCartVM()
			{
				ListCart = await wsPost<IEnumerable<ShoppingCart>, string>(SystemUrls.ShoppingCart.GetCartListByApplicationUserId, claim.Value), //_unitOfWork.ShoppingCarts.GetAll(u => u.ApplicationUserId == claim.Value),
            OrderHeader = new()

			};
			ShoppingCartVM.OrderHeader.ApplicationUser  = await wsPost<ApplicationUser, string>(SystemUrls.User.GetUserById, claim.Value);
			ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
			ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
			ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
			ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
			ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
			ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;



			foreach (var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,cart.Product.Price50, cart.Product.Price100);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);


			}
			return View(ShoppingCartVM);
		}
		[HttpPost]
		[ActionName("Summary")]
		[ValidateAntiForgeryToken]
		public async Task <IActionResult> SummaryPOST()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			ShoppingCartVM.ListCart = await wsPost <IEnumerable<ShoppingCart>, string>(SystemUrls.ShoppingCart.GetCartListByApplicationUserId, claim.Value); //_unitOfWork.ShoppingCarts.GetAll(u => u.ApplicationUserId == claim.Value);


			ShoppingCartVM.OrderHeader.OrderDate = System.DateTime.Now;
			ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;


			foreach (var cart in ShoppingCartVM.ListCart)
			{
				cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price,cart.Product.Price50, cart.Product.Price100);
				ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}

            ApplicationUser applicationUser = await wsPost<ApplicationUser,string>(SystemUrls.User.GetUserById, claim.Value);

      
			ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
			ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
			
			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
				ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
			}
			else
			{
				ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
				ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
			}
            ShoppingCartVM.OrderHeader  = await wsPost<OrderHeaderModel, OrderHeaderModel>(SystemUrls.Order.CreateOrderHeader, ShoppingCartVM.OrderHeader);

            await wsPost<ReturnModel, OrderDetailsAddModel>(SystemUrls.ShoppingCart.AddOrderDetails, new OrderDetailsAddModel
			{
				OrderId = ShoppingCartVM.OrderHeader.Id,
				CartList = ShoppingCartVM.ListCart
            });

          

			if (applicationUser.CompanyId.GetValueOrDefault() == 0)
			{
				//stripe settings 
				var domain = "https://localhost:44320/";
				var options = new SessionCreateOptions
				{
					PaymentMethodTypes = new List<string>
				{
				  "card",
				},
					LineItems = new List<SessionLineItemOptions>(),
					Mode = "payment",
					SuccessUrl = domain + $"Consumer/Cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
					CancelUrl = domain + $"Consumer/Cart/Index",
				};

				foreach (var item in ShoppingCartVM.ListCart)
				{

					var sessionLineItem = new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = (long)(item.Price * 100),//20.00 -> 2000
							Currency = "usd",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = item.Product.Title
							},

						},
						Quantity = item.Count,
					};
					options.LineItems.Add(sessionLineItem);

				}

				var service = new SessionService();
				Session session = service.Create(options);
                await wsPost<ReturnModel, UpdateStripePaymentModel>(SystemUrls.Order.UpdateStripePaymentID, 
					new UpdateStripePaymentModel() { Id = ShoppingCartVM.OrderHeader.Id, SessionId = session.Id, PaymentIntentId = session.PaymentIntentId });

			Response.Headers.Add("Location", session.Url);
				return new StatusCodeResult(303);

			}
			else
			{
				return RedirectToAction("OrderConfirmation", "Cart", new { id = ShoppingCartVM.OrderHeader.Id });
			}
			
		}
		public async Task<IActionResult> OrderConfirmation(int id)
		{
			OrderHeaderModel orderHeader = await wsPost<OrderHeaderModel, int>(SystemUrls.Order.GetOrderHeaderById, id); //_unitOfWork.OrderHeaders.GetFirstOrDefault(u => u.Id == id);
			if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
			{
				var service = new SessionService();
				Session session = service.Get(orderHeader.SessionId);
				//check the stripe status
				if (session.PaymentStatus.ToLower() == "paid")
				{
                    await wsPost<ReturnModel, UpdateStripePaymentModel>(SystemUrls.Order.UpdateStripePaymentID, new UpdateStripePaymentModel() { Id = id, SessionId = orderHeader.SessionId,PaymentIntentId=session.PaymentIntentId});
                    await wsPost<ReturnModel, UpdateStatusModel>(SystemUrls.Order.UpdateStatus, new UpdateStatusModel() { OrderHeaderId = id, OrderStatus = SD.StatusApproved, PaymentStatus = SD.PaymentStatusApproved });
				}
			}
            //_emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email, "New Order ", "<p>New Order Created</p>");


            await wsPost<ReturnModel, string>(SystemUrls.ShoppingCart.RemoveCarts, orderHeader.ApplicationUserId);

     		return View(id);
		}
		private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
		{
			if (quantity <= 50)
			{
				return price;
			}
			else
			{
				if (quantity <= 100)
				{
					return price50;
				}
				return price100;
			}
		}
	}
}

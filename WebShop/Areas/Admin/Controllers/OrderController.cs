using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Stripe;
using System.Security.Claims;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;
using WebShop.Utility;
using WebShop.Model.ViewModel;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebShop.Model.Models;

namespace WebShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class OrderController : Base
	{
		
		[BindProperty]
		public OrderVM OrderVM { get; set; }
		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Details(int orderId)
		{
			OrderVM = new OrderVM()
			{
				OrderHeader = await wsPost<OrderHeaderModel,int>(SystemUrls.Order.GetOrderHeaderById,orderId),// _unitOfWork.OrderHeaders.GetFirstOrDefault(u => u.Id == orderId),
				OrderDetail = await wsPost<IEnumerable<OrderDetailModel>,int>(SystemUrls.Order.GetOrderDetailsListById,orderId)// _unitOfWork.OrderDetails.GetAll(u => u.OrderId == orderId)
			};
			return View(OrderVM);
		}
		[HttpPost]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateOrderDetail()
		{

			var orderHEaderFromDb = await wsPost<OrderHeaderModel, OrderVM>(SystemUrls.Order.UpdateOrderDetails, OrderVM);

			
			TempData["Success"] = "Order Details Updated Successfully.";
			return RedirectToAction("Details", "Order", new { orderId = orderHEaderFromDb.Id });
		}

		[HttpPost]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> StartProcessing()
		{
			
            await wsPost<ReturnModel, UpdateStatusModel>(SystemUrls.Order.UpdateStatus, new UpdateStatusModel() { OrderHeaderId = OrderVM.OrderHeader.Id,OrderStatus = SD.StatusInProcess });

      		TempData["Success"] = "Order Status Updated Successfully.";
			return RedirectToAction("Details", "Order", new { orderId = OrderVM.OrderHeader.Id });
		}

		[HttpPost]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ShipOrder()
		{

            await wsPost<ReturnModel, OrderVM>(SystemUrls.Order.ShipOrder,OrderVM);


		
			TempData["Success"] = "Order Shipped Successfully.";
			return RedirectToAction("Details", "Order", new { orderId = OrderVM.OrderHeader.Id });
		}

		[HttpPost]
		[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
		[ValidateAntiForgeryToken]
		public  async Task<IActionResult> CancelOrder()
		{
			var orderHeader = await wsPost<OrderHeaderModel, int>(SystemUrls.Order.GetOrderHeaderById, OrderVM.OrderHeader.Id); //_unitOfWork.OrderHeaders.GetFirstOrDefault(u => u.Id == OrderVM.OrderHeader.Id);

            if (orderHeader.PaymentStatus == SD.PaymentStatusApproved)
			{
				var options = new RefundCreateOptions
				{
					Reason = RefundReasons.RequestedByCustomer,
					PaymentIntent = orderHeader.PaymentIntentId
				};

				var service = new RefundService();
				Refund refund = service.Create(options);


                await wsPost<ReturnModel, UpdateStatusModel>(SystemUrls.Order.UpdateStatus, new UpdateStatusModel() { OrderHeaderId = orderHeader.Id, OrderStatus = SD.StatusCancelled,PaymentStatus = SD.StatusRefunded });

			}
			else
			{
                await wsPost<ReturnModel, UpdateStatusModel>(SystemUrls.Order.UpdateStatus, new UpdateStatusModel() { OrderHeaderId = orderHeader.Id, OrderStatus = SD.StatusCancelled, PaymentStatus = SD.StatusCancelled });

			}

			TempData["Success"] = "Order Cancelled Successfully.";
			return RedirectToAction("Details", "Order", new { orderId = OrderVM.OrderHeader.Id });
		}

		[ActionName("Details")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Details_PAY_NOW()
		{
		OrderVM.OrderHeader = await wsPost<OrderHeaderModel, int>(SystemUrls.Order.GetOrderHeaderById, OrderVM.OrderHeader.Id);
			OrderVM.OrderDetail = await wsPost<IEnumerable<OrderDetailModel>, int>(SystemUrls.Order.GetOrderDetailsListById, OrderVM.OrderHeader.Id);

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
				SuccessUrl = domain + $"Admin/Order/PaymentConfirmation?orderHeaderid={OrderVM.OrderHeader.Id}",
				CancelUrl = domain + $"Admin/Order/Details?orderId={OrderVM.OrderHeader.Id}",
			};

			foreach (var item in OrderVM.OrderDetail)
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

			await wsPost<ReturnModel, UpdateStripePaymentModel>(SystemUrls.Order.UpdateStripePaymentID, new UpdateStripePaymentModel { Id = OrderVM.OrderHeader.Id ,SessionId = session.Id, PaymentIntentId = session.PaymentIntentId });

            Response.Headers.Add("Location", session.Url);
			return new StatusCodeResult(303);
		}


		public async Task<IActionResult> PaymentConfirmation(int orderHeaderid)
		{
            //OrderHeaderModel orderHeader = _unitOfWork.OrderHeaders.GetFirstOrDefault(u => u.Id == orderHeaderid);
            OrderHeaderModel orderHeader = await wsPost<OrderHeaderModel, int>(SystemUrls.Order.GetOrderHeaderById, orderHeaderid);

            if (orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment)
			{
				var service = new SessionService();
				Session session = service.Get(orderHeader.SessionId);
				//check the stripe status
				if (session.PaymentStatus.ToLower() == "paid")
				{

                    await wsPost<ReturnModel, UpdateStatusModel>(SystemUrls.Order.UpdateStatus, new UpdateStatusModel() { OrderHeaderId = orderHeaderid, OrderStatus = orderHeader.OrderStatus, PaymentStatus = SD.PaymentStatusApproved });
				}
			}
			return View(orderHeaderid);
		}

		#region API CALLS
		[HttpGet]
		public async Task<IActionResult> GetAll(string status)
		{
			IEnumerable<OrderHeaderModel> orderHeaders;

			


            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
			{
				orderHeaders = await wsGet<IEnumerable<OrderHeaderModel>>(SystemUrls.Order.GetOrders);
            }
			else
			{
				var claimsIdentity = (ClaimsIdentity)User.Identity;
				var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
				orderHeaders =   wsGet<IEnumerable<OrderHeaderModel>>(SystemUrls.Order.GetOrders).Result.Where(u => u.ApplicationUserId == claim.Value);
			}

			switch (status)
			{
				case "pending":
					orderHeaders = orderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment);
					break;
				case "inprocess":
					orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusInProcess);
					break;
				case "completed":
					orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusShipped);
					break;
				case "approved":
					orderHeaders = orderHeaders.Where(u => u.OrderStatus == SD.StatusApproved);
					break;
				default:
					break;
			}


			return Json(new { data = orderHeaders });
		}
		#endregion
	}
}

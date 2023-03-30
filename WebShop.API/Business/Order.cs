using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Model.ViewModel;
using WebShop.Models;
using WebShop.Utility;

namespace WebShop.API.Business
{
    public class Order
    {
        protected readonly IUnitOfWork _unitOfWork;


        public Order() { }


        public Order(List<object> objs)
        {
            _unitOfWork = objs[0] as IUnitOfWork;
        }

       
        public IEnumerable<OrderHeaderModel> GetOrders() => _unitOfWork.OrderHeaders.GetAll();


        public OrderHeaderModel GetOrderHeaderById(int id) => _unitOfWork.OrderHeaders.GetFirstOrDefault(u => u.Id == id);

        public IEnumerable<OrderDetailModel> GetOrderDetailsListById(int id) => _unitOfWork.OrderDetails.GetAll(u => u.OrderId == id);

        public ReturnModel UpdateStatus(UpdateStatusModel model)
        {
            var returnModel = new ReturnModel();
            _unitOfWork.OrderHeaders.UpdateStatus(model.OrderHeaderId, model.OrderStatus,model.PaymentStatus);
            _unitOfWork.Save();

            return returnModel;
        }

        public OrderHeaderModel UpdateOrderDetails(OrderVM model)
        {
            var returnModel = new ReturnModel();
            var orderHEaderFromDb = _unitOfWork.OrderHeaders.GetFirstOrDefault(u => u.Id == model.OrderHeader.Id);
            orderHEaderFromDb.Name = model.OrderHeader.Name;
            orderHEaderFromDb.PhoneNumber = model.OrderHeader.PhoneNumber;
            orderHEaderFromDb.StreetAddress = model.OrderHeader.StreetAddress;
            orderHEaderFromDb.City = model.OrderHeader.City;
            orderHEaderFromDb.State = model.OrderHeader.State;
            orderHEaderFromDb.PostalCode = model.OrderHeader.PostalCode;
            if (model.OrderHeader.Carrier != null)
            {
                orderHEaderFromDb.Carrier = model.OrderHeader.Carrier;
            }
            if (model.OrderHeader.TrackingNumber != null)
            {
                orderHEaderFromDb.TrackingNumber = model.OrderHeader.TrackingNumber;
            }
            _unitOfWork.OrderHeaders.Update(orderHEaderFromDb);
            _unitOfWork.Save();

            return orderHEaderFromDb;

        }

        public ReturnModel ShipOrder(OrderVM model)
        {
            var returnModel = new ReturnModel();
            var orderHeader = _unitOfWork.OrderHeaders.GetFirstOrDefault(u => u.Id == model.OrderHeader.Id);
            orderHeader.TrackingNumber = model.OrderHeader.TrackingNumber;
            orderHeader.Carrier = model.OrderHeader.Carrier;
            orderHeader.OrderStatus = SD.StatusShipped;
            orderHeader.ShippingDate = DateTime.Now;
            if (orderHeader.PaymentStatus == SD.PaymentStatusDelayedPayment)
            {
                orderHeader.PaymentDueDate = DateTime.Now.AddDays(30);
            }
            _unitOfWork.OrderHeaders.Update(orderHeader);
            _unitOfWork.Save();

            return returnModel;
        }


        public ReturnModel UpdateStripePaymentID(UpdateStripePaymentModel model)
        {
            var returnModel = new ReturnModel();
            _unitOfWork.OrderHeaders.UpdateStripePaymentID(model.Id, model.SessionId, model.PaymentIntentId);
            _unitOfWork.Save();

            return returnModel;

        }

        public OrderHeaderModel CreateOrderHeader(OrderHeaderModel model)
        {
           

            _unitOfWork.OrderHeaders.Add(model);
            _unitOfWork.Save();

            return model;
        }
    }
}

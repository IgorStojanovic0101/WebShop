using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;
using WebShop.Utility;

namespace WebShop.API.Business
{
    public class Order
    {
        protected readonly IUnitOfWork _unitOfWork;


        public Order() { }


        public Order(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<OrderHeaderModel> GetOrders() => _unitOfWork.OrderHeaders.GetAll();
            
          

    
            
        
    }
}

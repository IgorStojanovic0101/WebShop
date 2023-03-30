using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;

namespace WebShop.API.Business
{
    public class User 
    {
        protected readonly IUnitOfWork _unitOfWork;


        public User() { }


        public User(List<object> objs)
        {
            _unitOfWork = objs[0] as IUnitOfWork;
        }

        public ApplicationUser GetUserById(string id) => _unitOfWork.ApplicationUsers.GetAllAdmin().FirstOrDefault(u => u.Id == id);

    }
}

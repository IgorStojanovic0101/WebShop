using Microsoft.AspNetCore.Mvc;
using WebShop.API.Business;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;

namespace WebShop.API.Controllers
{
    public class UserController : Base<User>
    {

        public UserController(IUnitOfWork unitOfWork) : base(new List<object>() { unitOfWork })
        { }

        [HttpPost]
        public ApplicationUser GetUserById([FromBody] string id) => Call(x => x.GetUserById(id));
    }
}

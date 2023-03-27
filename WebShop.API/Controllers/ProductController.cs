using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using WebShop.API.Business;
using WebShop.DataAccess.Repository.IRepository;

namespace WebShop.API.Controllers
{
    public class ProductController : Base<Product>
    {

      
        public ProductController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {  }

        [HttpGet]
        public List<WebShop.Models.Product> GetProducts()  =>  Call(x => x.GetProducts());

       
    }
}

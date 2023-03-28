using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using WebShop.API.Business;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Models;

namespace WebShop.API.Controllers
{
    public class ProductController : Base<Product>
    {

      
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment) : base(new List<object>() { unitOfWork,hostEnvironment })
        {  }

        [HttpGet]
        public List<ProductModel> GetProducts()  =>  Call(x => x.GetProducts());

        [HttpPost]
        public ProductModel GetProductById([FromBody] int id) => Call(x => x.GetProductById(id));

        [HttpPost]
        public ReturnModel CreateProduct([FromBody] ProductModel model) => Call(x => x.CreateProduct(model));


        [HttpPost]
        public bool ProductExist([FromBody] int id) => Call(x => x.ProductExist(id));

        [HttpPost]
        public ReturnModel UpdateProduct([FromBody] ProductModel model) => Call(x => x.UpdateProduct(model));

        [HttpPost]
        public ReturnModel DeleteProduct([FromBody] int id) => Call(x => x.DeleteProduct(id));
    }
}

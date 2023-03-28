using Microsoft.AspNetCore.Mvc;
using WebShop.API.Business;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Models;

namespace WebShop.API.Controllers
{
    public class CategoryController : Base<Category>
    {
      


            public CategoryController(IUnitOfWork unitOfWork) : base(new List<object>() { unitOfWork })
            {  }

            [HttpGet]
            public List<CategoryModel> GetCategories() => Call(x => x.GetCategories());

            [HttpPost]
            public CategoryModel GetCategoryById([FromBody]int id) => Call(x => x.GetCategoryById(id));

            [HttpPost]
            public ReturnModel CreateCategory([FromBody] CategoryModel model) => Call(x => x.CreateCategory(model));


            [HttpPost]
            public bool CategoryExist([FromBody] int id) => Call(x => x.CategoryExist(id));

            [HttpPost]
            public ReturnModel UpdateCategory([FromBody] CategoryModel model) => Call(x => x.UpdateCategory(model));

            [HttpPost]
            public ReturnModel DeleteCategory([FromBody] int id) => Call(x => x.DeleteCategory(id));
    }
}

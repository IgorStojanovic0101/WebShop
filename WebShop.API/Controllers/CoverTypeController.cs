using Microsoft.AspNetCore.Mvc;
using WebShop.API.Business;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Models;

namespace WebShop.API.Controllers
{
    public class CoverTypeController : Base<CoverType>
    {
     
            public CoverTypeController(IUnitOfWork unitOfWork) : base(new List<object>() { unitOfWork })
        { }

            [HttpGet]
            public List<CoverTypeModel> GetCoverTypes() => Call(x => x.GetCoverTypes());

            [HttpPost]
            public CoverTypeModel GetCoverTypeById([FromBody] int id) => Call(x => x.GetCoverTypeById(id));

            [HttpPost]
            public ReturnModel CreateCoverType([FromBody] CoverTypeModel model) => Call(x => x.CreateCoverType(model));


            [HttpPost]
            public bool CoverTypeExist([FromBody] int id) => Call(x => x.CoverTypeExist(id));

            [HttpPost]
            public ReturnModel UpdateCoverType([FromBody] CoverTypeModel model) => Call(x => x.UpdateCoverType(model));

            [HttpPost]
            public ReturnModel DeleteCoverType([FromBody] int id) => Call(x => x.DeleteCoverType(id));
        }
    
}

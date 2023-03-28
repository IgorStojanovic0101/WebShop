using Microsoft.AspNetCore.Mvc;
using WebShop.API.Business;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Models;


namespace WebShop.API.Controllers
{
    public class CompanyController : Base<Company>
    {



        public CompanyController(IUnitOfWork unitOfWork) : base(new List<object>() { unitOfWork })
        { }

        [HttpGet]
        public List<CompanyModel> GetCompanies() => Call(x => x.GetCompanies());

        [HttpPost]
        public CompanyModel GetCompanyById([FromBody] int id) => Call(x => x.GetCompanyById(id));

        [HttpPost]
        public ReturnModel CreateCompany([FromBody] CompanyModel model) => Call(x => x.CreateCompany(model));


        [HttpPost]
        public bool CompanyExist([FromBody] int id) => Call(x => x.CompanyExist(id));

        [HttpPost]
        public ReturnModel UpdateCompany([FromBody] CompanyModel model) => Call(x => x.UpdateCompany(model));

        [HttpPost]
        public ReturnModel DeleteCompany([FromBody] int id) => Call(x => x.DeleteCompany(id));
    }
}

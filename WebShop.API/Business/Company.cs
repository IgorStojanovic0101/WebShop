using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Models;

namespace WebShop.API.Business
{
    public class Company
    {
        protected readonly IUnitOfWork _unitOfWork;


        public Company() { }


        public Company(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CompanyModel> GetCompanies() => _unitOfWork.Companies.GetAll().ToList();


        public CompanyModel GetCompanyById(int id) => _unitOfWork.Companies.GetFirstOrDefault(x => x.Id == id);


        public ReturnModel CreateCompany(CompanyModel model)
        {
            var returnModel = new ReturnModel();
            try
            {
                _unitOfWork.Companies.Add(model);
                _unitOfWork.Save();
                returnModel.Success = true;
            }
            catch (Exception ex)
            {
                returnModel.Success = false;

            }
            return returnModel;
        }
        public bool CompanyExist(int id) => this.GetCompanyById(id) != null;

        public ReturnModel UpdateCompany(CompanyModel model)
        {
            var returnModel = new ReturnModel();
            try
            {
                _unitOfWork.Companies.Update(model);
                _unitOfWork.Save();
                returnModel.Success = true;
            }
            catch (Exception ex)
            {
                returnModel.Success = false;

            }
            return returnModel;
        }
        public ReturnModel DeleteCompany(int id)
        {
            var returnModel = new ReturnModel();
            try
            {
                var model = _unitOfWork.Companies.GetFirstOrDefault(x => x.Id == id);
                _unitOfWork.Companies.Remove(model);
                _unitOfWork.Save();
                returnModel.Success = true;
            }
            catch (Exception ex)
            {
                returnModel.Success = false;

            }
            return returnModel;
        }
    }
}


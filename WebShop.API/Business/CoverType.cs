using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Models;

namespace WebShop.API.Business
{
    public class CoverType
    {
        protected readonly IUnitOfWork _unitOfWork;


        public CoverType() { }


        public CoverType(List<object> objs)
        {
            _unitOfWork = objs[0] as IUnitOfWork;
        }

        public List<CoverTypeModel> GetCoverTypes() => _unitOfWork.CoverTypes.GetAll().ToList();


        public CoverTypeModel GetCoverTypeById(int id) => _unitOfWork.CoverTypes.GetFirstOrDefault(x => x.Id == id);


        public ReturnModel CreateCoverType(CoverTypeModel model)
        {
            var returnModel = new ReturnModel();
            try
            {
                _unitOfWork.CoverTypes.Add(model);
                _unitOfWork.Save();
                returnModel.Success = true;
            }
            catch (Exception ex)
            {
                returnModel.Success = false;

            }
            return returnModel;
        }
        public bool CoverTypeExist(int id) => this.GetCoverTypeById(id) != null;

        public ReturnModel UpdateCoverType(CoverTypeModel model)
        {
            var returnModel = new ReturnModel();
            try
            {
                _unitOfWork.CoverTypes.Update(model);
                _unitOfWork.Save();
                returnModel.Success = true;
            }
            catch (Exception ex)
            {
                returnModel.Success = false;

            }
            return returnModel;
        }
        public ReturnModel DeleteCoverType(int id)
        {
            var returnModel = new ReturnModel();
            try
            {
                var model = _unitOfWork.CoverTypes.GetFirstOrDefault(x => x.Id == id);
                _unitOfWork.CoverTypes.Remove(model);
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

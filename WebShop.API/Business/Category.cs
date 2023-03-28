using WebShop.DataAccess.Repository;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Models;

namespace WebShop.API.Business
{
    public class Category
    {

     

        public Category() { }


        protected IUnitOfWork _unitOfWork;


        public Category(List<object> objs)
        {  
            _unitOfWork = objs[0] as IUnitOfWork;
        }

        public List<CategoryModel> GetCategories() => _unitOfWork.Categories.GetAll().ToList();


        public CategoryModel GetCategoryById(int id) => _unitOfWork.Categories.GetFirstOrDefault(x=>x.Id==id);


        public ReturnModel CreateCategory(CategoryModel model)
        {
            var returnModel = new ReturnModel();
            try
            {
                _unitOfWork.Categories.Add(model);
                _unitOfWork.Save();
                returnModel.Success = true;
            }
            catch (Exception ex)
            {
                returnModel.Success = false;

            }
            return returnModel;
        }
        public bool CategoryExist(int id) => _unitOfWork.Categories.CategoryExist(id);

        public ReturnModel UpdateCategory(CategoryModel model)
        {
            var returnModel = new ReturnModel();
            try
            {
                _unitOfWork.Categories.Update(model);
                _unitOfWork.Save();
                returnModel.Success = true;
            }
            catch (Exception ex)
            {
                returnModel.Success = false;

            }
            return returnModel;
        }
        public ReturnModel DeleteCategory(int id)
        {
            var returnModel = new ReturnModel();
            try
            {
                var model = _unitOfWork.Categories.GetFirstOrDefault(x=>x.Id==id);
                _unitOfWork.Categories.Remove(model);
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

using Microsoft.Extensions.Hosting;
using WebShop.Bussiness;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Model.Models;
using WebShop.Models;

namespace WebShop.API.Business
{
    public class Product
    {

        protected readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public Product()  { }


        public Product(List<object> objs)
        {
            _unitOfWork = objs[0] as IUnitOfWork;
            _hostEnvironment = objs[1] as IWebHostEnvironment;
        }

        public List<ProductModel> GetProducts() => _unitOfWork.Products.GetAll().ToList();
      
        public ProductModel GetProductById(int id) => _unitOfWork.Products.GetFirstOrDefault(x => x.Id == id);

        private void SaveProductFile(ProductModel model)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (model.file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var uploads = Path.Combine(wwwRootPath, @"images\products");
                var extension = Path.GetExtension(model.file.FileName);

                if (model.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, model.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    model.file.CopyTo(fileStreams);
                }
                model.ImageUrl = @"\images\products\" + filename + extension;
            }
        }

        public ReturnModel CreateProduct(ProductModel model)
        {
            var returnModel = new ReturnModel();

            this.SaveProductFile(model);
                try
            {
                _unitOfWork.Products.Add(model);
                _unitOfWork.Save();
                returnModel.Success = true;
            }
            catch (Exception ex)
            {
                returnModel.Success = false;

            }
            return returnModel;
        }
        public bool ProductExist(int id) => this.GetProductById(id) != null;

        public ReturnModel UpdateProduct(ProductModel model)
        {
            var returnModel = new ReturnModel();

            this.SaveProductFile(model);
            try
            {
                _unitOfWork.Products.Update(model);
                _unitOfWork.Save();
                returnModel.Success = true;
            }
            catch (Exception ex)
            {
                returnModel.Success = false;

            }
            return returnModel;
        }
        public ReturnModel DeleteProduct(int id)
        {
            var returnModel = new ReturnModel();
            try
            {
                var model = _unitOfWork.Products.GetFirstOrDefault(x => x.Id == id);

                if (model.ImageUrl != null)
                {
                    var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, model.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }


                _unitOfWork.Products.Remove(model);
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

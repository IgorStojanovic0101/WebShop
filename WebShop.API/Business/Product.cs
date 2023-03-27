using WebShop.Bussiness;
using WebShop.DataAccess.Repository.IRepository;

namespace WebShop.API.Business
{
    public class Product
    {

        protected readonly IUnitOfWork _unitOfWork;


        public Product()  { }


        public Product(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Models.Product> GetProducts() => this._unitOfWork.Products.GetAll().ToList();


    }
}

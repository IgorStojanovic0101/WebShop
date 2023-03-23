using WebShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.DataAccess.Repository.IRepository;

namespace WebShop.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Categories = new CategoryRepository(db);
            CoverTypes = new CoverTypeRepository(db);
            Products = new ProductRepository(db);
            Companies = new CompanyRepository(db);
            ApplicationUsers = new ApplicationUserRepository(db);
            ShoppingCarts = new ShoppingCartRepository(db);
            OrderDetails= new OrderDetailsRepository(db);
            OrderHeaders = new OrderHeaderRepository(db);

        }
        public ICategoryRepository Categories { get; private set; }

        public ICoverTypeRepository CoverTypes { get; private set; }

        public IProductRepository Products { get; private set; }

        public ICompanyRepository Companies { get; private set; }

        public IApplicationUser ApplicationUsers { get; private set; }

        public IShoppingCart ShoppingCarts { get; private set; }

		public IOrderDetailsRepository OrderDetails { get;private set; }

		public IOrderHeaderRepository OrderHeaders { get; private set; }

		public void Save()
        {
            _db.SaveChanges();
        }
    }
}

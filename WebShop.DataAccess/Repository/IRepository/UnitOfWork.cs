using WebShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DataAccess.Repository.IRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Categories = new CategoryRepository(db);
            CoverTypes = new CoverTypeRepository(db);

        }
        public ICategoryRepository Categories { get;private set; }

        public ICoverTypeRepository CoverTypes { get; private set; }
        public void Save() 
        {
            _db.SaveChanges();
        }
    }
}

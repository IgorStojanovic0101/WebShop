using WebShop.Data;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Model.Models;

namespace WebShop.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

    
        public void Update(Company obj)
        {
           _db.Update(obj);
        }

    }
}

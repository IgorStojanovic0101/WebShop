using WebShop.Data;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

    
        public void Update(CoverType ct)
        {
           _db.Update(ct);
        }

    }
}

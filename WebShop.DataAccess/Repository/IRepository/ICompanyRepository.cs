using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Model.Models;

namespace WebShop.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<CompanyModel>
    {
        void Update(CompanyModel obj);


    }
}

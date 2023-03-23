using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Model.Models;
using System.Linq.Expressions;

namespace WebShop.DataAccess.Repository.IRepository
{
    public interface IApplicationUser : IRepository<ApplicationUser>
    {
        void Update(ApplicationUser obj);

		IEnumerable<ApplicationUser> GetAllAdmin(Expression<Func<ApplicationUser, bool>>? filter = null);
	}
}

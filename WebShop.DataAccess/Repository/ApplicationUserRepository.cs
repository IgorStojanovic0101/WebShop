using WebShop.Data;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Model.ViewModel;
using WebShop.Model.Models;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebShop.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUser
    {
        private ApplicationDbContext _db;
	
		public ApplicationUserRepository(ApplicationDbContext db): base(db)
        {
			
			_db = db;
        }

		public IEnumerable<ApplicationUser> GetAllAdmin(Expression<Func<ApplicationUser, bool>>? filter = null)
		{

	
		var properties = typeof(ApplicationUser).GetProperties();
		var includeList = properties.Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string))
		 .Select(p => p.Name)
		 .ToList();

		IQueryable<ApplicationUser> query = dbSet;
		query = query.Where(filter ?? (x => true));

		var results = query.ToList();

		if (results.Count() > 0)
		{
			var query2 = Enumerable.Empty<ApplicationUser>().AsQueryable();

				

				var query1 = query.Where(x => x.CompanyId == null).ToList();
					foreach (var propertyInfo in includeList)
				query2 = query.Where(x => x.CompanyId != null).Include(propertyInfo);

				query1.AddRange(query2);
				query = query1.AsQueryable();
				//foreach(var item in results)
				//{
				//foreach (var propertyInfo in includeList)
				//item.Company = item.CompanyId.HasValue ? GetFirstOrDefault(x => x.Id.Equals(item.CompanyId.Value)):null;
				//}

			//	foreach (var propertyInfo in includeList)
			//		query2 = query.Where(x => x.CompanyId.HasValue).Include(propertyInfo).AsQueryable();

			//	query = query.Include(string.Empty).AsQueryable();

			//var NEW = query.Union(query2);
			}

			return query.ToList();

		}

		public void Update(ApplicationUser obj)
        {
           _db.Update(obj);
        }

    }
}

using WebShop.Data;
using WebShop.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebShop.Model.Models;

namespace WebShop.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        
        public void Add(T entity)
        {
           dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {				
            var properties = typeof(T).GetProperties();
            var includeList = properties.Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string)).ToArray();

			IQueryable<T> query = dbSet;
			query = query.Where(filter ?? (x => true));
        
			
              
			foreach (var propertyInfo in includeList)
				query = query.Include(propertyInfo.Name);
				
             
            
		
			return query;
         }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {

            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if (query.Count() > 0)
            {
                var includeList = typeof(T).GetProperties().Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string));
                foreach (var propertyInfo in includeList)
                    query = query.Include(propertyInfo.Name);
            }
			return query.FirstOrDefault();
         }

        public void Remove(T entity)
        {
           dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);    
                
         }
    }

}

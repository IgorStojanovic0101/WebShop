﻿using WebShop.Data;
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
    public class CategoryRepository : Repository<CategoryModel>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public bool CategoryExist(int id)
        {
           
             return GetAll().Any(x => x.Id == id);
            
        }
        public CategoryModel FindCategory(int id)
        {

            return GetFirstOrDefault(x => x.Id == id);

        }

      

        public void Update(CategoryModel obj)
        {
           _db.Update(obj);
        }

    }
}

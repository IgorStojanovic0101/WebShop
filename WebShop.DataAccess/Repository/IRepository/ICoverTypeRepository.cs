﻿using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverTypeModel>
    {
        void Update(CoverTypeModel obj);


    }
}

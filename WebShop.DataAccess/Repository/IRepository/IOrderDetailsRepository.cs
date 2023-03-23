﻿using WebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace WebShop.DataAccess.Repository.IRepository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail obj);

      //  IEnumerable<OrderDetail> GetAllWithProperties(Expression<Func<OrderDetail, bool>>? filter = null);
    }
}

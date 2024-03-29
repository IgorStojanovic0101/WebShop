﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        ICoverTypeRepository CoverTypes { get; }

         IProductRepository Products { get; }

	     ICompanyRepository Companies { get; }

        IApplicationUser ApplicationUsers { get; }
        IShoppingCart ShoppingCarts { get; }

        IOrderDetailsRepository OrderDetails { get; }
        IOrderHeaderRepository OrderHeaders { get; }

		void Save();
    }
}

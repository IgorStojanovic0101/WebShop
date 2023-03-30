using WebShop.Models;
using Microsoft.EntityFrameworkCore;
using WebShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebShop.Model.Models;
using WebShop.Model.ViewModel;

namespace WebShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 

        }

		public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CoverTypeModel> CoverTypes { get; set; }
        public DbSet<ProductModel> Products { get; set; }
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }

		public DbSet<CompanyModel> Companies { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<OrderDetailModel> OrderDetails { get; set; }
        public DbSet<OrderHeaderModel> OrderHeaders { get; set; }

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Model.Models;
using WebShop.Models;

namespace WebShop.Bussiness
{
    public class New 
    {
        public int Method1()
        {
            return 1;
        }
        public Product Method2()
        {
            return new Product { Author = "Igor " };
        }
        public int Method3(Product product)
        {
            if (product.Id == 5)
                return 1;
            else
                return 0;
           
        }
        public Product Method4(int BR)
        {
            if (BR == 5)
                return new Product { Author="Igor je genije"};
            else
                return new Product();

        }
        public Product Method5(Product Product)
        {
            if (Product.Id > 0)
                return new Product { Author = "Igor je genije veliki" };
            else
                return new Product();

        }
        public Product Method6(CoverType CT, Product test)
        {
            if (test.Id > 0)
                return new Product { Author = "Igor je genije veliki" };
            else
                return new Product();

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebShop.Bussiness;
using WebShop.Model.Models;
using WebShop.Models;

namespace WebShop.API.Controllers
{

 
    public class NewController : Base<New>
    {


     
        
        [HttpGet]
        public Product Index1()
        {
          
           //var s = Call(x => x.Method2());
           // var product = new Product { Id = 5 };

            int results = Call(x => x.Method1());
            var product = new Product { Author = "Igor" };
            int id = 5;
            return product;

        }

        [HttpPost]
        public Product Index3(int id) => Call(x => x.Method4(id));


        [HttpPost]
        public Product Index4(Product product) => Call(x => x.Method5(product));

        // [Route ("[action]")]
        [HttpPost]
        public Product Index5(CoverType ct,  Product test) => Call(x => x.Method6(ct,test));


        private static readonly string[] Summaries = new[]
       {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
         };
        private static readonly string[] Summaries2 = new[]
         {
        "Igor", "iggy"
         };


      
        [HttpGet]

        public string[] Index2()
        {
            return Summaries2;
        }
        [HttpPost]

        public void PostMethod(CoverType ct)
        {
            var cts = ct;
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Utility
{
    public class SystemUrls
    {
        public class New
        {
            private const string pref = "New/";
            public const string Index4 = pref + "Index4";
            public const string FindUsers = pref + "FindUsers";

        }
        public class Temp
        {
            private const string pref = "Temp/";
            public const string GetUsers = pref + "GetUsers";
            public const string FindUsers = pref + "FindUsers";

        }
         public static class Product
        {
            private const string pref = "Product/";
            public const string GetProducts = pref + "GetProducts";
          
        }
    }
}

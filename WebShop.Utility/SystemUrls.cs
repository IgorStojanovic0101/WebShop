using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Utility
{
    public class SystemUrls
    {
        public class User
        {
            private const string pref = "User/";
            public const string GetUserById = pref + "GetUserById";

        }
        public class Order
        {
            private const string pref = "Order/";
            public const string GetOrders = pref + "GetOrders";
            public const string GetOrderHeaderById = pref + "GetOrderHeaderById";
            public const string GetOrderDetailsListById = pref + "GetOrderDetailsListById";
            public const string UpdateStatus = pref + "UpdateStatus";
            public const string UpdateOrderDetails = pref + "UpdateOrderDetails";
            public const string ShipOrder = pref + "ShipOrder";
            public const string CancelOrder = pref + "CancelOrder";
            public const string UpdateStripePaymentID = pref + "UpdateStripePaymentID";

            public const string CreateOrderHeader = pref + "CreateOrderHeader";


        }
        public class Category
        {
            private const string pref = "Category/";
            public const string GetCategories = pref + "GetCategories";
            public const string GetCategoryById = pref + "GetCategoryById";
            public const string CreateCategory = pref + "CreateCategory";
            public const string CategoryExist = pref + "CategoryExist";
            public const string UpdateCategory = pref + "UpdateCategory";
            public const string DeleteCategory = pref + "DeleteCategory";

        }

        public class Company
        {
            private const string pref = "Company/";
            public const string GetCompanies = pref + "GetCompanies";
            public const string GetCompanyById = pref + "GetCompanyById";
            public const string CreateCompany = pref + "CreateCompany";
            public const string CompanyExist = pref + "CompanyExist";
            public const string UpdateCompany = pref + "UpdateCompany";
            public const string DeleteCompany = pref + "DeleteCompany";

        }
        public class CoverType
        {
            private const string pref = "CoverType/";
            public const string GetCoverTypes = pref + "GetCoverTypes";
            public const string GetCoverTypeById = pref + "GetCoverTypeById";
            public const string CreateCoverType = pref + "CreateCoverType";
            public const string CoverTypeExist = pref + "CoverTypeExist";
            public const string UpdateCoverType = pref + "UpdateCoverType";
            public const string DeleteCoverType = pref + "DeleteCoverType";

        }
        public static class Product
        {
            private const string pref = "Product/";
            public const string GetProducts = pref + "GetProducts";
            public const string GetProductById = pref + "GetProductById";
            public const string CreateProduct = pref + "CreateProduct";
            public const string ProductExist = pref + "ProductExist";
            public const string UpdateProduct = pref + "UpdateProduct";
            public const string DeleteProduct = pref + "DeleteProduct";

        }
        public static class ShoppingCart
        {
            private const string pref = "Cart/";
            public const string RemoveCarts = pref + "RemoveCarts";
            public const string GetCartListByApplicationUserId = pref + "GetCartListByApplicationUserId";
            public const string AddOrderDetails = pref + "AddOrderDetails";
            public const string Minus = pref + "Minus";
            public const string Plus = pref + "Plus";
            public const string GetShoppingCartBySearchModel = pref + "GetShoppingCartBySearchModel";
            public const string AddShoppingCart = pref + "AddShoppingCart";
            public const string IncrementCount = pref + "IncrementCount";



        }



    }
}

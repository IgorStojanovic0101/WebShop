using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Utility
{
    public class SystemUrls
    {
        public class Order
        {
            private const string pref = "Order/";
            public const string GetOrders = pref + "GetOrders";
            public const string FindUsers = pref + "FindUsers";

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

    }
}

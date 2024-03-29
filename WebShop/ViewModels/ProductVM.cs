﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShop.Models;

namespace WebShop.ViewModels
{
    public class ProductVM
    {
        [ValidateNever]

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        [ValidateNever]

        public IEnumerable<SelectListItem> CoverTypeList { get; set; }

        public ProductModel Product { get; set; }


    }
}

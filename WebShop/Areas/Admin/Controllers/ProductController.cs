using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.DataAccess.Repository.IRepository;
using WebShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShop.ViewModels;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using WebShop.Utility;
using WebShop.Model.Models;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Base
	{
       // private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController()
        {
          //  _hostEnvironment = hostEnvironment;
        }

    
        public async Task<IActionResult> Index()
        {

            var result = new List<ProductModel>();
     
            return View(result);
        }

      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var exist = await wsPost<bool, int>(SystemUrls.Product.ProductExist, id.Value);


            if (!exist)
                return NotFound();
            else
            {
                var obj = await wsPost<ProductModel, int>(SystemUrls.Product.GetProductById, id.Value);
                return View(obj);
            }
        }
        public async Task<IActionResult> Upsert(int? id)
        {

            var productVM = new ProductVM()
            {
                Product = new(),
                CategoryList =  wsGet<List<CategoryModel>>(SystemUrls.Category.GetCategories).Result.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                CoverTypeList =  wsGet<List<CoverTypeModel>>(SystemUrls.CoverType.GetCoverTypes).Result.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })

            };
            if (id == null || id == 0)
            {
                return View(nameof(Edit), productVM);
            }
            else
            {
                productVM.Product = await wsPost<ProductModel, int>(SystemUrls.Product.GetProductById, id.Value);
                return View(nameof(Edit), productVM);

            }
        }
      
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel obj)
        {

            if (ModelState.IsValid)
            {
                await wsPost<ReturnModel, ProductModel>(SystemUrls.Product.UpdateProduct, obj);
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var exist = await wsPost<bool, int>(SystemUrls.Product.ProductExist, id.Value);


            if (!exist)
                return NotFound();
            else
            {
                var obj = await wsPost<ProductModel, int>(SystemUrls.Product.GetProductById, id.Value);
                return View(obj);
            }

        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductVM obj, IFormFile? file)
        {
         
            if (ModelState.IsValid)
            {
                obj.Product.file = file;

             /*   string wwwRootPath = _hostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\products");
                    var extension = Path.GetExtension(file.FileName);

                    if(obj.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var fileStreams = new FileStream(Path.Combine(uploads,filename+extension),FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + filename + extension;
                }
             */
                if (obj.Product.Id != 0)
                {
                    await wsPost<ReturnModel, ProductModel>(SystemUrls.Product.UpdateProduct, obj.Product);

                }
                else
                {
                    await wsPost<ReturnModel, ProductModel>(SystemUrls.Product.CreateProduct, obj.Product);


                }
              

                return RedirectToAction("Index");
            }
            return View(nameof(Edit), obj);
        }

     


        #region

        [HttpGet]
        public  async Task<IActionResult> GetAll()
        {
            var productList = await wsGet<List<ProductModel>>(SystemUrls.Product.GetProducts);
            return Json(new { data = productList }); 
        }


        [HttpDelete]
        
        public async Task<IActionResult> Delete(int? id)
        {

            var returnModel = await wsPost<ReturnModel, int>(SystemUrls.Product.DeleteProduct, id.Value);
         
            return Json(new { success = true, messege = "Delete Sucessful" });
        }


        #endregion
    }
}

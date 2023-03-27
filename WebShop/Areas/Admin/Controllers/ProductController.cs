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

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Base
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
			
            var result =  await wsGet<List<Product>>(SystemUrls.Product.GetProducts);
     
            return View(result);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.Products.GetAll() == null)
            {
                return NotFound();
            }

            var obj = _unitOfWork.Products.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        public IActionResult Upsert(int? id)
        {

            var productVM = new ProductVM()
            {
                Product = new(),
                CategoryList = _unitOfWork.Categories.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                CoverTypeList =  _unitOfWork.CoverTypes.GetAll().Select(x => new SelectListItem
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
                productVM.Product = _unitOfWork.Products.GetFirstOrDefault(x => x.Id == id);
                return View(nameof(Edit), productVM);

            }
        }
      
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Products.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.Products.GetAll() == null)
            {
                return NotFound();
            }


            var obj = _unitOfWork.Products.GetFirstOrDefault(x => x.Id == id);
            return View(obj);

        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductVM obj, IFormFile? file)
        {
         
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
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
                if (obj.Product.Id != 0)
                {
                    _unitOfWork.Products.Update(obj.Product);
                }
                else
                {
                    _unitOfWork.Products.Add(obj.Product);

                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(nameof(Edit), obj);
        }

     


        #region

        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Products.GetAll();
            return Json(new { data = productList }); 
        }


        [HttpDelete]
        
        public IActionResult Delete(int? id)
        {
          
            var obj = _unitOfWork.Products.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, messege = "Error while deleting" });

            }
            if (obj.ImageUrl != null)
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            
            _unitOfWork.Products.Remove(obj);
            _unitOfWork.Save();
            // await _context.SaveChangesAsync();
            return Json(new { success = true, messege = "Delete Sucessful" });
        }


        #endregion
    }
}

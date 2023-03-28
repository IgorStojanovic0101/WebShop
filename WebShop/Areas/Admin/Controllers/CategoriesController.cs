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
using WebShop.Utility;
using WebShop.DataAccess.Repository;
using WebShop.Model.Models;

namespace WebShop.Areas.Admin.Controllers
{
     [Area("Admin")]
    public class CategoriesController : Base
    {

       
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var list = await wsGet<List<CategoryModel>>(SystemUrls.Category.GetCategories);
            return View(list);
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            

            var category_exist = await wsPost<bool, int>(SystemUrls.Category.CategoryExist, id.Value);


            if (!category_exist)
                return NotFound();  
            else
            {
                var category = await wsPost<CategoryModel, int>(SystemUrls.Category.GetCategoryById, id.Value);
                return View(category);
            }
        }

      
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Ne moze isto ime za Name i DispayOrder..");
            }
            if (ModelState.IsValid)
            {
                await wsPost<ReturnModel, CategoryModel>(SystemUrls.Category.CreateCategory, category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category_exist = await wsPost<bool, int>(SystemUrls.Category.CategoryExist, id.Value);


            if (!category_exist)
            {
                return NotFound();
            }
            else
            {
                var category = await wsPost<CategoryModel, int>(SystemUrls.Category.GetCategoryById, id.Value);
                return View(category);
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryModel category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
              
                var returnModel = await wsPost<ReturnModel, CategoryModel>(SystemUrls.Category.UpdateCategory, category);
             
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category_exist = await wsPost<bool, int>(SystemUrls.Category.CategoryExist, id.Value);


            if (!category_exist)
            {
                return NotFound();
            }
            else
            {
                var category = await wsPost<CategoryModel, int>(SystemUrls.Category.GetCategoryById, id.Value);
                return View(category);
            }

        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
         
          
           var returnModel = await wsPost<ReturnModel, int>(SystemUrls.Category.DeleteCategory, id);
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

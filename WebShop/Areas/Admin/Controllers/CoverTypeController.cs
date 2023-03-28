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
using WebShop.Model.Models;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Base
    {
     

       
       
        public async Task<IActionResult> Index()
        {
            var list = await wsGet<List<CoverTypeModel>>(SystemUrls.CoverType.GetCoverTypes);
            return View(list);
        }

     
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var exist = await wsPost<bool, int>(SystemUrls.CoverType.CoverTypeExist, id.Value);


            if (!exist)
                return NotFound();
            else
            {
                var obj = await wsPost<CoverTypeModel, int>(SystemUrls.CoverType.GetCoverTypeById, id.Value);
                return View(obj);
            }
        }

      
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoverTypeModel obj)
        {

            if (ModelState.IsValid)
            {
                await wsPost<ReturnModel, CoverTypeModel>(SystemUrls.CoverType.CreateCoverType, obj);
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


            var exist = await wsPost<bool, int>(SystemUrls.CoverType.CoverTypeExist, id.Value);


            if (!exist)
                return NotFound();
            else
            {
                var obj = await wsPost<CoverTypeModel, int>(SystemUrls.CoverType.GetCoverTypeById, id.Value);
                return View(obj);
            }

        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  CoverTypeModel obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await wsPost<ReturnModel, CoverTypeModel>(SystemUrls.CoverType.UpdateCoverType, obj);
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var exist = await wsPost<bool, int>(SystemUrls.CoverType.CoverTypeExist, id.Value);


            if (!exist)
                return NotFound();
            else
            {
                var obj = await wsPost<CoverTypeModel, int>(SystemUrls.CoverType.GetCoverTypeById, id.Value);
                return View(obj);
            }
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var returnModel = await wsPost<ReturnModel, int>(SystemUrls.CoverType.DeleteCoverType, id);
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

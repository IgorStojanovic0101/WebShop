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
using WebShop.Model.Models;
using WebShop.Utility;

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Base
    {
      
      

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var list = await wsGet<List<CompanyModel>>(SystemUrls.Company.GetCompanies);
            return View(list);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var exist = await wsPost<bool, int>(SystemUrls.Company.CompanyExist, id.Value);


            if (!exist)
                return NotFound();
            else
            {
                var obj = await wsPost<CompanyModel, int>(SystemUrls.Company.GetCompanyById, id.Value);
                return View(obj);
            }

        }
     
      
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyModel obj)
        {

            if (ModelState.IsValid)
            {
                await wsPost<ReturnModel, CompanyModel>(SystemUrls.Company.CreateCompany, obj);
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var exist = await wsPost<bool, int>(SystemUrls.Company.CompanyExist, id.Value);


            if (!exist)
                return NotFound();
            else
            {
                var obj = await wsPost<CompanyModel, int>(SystemUrls.Company.GetCompanyById, id.Value);
                return View(obj);
            }

        }

        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyModel obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await wsPost<ReturnModel, CompanyModel>(SystemUrls.Company.UpdateCompany, obj);
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }



      

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var List = await wsGet<List<CompanyModel>>(SystemUrls.Company.GetCompanies);
            return Json(new { data = List }); 
        }



      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var exist = await wsPost<bool, int>(SystemUrls.Company.CompanyExist, id.Value);


            if (!exist)
                return NotFound();
            else
            {
                var obj = await wsPost<CompanyModel, int>(SystemUrls.Company.GetCompanyById, id.Value);
                return View(obj);
            }
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var returnModel = await wsPost<ReturnModel, int>(SystemUrls.Company.DeleteCompany, id);
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

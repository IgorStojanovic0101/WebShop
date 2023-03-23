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

namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
      

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var list = _unitOfWork.Companies.GetAll();
            return View(list);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.Companies.GetAll() == null)
            {
                return NotFound();
            }

            var obj = _unitOfWork.Companies.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
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
        public async Task<IActionResult> Create(Company obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Companies.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.Companies.GetAll() == null)
            {
                return NotFound();
            }


            var obj = _unitOfWork.Companies.GetFirstOrDefault(x => x.Id == id);
            return View(obj);

        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Company obj)
        {
            if (id != obj.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
              
                    _unitOfWork.Companies.Update(obj);
                    _unitOfWork.Save();
               
             
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }



      

        [HttpGet]
        public IActionResult GetAll()
        {
            var List = _unitOfWork.Companies.GetAll();
            return Json(new { data = List }); 
        }



        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.Companies.GetAll() == null)
            {
                return NotFound();
            }

            var obj = _unitOfWork.Companies.GetFirstOrDefault(m => m.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.Companies.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Companies'  is null.");
            }
            // var category = await _context.Categories.FindAsync(id);
            var obj = _unitOfWork.Companies.GetFirstOrDefault(x => x.Id == id);
            if (obj != null)
            {
                _unitOfWork.Companies.Remove(obj);
            }
            _unitOfWork.Save();
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

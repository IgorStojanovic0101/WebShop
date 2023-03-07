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


namespace WebShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var list = _unitOfWork.CoverTypes.GetAll();
            return View(list);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.CoverTypes.GetAll() == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.CoverTypes.GetFirstOrDefault(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoverType ct)
        {
           
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverTypes.Add(ct);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(ct);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.CoverTypes.GetAll() == null)
            {
                return NotFound();
            }

          
            var category = _unitOfWork.CoverTypes.GetFirstOrDefault(x => x.Id == id);
            return View(category);
            
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CoverType ct)
        {
            if (id != ct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.CoverTypes.Update(ct);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.Categories.CategoryExist(ct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ct);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.CoverTypes.GetAll() == null)
            {
                return NotFound();
            }

            var coverType = _unitOfWork.CoverTypes.GetFirstOrDefault(m => m.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.CoverTypes.GetAll() == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CoverType'  is null.");
            }
            // var category = await _context.Categories.FindAsync(id);
            var coverType = _unitOfWork.CoverTypes.GetFirstOrDefault(x => x.Id == id);
            if (coverType != null)
            {
                _unitOfWork.CoverTypes.Remove(coverType);
            }
            _unitOfWork.Save();
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

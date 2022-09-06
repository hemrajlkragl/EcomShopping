using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcomShopping.Data;
using EcomShopping.Models;

namespace EcomShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialTagController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SpecialTag
        public async Task<IActionResult> Index()
        {
              return _context.SpecialTag != null ? 
                          View(await _context.SpecialTag.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SpecialTag'  is null.");
        }

        // GET: Admin/SpecialTag/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SpecialTag == null)
            {
                return NotFound();
            }

            var specialTag = await _context.SpecialTag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        // GET: Admin/SpecialTag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SpecialTag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] SpecialTag specialTag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialTag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialTag);
        }

        // GET: Admin/SpecialTag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SpecialTag == null)
            {
                return NotFound();
            }

            var specialTag = await _context.SpecialTag.FindAsync(id);
            if (specialTag == null)
            {
                return NotFound();
            }
            return View(specialTag);
        }

        // POST: Admin/SpecialTag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] SpecialTag specialTag)
        {
            if (id != specialTag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialTagExists(specialTag.Id))
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
            return View(specialTag);
        }

        // GET: Admin/SpecialTag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SpecialTag == null)
            {
                return NotFound();
            }

            var specialTag = await _context.SpecialTag
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialTag == null)
            {
                return NotFound();
            }

            return View(specialTag);
        }

        // POST: Admin/SpecialTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SpecialTag == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SpecialTag'  is null.");
            }
            var specialTag = await _context.SpecialTag.FindAsync(id);
            if (specialTag != null)
            {
                _context.SpecialTag.Remove(specialTag);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialTagExists(int id)
        {
          return (_context.SpecialTag?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

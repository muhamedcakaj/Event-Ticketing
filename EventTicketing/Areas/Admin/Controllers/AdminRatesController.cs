using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventTicketing.Data;
using EventTicketing.Models;

namespace EventTicketing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminRatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminRates
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rates.Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AdminRates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate = await _context.Rates
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rate == null)
            {
                return NotFound();
            }

            return View(rate);
        }

        // GET: Admin/AdminRates/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminRates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Message,CreatedAt")] Rate rate)
        {
          
                _context.Add(rate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         
        }

        // GET: Admin/AdminRates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate = await _context.Rates.FindAsync(id);
            if (rate == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", rate.UserId);
            return View(rate);
        }

        // POST: Admin/AdminRates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Message,CreatedAt")] Rate rate)
        {
            if (id != rate.Id)
            {
                return NotFound();
            }

        
                    _context.Update(rate);
                    await _context.SaveChangesAsync();
            
                return RedirectToAction(nameof(Index));
      
        }

        // GET: Admin/AdminRates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rate = await _context.Rates
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rate == null)
            {
                return NotFound();
            }

            return View(rate);
        }

        // POST: Admin/AdminRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rate = await _context.Rates.FindAsync(id);
            if (rate != null)
            {
                _context.Rates.Remove(rate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RateExists(int id)
        {
            return _context.Rates.Any(e => e.Id == id);
        }
    }
}

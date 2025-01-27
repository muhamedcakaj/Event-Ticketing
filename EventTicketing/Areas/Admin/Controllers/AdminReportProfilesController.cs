using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventTicketing.Data;

namespace EventTicketing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminReportProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminReportProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminReportProfiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReportProfiles.Include(r => r.ReportedBy).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AdminReportProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportProfile = await _context.ReportProfiles
                .Include(r => r.ReportedBy)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportProfile == null)
            {
                return NotFound();
            }

            return View(reportProfile);
        }

        // GET: Admin/AdminReportProfiles/Create
        public IActionResult Create()
        {
            ViewData["ReportedById"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminReportProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ReportedById")] ReportProfile reportProfile)
        {
           
                _context.Add(reportProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminReportProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportProfile = await _context.ReportProfiles.FindAsync(id);
            if (reportProfile == null)
            {
                return NotFound();
            }
            ViewData["ReportedById"] = new SelectList(_context.ApplicationUsers, "Id", "Id", reportProfile.ReportedById);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", reportProfile.UserId);
            return View(reportProfile);
        }

        // POST: Admin/AdminReportProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ReportedById")] ReportProfile reportProfile)
        {
            if (id != reportProfile.Id)
            {
                return NotFound();
            }

         
                    _context.Update(reportProfile);
                    await _context.SaveChangesAsync();
              
                return RedirectToAction(nameof(Index));
        
        }

        // GET: Admin/AdminReportProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportProfile = await _context.ReportProfiles
                .Include(r => r.ReportedBy)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportProfile == null)
            {
                return NotFound();
            }

            return View(reportProfile);
        }

        // POST: Admin/AdminReportProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportProfile = await _context.ReportProfiles.FindAsync(id);
            if (reportProfile != null)
            {
                _context.ReportProfiles.Remove(reportProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportProfileExists(int id)
        {
            return _context.ReportProfiles.Any(e => e.Id == id);
        }
    }
}

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
    public class AdminReportPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminReportPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminReportPosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ReportPosts.Include(r => r.Post).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AdminReportPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportPosts = await _context.ReportPosts
                .Include(r => r.Post)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportPosts == null)
            {
                return NotFound();
            }

            return View(reportPosts);
        }

        // GET: Admin/AdminReportPosts/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminReportPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,PostId")] ReportPosts reportPosts)
        {
           
                _context.Add(reportPosts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
          
        }

        // GET: Admin/AdminReportPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportPosts = await _context.ReportPosts.FindAsync(id);
            if (reportPosts == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Description", reportPosts.PostId);
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", reportPosts.UserId);
            return View(reportPosts);
        }

        // POST: Admin/AdminReportPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PostId")] ReportPosts reportPosts)
        {
            if (id != reportPosts.Id)
            {
                return NotFound();
            }

         
                    _context.Update(reportPosts);
                    await _context.SaveChangesAsync();
             
                return RedirectToAction(nameof(Index));
         
        }

        // GET: Admin/AdminReportPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reportPosts = await _context.ReportPosts
                .Include(r => r.Post)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportPosts == null)
            {
                return NotFound();
            }

            return View(reportPosts);
        }

        // POST: Admin/AdminReportPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reportPosts = await _context.ReportPosts.FindAsync(id);
            if (reportPosts != null)
            {
                _context.ReportPosts.Remove(reportPosts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportPostsExists(int id)
        {
            return _context.ReportPosts.Any(e => e.Id == id);
        }
    }
}

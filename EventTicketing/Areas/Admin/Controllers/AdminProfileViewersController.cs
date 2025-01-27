using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventTicketing.Data;
using EventTicketing.Models;
using Microsoft.AspNetCore.Authorization;

namespace EventTicketing.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminProfileViewersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminProfileViewersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProfileViewers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProfileViewers.Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AdminProfileViewers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileViewer = await _context.ProfileViewers
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profileViewer == null)
            {
                return NotFound();
            }

            return View(profileViewer);
        }

        // GET: Admin/AdminProfileViewers/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminProfileViewers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Message,CreatedAt")] ProfileViewer profileViewer)
        {
           
                _context.Add(profileViewer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminProfileViewers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileViewer = await _context.ProfileViewers.FindAsync(id);
            if (profileViewer == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", profileViewer.UserId);
            return View(profileViewer);
        }

        // POST: Admin/AdminProfileViewers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Message,CreatedAt")] ProfileViewer profileViewer)
        {
            if (id != profileViewer.Id)
            {
                return NotFound();
            }

           
                _context.Update(profileViewer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminProfileViewers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profileViewer = await _context.ProfileViewers
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profileViewer == null)
            {
                return NotFound();
            }

            return View(profileViewer);
        }

        // POST: Admin/AdminProfileViewers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profileViewer = await _context.ProfileViewers.FindAsync(id);
            if (profileViewer != null)
            {
                _context.ProfileViewers.Remove(profileViewer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileViewerExists(int id)
        {
            return _context.ProfileViewers.Any(e => e.Id == id);
        }
    }
}

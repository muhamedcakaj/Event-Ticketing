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
    public class AdminRepostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminRepostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminReposts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reposts.Include(r => r.OriginalPost).Include(r => r.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AdminReposts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repost = await _context.Reposts
                .Include(r => r.OriginalPost)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repost == null)
            {
                return NotFound();
            }

            return View(repost);
        }

        // GET: Admin/AdminReposts/Create
        public IActionResult Create()
        {
            ViewData["OriginalPostId"] = new SelectList(_context.Posts, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminReposts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OriginalPostId,UserId,CreatedAt")] Repost repost)
        {
          
                _context.Add(repost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminReposts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repost = await _context.Reposts.FindAsync(id);
            if (repost == null)
            {
                return NotFound();
            }
            ViewData["OriginalPostId"] = new SelectList(_context.Posts, "Id", "Description", repost.OriginalPostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", repost.UserId);
            return View(repost);
        }

        // POST: Admin/AdminReposts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OriginalPostId,UserId,CreatedAt")] Repost repost)
        {
            if (id != repost.Id)
            {
                return NotFound();
            }

        
                    _context.Update(repost);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminReposts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repost = await _context.Reposts
                .Include(r => r.OriginalPost)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repost == null)
            {
                return NotFound();
            }

            return View(repost);
        }

        // POST: Admin/AdminReposts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repost = await _context.Reposts.FindAsync(id);
            if (repost != null)
            {
                _context.Reposts.Remove(repost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepostExists(int id)
        {
            return _context.Reposts.Any(e => e.Id == id);
        }
    }
}

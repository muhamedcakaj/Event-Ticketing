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
    public class AdminLikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminLikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminLikes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Likes.Include(l => l.Post).Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AdminLikes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likes = await _context.Likes
                .Include(l => l.Post)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (likes == null)
            {
                return NotFound();
            }

            return View(likes);
        }

        // GET: Admin/AdminLikes/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminLikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PostId,UserId,CreatedAt")] Likes likes)
        {
          
                _context.Add(likes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminLikes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likes = await _context.Likes.FindAsync(id);
            if (likes == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Description", likes.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", likes.UserId);
            return View(likes);
        }

        // POST: Admin/AdminLikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostId,UserId,CreatedAt")] Likes likes)
        {
            if (id != likes.Id)
            {
                return NotFound();
            }

         
                    _context.Update(likes);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminLikes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var likes = await _context.Likes
                .Include(l => l.Post)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (likes == null)
            {
                return NotFound();
            }

            return View(likes);
        }

        // POST: Admin/AdminLikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var likes = await _context.Likes.FindAsync(id);
            if (likes != null)
            {
                _context.Likes.Remove(likes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LikesExists(int id)
        {
            return _context.Likes.Any(e => e.Id == id);
        }
    }
}

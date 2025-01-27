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
    public class AdminSavePostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminSavePostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminSavePosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SavePosts.Include(s => s.Post).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AdminSavePosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savePost = await _context.SavePosts
                .Include(s => s.Post)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savePost == null)
            {
                return NotFound();
            }

            return View(savePost);
        }

        // GET: Admin/AdminSavePosts/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminSavePosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,PostId,CreatedAt")] SavePost savePost)
        {
           
                _context.Add(savePost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         
        }

        // GET: Admin/AdminSavePosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savePost = await _context.SavePosts.FindAsync(id);
            if (savePost == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Description", savePost.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", savePost.UserId);
            return View(savePost);
        }

        // POST: Admin/AdminSavePosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PostId,CreatedAt")] SavePost savePost)
        {
            if (id != savePost.Id)
            {
                return NotFound();
            }

         
                  _context.Update(savePost);
                 await _context.SaveChangesAsync();
              
                return RedirectToAction(nameof(Index));

        }

        // GET: Admin/AdminSavePosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savePost = await _context.SavePosts
                .Include(s => s.Post)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savePost == null)
            {
                return NotFound();
            }

            return View(savePost);
        }

        // POST: Admin/AdminSavePosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var savePost = await _context.SavePosts.FindAsync(id);
            if (savePost != null)
            {
                _context.SavePosts.Remove(savePost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavePostExists(int id)
        {
            return _context.SavePosts.Any(e => e.Id == id);
        }
    }
}

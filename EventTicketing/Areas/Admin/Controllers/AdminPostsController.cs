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
    public class AdminPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminPosts
        public async Task<IActionResult> Index(string location, string title, string userId, bool? only2025, string sortBy = "CreatedAt", string sortOrder = "desc", int pageNumber = 1)
        {
            int pageSize = 5; // Number of posts per page

            var query = _context.Posts.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(p => p.Location.Contains(location));
            }

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(p => p.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(p => p.UserId == userId);
            }

            if (only2025.HasValue && only2025.Value)
            {
                query = query.Where(p => p.EventDate.Year == 2025);
            }

            // Apply sorting
            query = sortBy switch
            {
                "Title" => sortOrder == "asc" ? query.OrderBy(p => p.Title) : query.OrderByDescending(p => p.Title),
                "EventDate" => sortOrder == "asc" ? query.OrderBy(p => p.EventDate) : query.OrderByDescending(p => p.EventDate),
                "Location" => sortOrder == "asc" ? query.OrderBy(p => p.Location) : query.OrderByDescending(p => p.Location),
                _ => sortOrder == "asc" ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt),
            };

            // Pagination
            int totalPosts = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalPosts / (double)pageSize);

            var posts = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Pass data to the view
            ViewData["CurrentPage"] = pageNumber;
            ViewData["TotalPages"] = totalPages;
            ViewData["LocationFilter"] = location;
            ViewData["TitleFilter"] = title;
            ViewData["UserIdFilter"] = userId;
            ViewData["Only2025Filter"] = only2025;
            ViewData["SortBy"] = sortBy;
            ViewData["SortOrder"] = sortOrder;

            return View(posts);
        }

        // GET: Admin/AdminPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Admin/AdminPosts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,EventDate,Location,CreatedAt,UserId")] Post post)
        {
         
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        // POST: Admin/AdminPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,EventDate,Location,CreatedAt,UserId")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

        
                    _context.Update(post);
                    await _context.SaveChangesAsync();
             
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin/AdminPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}

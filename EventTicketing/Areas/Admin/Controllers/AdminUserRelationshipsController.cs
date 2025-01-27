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
    public class AdminUserRelationshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminUserRelationshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminUserRelationships
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserRelationships.Include(u => u.FollowedUser).Include(u => u.FollowerUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AdminUserRelationships/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRelationship = await _context.UserRelationships
                .Include(u => u.FollowedUser)
                .Include(u => u.FollowerUser)
                .FirstOrDefaultAsync(m => m.FollowerId == id);
            if (userRelationship == null)
            {
                return NotFound();
            }

            return View(userRelationship);
        }

        // GET: Admin/AdminUserRelationships/Create
        public IActionResult Create()
        {
            ViewData["FollowedId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["FollowerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminUserRelationships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FollowerId,FollowedId")] UserRelationship userRelationship)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRelationship);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FollowedId"] = new SelectList(_context.Users, "Id", "Id", userRelationship.FollowedId);
            ViewData["FollowerId"] = new SelectList(_context.Users, "Id", "Id", userRelationship.FollowerId);
            return View(userRelationship);
        }

        // GET: Admin/AdminUserRelationships/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRelationship = await _context.UserRelationships.FindAsync(id);
            if (userRelationship == null)
            {
                return NotFound();
            }
            ViewData["FollowedId"] = new SelectList(_context.Users, "Id", "Id", userRelationship.FollowedId);
            ViewData["FollowerId"] = new SelectList(_context.Users, "Id", "Id", userRelationship.FollowerId);
            return View(userRelationship);
        }

        // POST: Admin/AdminUserRelationships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("FollowerId,FollowedId")] UserRelationship userRelationship)
        {
            if (id != userRelationship.FollowerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRelationship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRelationshipExists(userRelationship.FollowerId))
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
            ViewData["FollowedId"] = new SelectList(_context.Users, "Id", "Id", userRelationship.FollowedId);
            ViewData["FollowerId"] = new SelectList(_context.Users, "Id", "Id", userRelationship.FollowerId);
            return View(userRelationship);
        }

        // GET: Admin/AdminUserRelationships/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRelationship = await _context.UserRelationships
                .Include(u => u.FollowedUser)
                .Include(u => u.FollowerUser)
                .FirstOrDefaultAsync(m => m.FollowerId == id);
            if (userRelationship == null)
            {
                return NotFound();
            }

            return View(userRelationship);
        }

        // POST: Admin/AdminUserRelationships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userRelationship = await _context.UserRelationships.FindAsync(id);
            if (userRelationship != null)
            {
                _context.UserRelationships.Remove(userRelationship);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRelationshipExists(string id)
        {
            return _context.UserRelationships.Any(e => e.FollowerId == id);
        }
    }
}

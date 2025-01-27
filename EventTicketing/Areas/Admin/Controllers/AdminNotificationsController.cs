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
    public class AdminNotificationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminNotificationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminNotifications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Notifications.Include(n => n.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AdminNotifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notifications == null)
            {
                return NotFound();
            }

            return View(notifications);
        }

        // GET: Admin/AdminNotifications/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/AdminNotifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Message,CreatedAt")] Notifications notifications)
        {
          
                _context.Add(notifications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminNotifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications.FindAsync(id);
            if (notifications == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", notifications.UserId);
            return View(notifications);
        }

        // POST: Admin/AdminNotifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Message,CreatedAt")] Notifications notifications)
        {
            if (id != notifications.Id)
            {
                return NotFound();
            }

         
                    _context.Update(notifications);
                    await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Admin/AdminNotifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notifications = await _context.Notifications
                .Include(n => n.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notifications == null)
            {
                return NotFound();
            }

            return View(notifications);
        }

        // POST: Admin/AdminNotifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notifications = await _context.Notifications.FindAsync(id);
            if (notifications != null)
            {
                _context.Notifications.Remove(notifications);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotificationsExists(int id)
        {
            return _context.Notifications.Any(e => e.Id == id);
        }
    }
}

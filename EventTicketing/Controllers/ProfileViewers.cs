using EventTicketing.Data;
using EventTicketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventTicketing.Controllers
{
    [Authorize(Roles = "user")]
    public class ProfileViewersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileViewersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewers = await _context.ProfileViewers
                .Where(v => v.UserId == userId)
                .Include(u => u.User)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();

            return View(viewers);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var viewer = await _context.ProfileViewers.FindAsync(id);
            if (viewer != null)
            {
                _context.ProfileViewers.Remove(viewer);

                var log = new Log
                {
                    Action = "Delete Profile Viewer",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Logs.Add(log);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
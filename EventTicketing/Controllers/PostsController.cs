using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventTicketing.Data;
using EventTicketing.Models;
using Microsoft.AspNetCore.Authorization;

namespace EventTicketing.Controllers
{
    [Authorize(Roles = "user")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Post()
        {
            return View();
        }
        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,EventDate,Location,CreatedAt")] Post post)
        {
            var UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                post.UserId = UserId;
                post.CreatedAt = DateTime.UtcNow;

                _context.Add(post);
            var log = new Log
            {
                Action = "Create Post",
                UserId = UserId
            };
                _context.Logs.Add(log);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
        }
    }
}

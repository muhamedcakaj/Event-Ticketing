using System.Security.Claims;
using EventTicketing.Data;
using EventTicketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventTicketing.Controllers
{
    [Authorize(Roles = "user")]
    public class LikesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userLikes = await _context.Likes
                .Where(l => l.UserId == userId)
                .Include(l => l.Post)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();

            return View(userLikes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var likedpost = await _context.Likes.FindAsync(id);
            if (likedpost == null || likedpost.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            //Deleting Saved Post

            _context.Likes.Remove(likedpost);

           var log = new Log
           {
               Action = "Delete Like",
               UserId = userId
           };
            _context.Logs.Add(log);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventTicketing.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using EventTicketing.Models;

namespace EventTicketing.Controllers
{
    [Authorize(Roles = "user")]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        // GET: Profile
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userPosts = await _context.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            return View(userPosts);
        }
        //Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null || post.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }

            return View(post);
        }

        //Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

           
                try
                {
                    post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Ensure user ID remains unchanged
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null || post.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            //Deleting Repost
            var reposts = await _context.Reposts
            .Where(r => r.OriginalPostId == id)
            .ToListAsync();

            _context.Reposts.RemoveRange(reposts);

            //Deleting Likes
            var likes = await _context.Likes
           .Where(l => l.PostId  == id)
           .ToListAsync();

            _context.Likes.RemoveRange(likes);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
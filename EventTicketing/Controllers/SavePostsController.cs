using System.Security.Claims;
using EventTicketing.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Identity.Client;

namespace EventTicketing.Controllers
{

    [Authorize(Roles = "user")]
    public class SavePostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SavePostsController(ApplicationDbContext context)
        {
            _context = context;
        }

       public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var userSavePosts = await _context.SavePosts
                .Where(sp => sp.UserId == userId)
                .Include(sp => sp.Post)
                .OrderByDescending(sp => sp.CreatedAt)
                .ToListAsync();

            return View(userSavePosts);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var savedPost = await _context.SavePosts.FindAsync(id);
            if (savedPost == null || savedPost.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Unauthorized();
            }
            //Deleting Saved Post

            _context.SavePosts.Remove(savedPost);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
    }

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EventTicketing.Models;
using System.Linq;
using System.Threading.Tasks;
using EventTicketing.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventTicketing.Controllers
{
    [Authorize(Roles = "user")]
    public class SearchController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public SearchController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var users = _userManager.Users
                .Where(u => u.UserName.Contains(searchTerm))
                .ToList();

            return View("Index", users);
        }

        public async Task<IActionResult> Profile(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var userPosts = _context.Posts
                .Where(p => p.UserId == userId)
                .ToList();

            ViewBag.Username = user.UserName; // Pass the username for the view

            // Get logged-in user ID
            var loggedInUserId = _userManager.GetUserId(User);

            //Populate profile Viewer
            var userloggedIn = await _context.Users
                 .FirstOrDefaultAsync(u => u.Id == loggedInUserId);

            var profileViewer = new ProfileViewer
            {
                UserId = userId,
                Message = userloggedIn.UserName + " has visited your profile "
            };
            _context.ProfileViewers.Add(profileViewer);
            _context.SaveChanges();

            // Populate profile Viewer

            // Check follow relationships
            var isFollowing = _context.UserRelationships
                .Any(ur => ur.FollowerId == loggedInUserId && ur.FollowedId == userId);

            var isFollowedBy = _context.UserRelationships
                .Any(ur => ur.FollowerId == userId && ur.FollowedId == loggedInUserId);

            ViewBag.FollowStatus = isFollowing
                ? "Unfollow"
                : (isFollowedBy ? "Follow Back" : "Follow");

            ViewBag.ProfileUserId = userId; // Send profile user ID to view

            return View(userPosts);
        }

        [HttpPost]
        public async Task<IActionResult> Follow(string userId)
        {
            var loggedInUserId = _userManager.GetUserId(User);


            if (string.IsNullOrEmpty(loggedInUserId) || string.IsNullOrEmpty(userId))
            {
                return BadRequest("Invalid user IDs.");
            }

            var existingFollow = _context.UserRelationships
                .FirstOrDefault(ur => ur.FollowerId == loggedInUserId && ur.FollowedId == userId);

            if (existingFollow == null)
            {
                var newFollow = new UserRelationship
                {
                    FollowerId = loggedInUserId,
                    FollowedId = userId
                };
                //Log
                var log = new Log
                {
                    Action = "Followed User",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Logs.Add(log);
                //Log

                //Adding Notifications for follower type

                var userWhoIsFollowing = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == loggedInUserId);

                var notifications = new Notifications
                {
                    UserId = userId,
                    Message = "The User " + userWhoIsFollowing.UserName + " has followed you"
                };
                //Adding Notifications for follower type

                _context.Notifications.Add(notifications);
                _context.UserRelationships.Add(newFollow);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile", new { userId });
        }

        [HttpPost]
        public async Task<IActionResult> Unfollow(string userId)
        {
            var loggedInUserId = _userManager.GetUserId(User);

            var existingFollow = _context.UserRelationships
                .FirstOrDefault(ur => ur.FollowerId == loggedInUserId && ur.FollowedId == userId);

            if (existingFollow != null)
            {
                //Log
                var log = new Log
                {
                    Action = "Unfollowed User",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Logs.Add(log);
                //Log
                _context.UserRelationships.Remove(existingFollow);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Profile", new { userId });
        }
        
        [HttpPost]
        public async Task<IActionResult> Report(string userId)
        {
            var loggedInUserId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(loggedInUserId) || string.IsNullOrEmpty(userId))
            {
                return BadRequest("Invalid user IDs.");
            }

            // Check if the report already exists to avoid duplicate reports
            var existingReport = await _context.ReportProfiles
                .FirstOrDefaultAsync(rp => rp.UserId == userId && rp.ReportedById == loggedInUserId);

            if (existingReport == null)
            {
                var newReport = new ReportProfile
                {
                    UserId = userId,
                    ReportedById = loggedInUserId // Add who reported the user
                };

                _context.ReportProfiles.Add(newReport);
                await _context.SaveChangesAsync();
            }

            // Redirect back to the profile page
            return RedirectToAction("Profile", new { userId });
        }
        
    }
}
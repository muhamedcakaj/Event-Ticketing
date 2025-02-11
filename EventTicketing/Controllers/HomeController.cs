using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EventTicketing.Models;
using EventTicketing.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace EventTicketing.Controllers
{
    [Authorize(Roles = "user")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var loggedInUserId = _userManager.GetUserId(User);

            // Get followed users' IDs
            var followedUserIds = _context.UserRelationships
                .Where(ur => ur.FollowerId == loggedInUserId)
                .Select(ur => ur.FollowedId)
                .ToList();

            // Get followed users' original posts
            var followedUsersPosts = _context.Posts
                .Where(p => followedUserIds.Contains(p.UserId))
                .OrderByDescending(p => p.CreatedAt)
                .Include(p => p.User)
                .ToList();

            // Get reposts from followed users (with original post info)
            var followedUsersReposts = _context.Reposts
                .Where(r => followedUserIds.Contains(r.UserId))
                .OrderByDescending(r => r.CreatedAt)
                .Include(r => r.OriginalPost)
                .ThenInclude(op => op.User)
                .Include(r => r.User)
                .ToList();

            // Combine posts and reposts into a single feed
            var feed = followedUsersPosts
             .Select(p => new { Post = p, IsRepost = false, RepostedBy = (ApplicationUser)null })
             .Union(followedUsersReposts.Select(r => new { Post = r.OriginalPost, IsRepost = true, RepostedBy = r.User }))
             .OrderByDescending(x => x.Post.CreatedAt)
             .ToList();

            // Get reposted post IDs by the logged-in user
            var repostedPostIds = _context.Reposts
                .Where(r => r.UserId == loggedInUserId)
                .Select(r => r.OriginalPostId)
                .ToList();

            ViewBag.RepostedPosts = repostedPostIds;

            var likedPostIds = _context.Likes
                .Where(l => l.UserId == loggedInUserId)
                .Select(l => l.PostId)
                .ToList();

            ViewBag.LikedPosts = likedPostIds;

            //Get Saved Posts id-s
            var savedPostIds = _context.SavePosts
            .Where(s => s.UserId == loggedInUserId)
            .Select(s => s.PostId)
            .ToList();

            ViewBag.SavedPosts = savedPostIds;

            return View(feed);
        }

        [HttpPost]
        public async Task<IActionResult> Repost(int postId)
        {
            var loggedInUserId = _userManager.GetUserId(User);

            // Check if the post is already reposted
            var existingRepost = await _context.Reposts
                .FirstOrDefaultAsync(r => r.OriginalPostId == postId && r.UserId == loggedInUserId);

            if (existingRepost == null)
            {
                // Get the original post if this is a reposted post
                var originalPostId = postId;
                var repostedPost = await _context.Reposts.FirstOrDefaultAsync(r => r.Id == postId);
                if (repostedPost != null)
                {
                    originalPostId = repostedPost.OriginalPostId;
                }

                var repost = new Repost
                {
                    OriginalPostId = originalPostId,
                    UserId = loggedInUserId
                };

                //Adding Notifications for repost type

                var originalpost = await _context.Posts
                .Include(p => p.User)  // Eagerly load the User navigation property
                .FirstOrDefaultAsync(p => p.Id == postId);

                var userWhoIsReposting = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == loggedInUserId);


                //Log
                var log = new Log
                {
                    Action = "Reposted Post",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Logs.Add(log);
                //Log

                var notifications = new Notifications
                {
                    UserId = originalpost.UserId,
                    Message = "The User '"+ userWhoIsReposting.UserName+ "' has reposted your post "+originalpost.Title
                };
                //Adding Notifications for repost type

                _context.Notifications.Add(notifications);
                _context.Reposts.Add(repost);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRepost(int postId)
        {
            var loggedInUserId = _userManager.GetUserId(User);

            var repost = await _context.Reposts
                .FirstOrDefaultAsync(r => r.OriginalPostId == postId && r.UserId == loggedInUserId);

            if (repost != null)
            {
                //Log
                var log = new Log
                {
                    Action = "Remove Repost",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Logs.Add(log);
                //Log
                _context.Reposts.Remove(repost);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Like (int postId)
        {
            var loggedInUserId = _userManager.GetUserId(User);
            
            var existingLike = await _context.Likes
                .FirstOrDefaultAsync(l=>l.PostId==postId && l.UserId == loggedInUserId);

            if (existingLike == null)
            {
                var like = new Likes
                {
                    PostId = postId,
                    UserId = loggedInUserId
                };
                //Log
                var log = new Log
                {
                    Action = "Liked Post",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Logs.Add(log);
                //Log

                //Adding Notifications for like type
                var originalpost = await _context.Posts
                .Include(p => p.User) 
                .FirstOrDefaultAsync(p => p.Id == postId);

                var userWhoIsReposting = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == loggedInUserId);

                var notifications = new Notifications
                {
                    UserId = originalpost.UserId,
                    Message = "The User '" + userWhoIsReposting.UserName + "' has liked your post " + originalpost.Title
                };
                //Adding Notifications for repost type

                _context.Notifications.Add(notifications);
                _context.Likes.Add(like);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult>Dislike (int postId)
        {
            var loggedInUserId = _userManager.GetUserId(User);

            var dislike = await _context.Likes
                .FirstOrDefaultAsync(l => l.PostId == postId && l.UserId==loggedInUserId );

            if(dislike !=null)
            {
                //Log
                var log = new Log
                {
                    Action = "Unliked Post",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Logs.Add(log);
                //Log
                _context.Remove(dislike);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Save(int postId)
        {
            var loggedInUserId = _userManager.GetUserId(User);

            var existingSave = await _context.SavePosts
                .FirstOrDefaultAsync(s => s.PostId == postId && s.UserId == loggedInUserId);

            if (existingSave == null)
            {
                var savePost = new SavePost
                {
                    PostId = postId,
                    UserId = loggedInUserId
                };

                //Log
                var log = new Log
                {
                    Action = "Saved Post",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Logs.Add(log);
                //Log

                _context.SavePosts.Add(savePost);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Unsave(int postId)
        {
            var loggedInUserId = _userManager.GetUserId(User);

            var savedPost = await _context.SavePosts
                .FirstOrDefaultAsync(s => s.PostId == postId && s.UserId == loggedInUserId);

            if (savedPost != null)
            {
                //Log
                var log = new Log
                {
                    Action = "Unsave Post",
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                _context.Logs.Add(log);
                //Log
                _context.SavePosts.Remove(savedPost);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ReportPost(int postId)
        {
            var loggedInUserId = _userManager.GetUserId(User);

            if (postId <= 0)
            {
                return BadRequest("Invalid post ID.");
            }

            // Check if the post has already been reported by this user
            var existingReport = await _context.ReportPosts
                .FirstOrDefaultAsync(rp => rp.UserId == loggedInUserId && rp.PostId == postId);

            if (existingReport != null)
            {
                return BadRequest("You have already reported this post.");
            }

            // Create a new report for the post
            var report = new ReportPosts
            {
                UserId = loggedInUserId,
                PostId = postId
            };

            //Log
            var log = new Log
            {
                Action = "Reported Post",
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            _context.Logs.Add(log);
            //Log

            _context.ReportPosts.Add(report);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");  // Or wherever you want to redirect after reporting
        }
    }
}
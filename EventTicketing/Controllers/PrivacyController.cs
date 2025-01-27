using EventTicketing.Data;
using EventTicketing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventTicketing.Controllers
{
    [Authorize(Roles = "user")]
    public class PrivacyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PrivacyController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize] // Ensure only logged-in users can submit a rating
        public async Task<IActionResult> SubmitRating(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                TempData["Error"] = "Message cannot be empty.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "You need to be logged in to submit a rating.";
                return RedirectToAction("Index");
            }

            var rating = new Rate
            {
                UserId = user.Id,
                Message = message
            };

            var log = new Log
            {
                Action = "Submit Rating",
                UserId = user.Id
            };
            _context.Logs.Add(log);

            _context.Rates.Add(rating);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thank you for your feedback!";
            return RedirectToAction("Index");
        }
    }
}
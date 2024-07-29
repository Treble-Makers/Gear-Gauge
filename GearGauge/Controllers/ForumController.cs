using System;
using System.Linq;
using System.Threading.Tasks;
using GearGauge.Data;
using GearGauge.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GearGauge.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly GearGaugeDbContext _context;
        private readonly UserManager<User> _userManager;

        public ForumController(GearGaugeDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var comments = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Replies)
                    .ThenInclude(r => r.User)
                .Where(c => c.ParentCommentId == null)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return View(comments);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(string content, int? parentCommentId)
        {
            if (string.IsNullOrEmpty(content))
            {
                return BadRequest("Looks like you forgot to share something.");
            }

            var user = await _userManager.GetUserAsync(User);
            var comment = new Comment
            {
                Content = content,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id,
                ParentCommentId = parentCommentId
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
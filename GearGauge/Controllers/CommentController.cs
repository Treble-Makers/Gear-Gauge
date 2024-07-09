using Microsoft.AspNetCore.Mvc;
using GearGauge.ViewModels;
using GearGauge.Models;
using GearGauge.Data;

namespace GearGauge.Controllers;
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly MusicItemDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController (MusicItemDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            context = dbContext;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int musicItemId, string content)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var comment = new Comment
            {
                MusicItemId = musicItemId,
                Content = content,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow 
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "MusicItems", new { id = musicItemId });
        }
}
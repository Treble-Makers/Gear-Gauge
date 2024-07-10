using Microsoft.AspNetCore.Mvc;
using GearGauge.ViewModels;
using GearGauge.Models;
using GearGauge.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.Controllers;

    [Authorize]
    public class CommentController : Controller
    {
        private readonly GearGaugeDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CommentController (GearGaugeDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _context = dbContext;
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
                GearInventory = gearInventory, // new name for MusicItemId
                // MusicItem property is looking for MusicItem object, is receiving an int Id 
                Content = content,
                Id = user.Id, // somehow the user.ID is a string?? 

                CreatedAt = DateTime.UtcNow 
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            

            return RedirectToAction("Details", "MusicItems", new { id = gearInventory }); // make it commentId. 
            //It was musicItemId
        }
    }
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GearGauge.Data;
using GearGauge.Models;
using Microsoft.AspNetCore.Authorization;
using GearGauge.ViewModels;

namespace GearGauge.Controllers 
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly GearGaugeDbContext _context;
        private readonly UserManager<User> _userManager;

        public FavoritesController(GearGaugeDbContext dbContext, UserManager<User> userManager)
        {
           _context = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> List()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var favorites = await _context.Favorites
                .Where(f => f.UserId == user.Id)
                .Include(f => f.Gear)
                .ToListAsync();

            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorite(int gearId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.GearId == gearId && f.UserId == user.Id);
            
            if (existingFavorite != null)
            {
                return RedirectToAction("Detail", "GearInventory", new { id = gearId }); 
            }

            var gear = await _context.Gear.FindAsync(gearId);  // Changed from GearInventory to Gear
            if (gear == null)
            {
                return NotFound();
            }

            var favorite = new Favorites
            {
                GearId = gearId,
                UserId = user.Id,
                Gear = gear
            };

            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return RedirectToAction("Detail", "GearInventory", new { id = gearId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorite(int gearId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.GearId == gearId && f.UserId == user.Id);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }
    }
}
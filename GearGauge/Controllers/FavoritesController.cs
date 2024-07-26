using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GearGauge.Data;
using GearGauge.Models;
using System.Threading.Tasks;

namespace GearGauge.Controllers;

    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly GearGaugeDbContext _context;
        private readonly UserManager<User> _userManager;

        public FavoritesController(GearGaugeDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> List()
        {
             var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            // var user = await _userManager.GetUserAsync(User);
            var favorites = await _context.Favorites
                .Where(f => f.UserId == user.Id)
                .Include(f => f.GearInventory)
                .ToListAsync();
            return View(favorites);
        }

        [HttpGet("AddToFavorites")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToFavorites(int id)
        { 
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var gearInventory = await _context.GearInventories.FindAsync(id);
            if (gearInventory == null)
            {
                return NotFound();
            }

            var existingFavorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.GearInventory == gearInventory);

            if (existingFavorite == null)
            {
                var favorite = new Favorites
                {
                    UserId = user.Id,
                    GearInventory= gearInventory
                };
                _context.Favorites.Add(favorite);
                _context.SaveChanges();
            
                return RedirectToAction("List", favorite);
            }
            return View();
            // return RedirectToAction("AddToFavorites", "Favorites", new { id = id });
        }


            // [HttpPost]
    // public async Task<IActionResult> RemoveFromFavorites(int id)
    // {
    //     var user = await _userManager.GetUserAsync(User);
    //     var favorite = await _context.Favorites
    //         .FirstOrDefaultAsync(f => f.UserId == user.Id && f.GearInventory == gearInventory);

    //     if (favorite != null)
    //     {
    //         _context.Favorites.Remove(favorite);
    //         await _context.SaveChangesAsync();
    //     }

    //     return RedirectToAction("Index");
    // 
    }
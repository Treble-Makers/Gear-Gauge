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
            var favorites = await _context.Favorites
                .Where(f => f.UserId == user.Id)
                .Include(f => f.GearInventory)
                .ToListAsync();
            return View(favorites);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.GearInventoryId == id);

            if (existingFavorite == null)
            {
                var favorite = new Favorites
                {
                    UserId = user.Id,
                    GearInventoryId = id
                };
                _context.Favorites.Add(favorite);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Detail", "GearInventory", new { id = id });
        }

    [HttpPost]
    public async Task<IActionResult> RemoveFromFavorites(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.GearInventoryId == id);

        if (favorite != null)
        {
            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
}


//     [Authorize]
//     public class FavoritesController : Controller
//     {
//         private readonly GearGaugeDbContext _context;
//         private readonly UserManager<User> _userManager;

//         public FavoritesController(GearGaugeDbContext dbContext, UserManager<User> userManager)
//         {
//            _context = dbContext;
//             _userManager = userManager;
//         }

//         public async Task<IActionResult> List()
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return Unauthorized();
//             }

//             var favorites = await _context.Favorites
//                 .Where(f => f.UserId == user.Id)
//                 .Include(f => f.GearInventories)
//                 .ToListAsync();

//             return View(favorites);
//         }

//         [HttpPost]
//         public async Task<IActionResult> AddToFavorite(GearInventory gearInventories)
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return Unauthorized();
//             }

//             var existingFavorite = await _context.Favorites
//                 .FirstOrDefaultAsync(f => f.GearInventories == gearInventories && f.UserId == user.Id);
            
//             if (existingFavorite != null)
//             {
//                 return RedirectToAction("Detail", "GearInventory", new { id = gearInventories.Id }); 
//             }

//             var gear = await _context.Gear.FindAsync(gearInventories.Id); 
//             if (gear == null)
//             {
//                 return NotFound();
//             }

//             var favorite = new Favorites
//             {
//                 // GearId = gearId,
//                 UserId = user.Id,
//                 // Gear = gear
//             };

//             _context.Favorites.Add(favorite);
//             await _context.SaveChangesAsync();

//             return RedirectToAction("Detail", "GearInventory", new { id = gearInventories.Id });
//         }

//         [HttpPost]
//         public async Task<IActionResult> RemoveFromFavorite(GearInventory gearInventories)
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return Unauthorized();
//             }

//             var favorite = await _context.Favorites
//                 .FirstOrDefaultAsync(f => f.GearInventories == gearInventories && f.UserId == user.Id);

//             if (favorite != null)
//             {
//                 _context.Favorites.Remove(favorite);
//                 await _context.SaveChangesAsync();
//             }

//             return RedirectToAction("List");
//         }
//     }
// }
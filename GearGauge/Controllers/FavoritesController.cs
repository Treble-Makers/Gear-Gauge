using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GearGauge.Data;
using GearGauge.Models;
using Microsoft.AspNetCore.Authorization;

namespace GearGauge.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly GearGaugeDbContext context;
        private readonly UserManager<User> _userManager;

        public FavoritesController(GearGaugeDbContext dbContext, UserManager<User> userManager)
        {
           context = dbContext;
            _userManager = userManager;
        }
    [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int gearId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var favorite = await context.Favorites
                .FirstOrDefaultAsync(f => f.GearId == gearId && f.UserId == user.Id);

            if (favorite == null)
            {
                favorite = new Favorite
                {
                    GearId = gearId,
                    UserId = user.Id
                };
                context.Favorites.Add(favorite);
            }
            else
            {
                context.Favorites.Remove(favorite);
            }

            await context.SaveChangesAsync();

            return Json(new { isFavorited = favorite != null });
        }
        
        [HttpPost]
        public async Task<IActionResult> AddToFavorite(int gearId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var existingFavorite = await context.Favorites
            .FirstOrDefaultAsync(f => f.GearId == gearId && f.UserId == user.Id);
            
            if (existingFavorite != null)
            {
                return RedirectToAction("Detail", "GearInventory", new { id = gearId }); 
            }


            var favorite = new Favorite
            {
                GearId = gearId,
                UserId = user.Id
            };

            context.Favorites.Add(favorite);
            await context.SaveChangesAsync();

            return RedirectToAction("Detail", "GearInventory", new { id = gearId });
        }

        public async Task<IActionResult> List()
        {
            var user = await _userManager.GetUserAsync(User); // may need _signinManager.IsSignedIn(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var favorites = await context.Favorites
                .Where (f => f.UserId == user.Id)
                .Include(f => f.Gear)
                .ToListAsync();

            return View(favorites); // remove "Dtails"?
        }
         [HttpPost]
        public async Task<IActionResult> RemoveFromFavorite(int gearId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var favorite = await context.Favorites
                .FirstOrDefaultAsync(f => f.GearId == gearId && f.UserId == user.Id);

            if (favorite != null)
            {
                context.Favorites.Remove(favorite);
                await context.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }
    }

        // public async Task<IActionResult> GetFavoriteGearSummary()
        // {
        //     var user = await _userManager.GetUserAsync(User);
        //     if (user == null)
        //     {
        //         return Unauthorized();
        //     }

            // var favoriteItems = await context.Favorites
            //     .Where(f => f.UserId == user.Id)
            //     .Include(f => f.GearId) // Assuming Gear is the navigation property for the favorited item
            //     .ToListAsync();

            // return PartialView("_FavoriteGearSummary", favoriteItems);
        //}
}


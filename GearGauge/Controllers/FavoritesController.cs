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
        private readonly GearGaugeDbContext context;
        private readonly UserManager<User> _userManager;

        public FavoritesController(GearGaugeDbContext dbContext, UserManager<User> userManager)
        {
           context = dbContext;
            _userManager = userManager;
        }
    [HttpGet]
     public async Task<IActionResult> List(string userId)// was it list?
    {
          var favorites = await context.Favorites
                .FirstOrDefaultAsync(f => f.IsFavorite == false && f.UserId == userId);

        return View("List", favorites);
    }

    // [HttpPost]
    //     public async Task<IActionResult> ToggleFavorites(FavoritesListViewModel favoritesListViewModel)
    //     {
    //         var user = await _userManager.GetUserAsync(User);
    //         if (user == null) //might want to utilize signinmanager or usermanager for validation
    //         {
    //             return Unauthorized();
    //         }

    //         var favorites = await context.Favorites
    //             .FirstOrDefaultAsync(f => f.IsFavorite == false && f.UserId == user.Id);

    //         if (favorites == null) //isfav = false
    //         {
    //             favorites = new Favorites
    //             {
    //                 IsFavorite = true,
    //                 UserId = user.Id
                    
    //             };
    //             context.Favorites.Add(favorites);
    //         }
    //         // else 
    //         // {
    //         //     favorite = new Favorite
    //         //     {
    //         //         IsFavorite = false
    //         //     };
    //         //     context.Favorites.Add(favorite);
    //         // }

    //         await context.SaveChangesAsync();

    //         return View("List", favoritesListViewModel);
    //     }
        
    //     [HttpPost]
    //     public async Task<IActionResult> AddToFavorite(int gearId)
    //     {
    //         var user = await _userManager.GetUserAsync(User);
    //         if (user == null)
    //         {
    //             return Unauthorized();
    //         }

    //         var existingFavorites = await context.Favorites
    //         .FirstOrDefaultAsync(f => f.GearId == gearId && f.UserId == user.Id);
            
    //         if (existingFavorites != null)
    //         {
    //             return RedirectToAction("Detail", "GearInventory", new { id = gearId }); 
    //         }


    //         var favorite = new Favorites
    //         {
    //             GearId = gearId,
    //             UserId = user.Id
    //         };

    //         context.Favorites.Add(favorites); // changed from existingFavorites
    //         await context.SaveChangesAsync();

    //         return RedirectToAction("Detail", "GearInventory", new { id = gearId });
    //     }

        public async Task<IActionResult> List(FavoritesListViewModel favoritesListViewModel)
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

            return View("Details", favoritesListViewModel); // switched it from List to Details
        }
         [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(int gearId)
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
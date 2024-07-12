// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using GearGauge.Data;
// using GearGauge.Models;
// using Microsoft.AspNetCore.Authorization;

// namespace GearGauge.Controllers
// {
//     [Authorize]
//     public class FavoritesController : Controller
//     {
//         private readonly GearGaugeDbContext context;
//         private readonly UserManager<User> _userManager;

//         public FavoritesController(GearGaugeDbContext dbContext, UserManager<User> userManager)
//         {
//            context = dbContext;
//             _userManager = userManager;
//         }

//         [HttpPost]
//         public async Task<IActionResult> AddToFavorite(int gearId) // was (nt musicItemId)
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return Unauthorized();
//             }

//             var favorite = new Favorite
//             {
//                 GearId = gearId, //  MusicItemId = musicItemId,
//                 UserId = user.Id
//             };

//             context.Favorites.Add(favorite);
//             await context.SaveChangesAsync();

//             return RedirectToAction("Detail", "MusicItem", new { id = gearId });
//         }

//         public async Task<IActionResult> List()
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return Unauthorized();
//             }

//             var favorites = await context.Favorites
//                 .Where (f => f.UserId == user.Id)
//                 .Include(f => f.Gear) //was MusicItem or GearInventory
//                 .ToListAsync();

//             return View(favorites);
//         }
//     }
// }

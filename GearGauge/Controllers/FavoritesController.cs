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
        public async Task<IActionResult> AddToFavorite(GearInventory gearInventory) 
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            // var favorite = new Favorite
            // {
            //     GearInventory = gearInventory, 
            //     UserId = user.Id
            // };

            // context.Favorites.Add(favorite);
            // await context.SaveChangesAsync();

            return RedirectToAction("Detail", "GearInventory");
        }

        public async Task<IActionResult> List()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var favorites = await context.Favorites
                .Where (f => f.Id == user.Id)
                .Include(g => g.Id) 
                .ToListAsync();

            return View(favorites);
        }
    }
}

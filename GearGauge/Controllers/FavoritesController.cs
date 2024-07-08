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
        private readonly MusicItemDbContext _context;
        private readonly UserManager<User> _userManager;

        public FavoritesController(MusicItemDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorite(int musicItemId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            var favorite = new FavoriteItem
            {
                MusicItemId = musicItemId,
                UserId = user.Id
            };

            _context.Favorites.Add(favoriteitem);
            await _context.SaveChangesAsync();

            return RedirectToAction("Detail", "MusicItem", new { id = musicItemId });
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
                .Include(f => f.MusicItem)
                .ToListAsync();

            return View(favorites);
        }
    }
}

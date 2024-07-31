using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GearGauge.Data;
using GearGauge.Models;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GearGauge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly GearGaugeDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<WatchlistController> _logger;

        public WatchlistController(GearGaugeDbContext context, UserManager<User> userManager, ILogger<WatchlistController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: api/watchlist
        [HttpGet]
        public IActionResult GetWatchList()
        {
            var watchlist = _context.Watchlists
                .Include(w => w.GearInventory)
                .ToList();
            return Ok(watchlist);
        }

        [HttpGet("{id}")]
        public IActionResult GetWatchlistItem(int id)
        {
            var watchlist = _context.Watchlists
                .Include(w => w.GearInventory)
                .FirstOrDefault(w => w.WatchlistId == id);
            if (watchlist == null)
            {
                return NotFound();
            }
            return Ok(watchlist);
        }

        [Authorize] // Ensure only logged-in users can add to watchlist
        [HttpPost]
        public IActionResult AddToWatchlist([FromBody] WatchlistItemDto watchlistItemDto)
        {
            _logger.LogInformation("AddToWatchlist called with ItemId: {ItemId}", watchlistItemDto.ItemId);

            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var userId = _userManager.GetUserId(User); // Get the current user's identity

                    var watchlist = new Watchlist
                    {
                        UserId = userId,
                        GearInventoryId = watchlistItemDto.ItemId,
                        DateAdded = DateTime.Now
                    };

                    _context.Watchlists.Add(watchlist);
                    _context.SaveChanges();
                    _logger.LogInformation("Item added to watchlist for user: {UserId}", userId);
                    return Ok(new { success = true, message = "Added to watchlist" });
                }
                _logger.LogWarning("Invalid model state for AddToWatchlist: {ModelState}", ModelState);
                return BadRequest(new { success = false, message = "Invalid model state" });
            }
            _logger.LogWarning("Unauthorized attempt to add to watchlist");
            return Unauthorized(new { success = false, message = "Must be logged in to add items to watchlist" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateWatchlistItem(int id, [FromBody] Watchlist watchlist)
        {
            if (id != watchlist.WatchlistId)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                _context.Watchlists.Update(watchlist);
                _context.SaveChanges();
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteWatchlistItem(int id)
        {
            var watchlistItem = _context.Watchlists.Find(id);
            if (watchlistItem == null)
            {
                return NotFound();
            }
            _context.Watchlists.Remove(watchlistItem);
            _context.SaveChanges();
            return NoContent();
        }
    }

    public class WatchlistItemDto
    {
        public int ItemId { get; set; }
    }
}

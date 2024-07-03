using Microsoft.AspNetCore.Mvc;
using GearGauge.Data;
using GearGauge.Models;
using System.Linq;

namespace GearGauge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchlistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WatchlistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/watchlist
        [HttpGet]
        public IActionResult GetWatchList()
        {
            var watchlist = _context.Watchlists.ToList();
            return Ok(watchlist);
        }

        // GET: api/watchlist/{id}
        [HttpGet("{id}")]
        public IActionResult GetWatchlistItem(int id)
        {
            var watchlistItem = _context.Watchlists.Find(id);
            if (watchlistItem == null)
            {
                return NotFound();
            }
            return Ok(watchlistItem);
        }

        // POST: api/watchlist
        [HttpPost]
        public IActionResult AddToWatchlist([FromBody] Watchlist watchlist)
        {
            if (ModelState.IsValid)
            {
                _context.Watchlists.Add(watchlist);
                _context.SaveChanges();
                return CreatedAtAction(nameof(GetWatchlistItem), new { id = watchlist.WatchlistId }, watchlist);
            }
            return BadRequest(ModelState);
        }

        // PUT: api/watchlist/{id}
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

        // DELETE: api/watchlist/{id}
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
}

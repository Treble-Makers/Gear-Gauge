using Microsoft.AspNetCore.Mvc;
using GearGauge.ApplicationDbContext;
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

        //GET: api/watchlist
        public IActionResult GetWatchList()
        {
            var watchlist = _context.Watchlists.ToList();
            return Ok(watchlist);
        }
        [HttpGet("{id}")]
        public IActionResult GetWatchlistItem(int id)
        {
            var watchlist = _context.Watchlists.Find(id);
            if (watchlist == null)
            {
                return NotFound();
            }
            return Ok(watchlistItem);
        }
        [HttpPost]
        public IActionResult AddToWatchList([FromBody] Watchlist watchlist)
        {
            if (ModelState.IsValid)
            {
            _context.Watchlists.Add(watchlist);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetWatchlistItem), new { id = watchlist.Id }, watchlist);
            }
        return BadRequest(ModelState);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateWatchlistItem(int id, [FromBody] Watchlist watchlist)
        {
            if (id != watchlist.Id)
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
}
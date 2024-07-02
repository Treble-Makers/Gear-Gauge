using System;
using System.ComponentModel.DataAnnotations;

namespace GearGauge.Models
{
    public class Watchlist
    {
        [Key]
        public int WatchlistId { get; set; }
        public int UserId { get; set; }
        public int GearId { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual User User { get; set; }
        public virtual Gear Gear { get; set; }
    }
}


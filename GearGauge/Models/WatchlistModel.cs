using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearGauge.Models
{
    public class Watchlist
    {
        [Key]
        public int WatchlistId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("GearInventory")]
        public int GearInventoryId { get; set; }
        public virtual GearInventory GearInventory { get; set; }

        public DateTime DateAdded { get; set; }
        public string FormField { get; set; }
        public bool NotificationField { get; set; }
        public string LikedItems { get; set; }
        public bool RedExclamation { get; set; }
        public string Tag { get; set; }

        // Original properties retained
        public int GearId { get; set; } // This property might be redundant with GearInventoryId
        public virtual Gear Gear { get; set; } // This property might be redundant with GearInventory
    }
}


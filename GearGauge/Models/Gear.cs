using System.ComponentModel.DataAnnotations;

namespace GearGauge.Models
{
    public class Gear
    {
        [Key]
        public int GearId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Watchlist> Watchlists { get; set; }

    }
}



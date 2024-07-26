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
        public ICollection<Favorites> FavoriteGears { get; set; }

          public virtual Comment Comment { get; set; }

        public static implicit operator Gear(int v)
        {
            throw new NotImplementedException();
        }
    }
}



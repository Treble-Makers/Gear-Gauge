using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GearGauge.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<GearInventory> GearInventories { get; set; }

        public Tag()
        {
            GearInventories = new List<GearInventory>();
        }
    }
}


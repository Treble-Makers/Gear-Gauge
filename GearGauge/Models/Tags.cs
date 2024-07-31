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

        public ICollection<GearInventoryTag> GearInventoryTags { get; set; }

        public Tag()
        {
            GearInventoryTags = new List<GearInventoryTag>();
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GearGauge.Models
{
    public class GearInventory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MarketValue { get; set; }
        public string ImagePath { get; set; }
        public List<GearInventory> GearInventories { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public GearInventory()
        {
            Tags = new List<Tag>();
        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearGauge.Models
{
    public class GearInventory
    { 
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MarketValue { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
        public byte[]? Image { get; set; }
        public List<GearInventory> GearInventories { get; set; }
        public List<Gear> Gear { get; set; }

        public GearInventory() 
        {
            Tags = new List<GearInventoryTag>();
        }

        public GearInventory(int id, string title, string description, int marketValue)
        {
            Id = id;
            Title = title;
            Description = description;
            MarketValue = marketValue;
            Tags = new List<GearInventoryTag>();
        }

        public override string? ToString()
        {
            return Title;
        }

        public override bool Equals(object? obj)
        {
            return obj is GearInventory @gearInventory && Id == @gearInventory.Id;
        }

        public ICollection<GearInventoryTag> Tags { get; set; }
    }

    public class GearInventoryTag
    {
        public int GearInventoryId { get; set; }
        public GearInventory GearInventory { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GearGauge.Models;

  public class GearInventory
  { 
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int MarketValue { get; set; }
    public byte[]? Image { get; set; }
    public List<GearInventory> GearInventories { get; set; }
    public List<Gear> Gear { get; set; }

    public List<Favorite> Favorites { get; set; }
   // public int CommentId { get; set; }
   // public ICollection<Comment> Comments { get; set; } = new List<Comment>();
   // public Comment Comment { get; set; }

    public GearInventory() { }

    public GearInventory(int id, string title, string description, int marketValue)
    {
        Id = id;
        Title = title;
        Description = description;
        MarketValue = marketValue;
    }

    public override string? ToString()
    {
        return Title;
    }

    public override bool Equals(object? obj)
    {
        return obj is GearInventory @gearInventory && Id == @gearInventory.Id;
    }

    // public override int GetHashCode()

    

        public ICollection<Tag> Tags { get; set; }

        // public GearInventory()
        // {
        //     Tags = new List<Tag>();
        // }
    }


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
   // public string ImagePath { get; set; }
    public List<GearInventory> GearInventories { get; set; }
    public List<Gear> Gear { get; set; }
   // public byte[] Image { get; set; }
    [NotMapped]
   // public IFormFile ImageFile { get; set; }
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
   // public int CommentId { get; set; }
   // public ICollection<Comment> Comments { get; set; } = new List<Comment>();
   // public Comment Comment { get; set; }
   // public ICollection<GearInventory>? GearInventories { get; set; }

    public GearInventory() { }

    public GearInventory(string title, string description)
    {
        Title = title;
        Description = description;
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


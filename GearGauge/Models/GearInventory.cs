using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace GearGauge.Models;

  public class GearInventory
  { 
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int MarketValue { get; set; }
    [ForeignKey("User")]
    public string UserId { get; set; }
    public User User { get; set; }
    public byte[]? Image { get; set; }
    public List<GearInventory> GearInventories { get; set; }
  

    public GearInventory() { }

    public GearInventory(int id, string title, string description, int marketValue, User user, byte[]? image)
    {
        Id = id;
        Title = title;
        Description = description;
        MarketValue = marketValue;
        User = user;
        Image = image;
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

    

        // public ICollection<Tag> Tags { get; set; }

        // public GearInventory()
        // {
        //     Tags = new List<Tag>();
        // }
    }


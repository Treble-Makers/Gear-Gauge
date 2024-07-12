using System.ComponentModel.DataAnnotations.Schema;
using GearGauge.Models;
using Microsoft.Identity.Client;

public class GearInventoryViewModel
{
    public int Id {get; set;}
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MarketValue { get; set; }
    public int GearInventoryId { get; set; }
    public List<GearInventory> GearInventories { get; set; }
  //  public string ImagePath { get; set; } 
    [NotMapped]
   // public IFormFile ImageFile { get; set; }

    // Navigation property for tags
    public ICollection<Tag> Tags { get; set; }

    public GearInventoryViewModel()
    {
        Tags = new List<Tag>();
    }

    public override string ToString()
    {
        return Title;
    }

}


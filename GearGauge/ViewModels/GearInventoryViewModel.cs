using System;
using GearGauge.Models;

namespace GearGauge.ViewModels;

public class GearInventoryViewModel
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MarketValue { get; set; }
    

    public GearInventoryViewModel(GearInventory theGearInventories)
    {
        Id = theGearInventories.Id;
        Title = theGearInventories.Title;
        Description = theGearInventories.Description;
        MarketValue = theGearInventories.MarketValue;
       
    }
}

using System;
using GearGauge.Models;

namespace GearGauge.ViewModels;

public class MusicItemViewModel
{
    public int MusicItemId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MarketValue { get; set; }
    public string? MusicItemCategory { get; set; }

    public MusicItemViewModel(MusicItem theMusicItems)
    {
        MusicItemId = theMusicItems.Id;
        Title = theMusicItems.Title;
        Description = theMusicItems.Description;
        MarketValue = theMusicItems.MarketValue;
       
    }
}
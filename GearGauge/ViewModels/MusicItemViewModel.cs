using System;
using GearGauge.Models;

namespace GearGauge.ViewModels;

public class MusicItemViewModel
{
    public int MusicItemId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? MusicItemCategory { get; set; }

    public MusicItemViewModel(MusicItem theMusicItem)
    {
        MusicItemId = theMusicItem.Id;
        Title = theMusicItem.Title;
        Description = theMusicItem.Description;
    }
}

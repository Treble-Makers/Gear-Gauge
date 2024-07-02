using System;
using System.ComponentModel.DataAnnotations;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace GearGauge.ViewModels;

public class AddMusicItemViewModel
{
    [Required(ErrorMessage = "Instrument title is required.")]
    public string? Title { get; set; }

    public string? Description { get; set; }
    public int MarketValue { get; set; }
  

    public string? MusicItemCategory {get; set; }

    public int CategoryId { get; set; }
    public List<SelectListItem>? Categories { get; set; }

    public AddMusicItemViewModel (List<MusicItem> categories) {
        Categories = new List<SelectListItem>();

        foreach (var category in categories)
        {
            Categories.Add(
                new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.Title,
                    


                });
        }
    }

    public AddMusicItemViewModel ()
    {

    }

}

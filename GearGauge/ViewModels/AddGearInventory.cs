using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GearGauge.Models;

namespace GearGauge.ViewModels
{
    public class AddGearInventoryViewModel
    {
        [Required(ErrorMessage = "Instrument title is required.")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Market value must be a positive number.")]
        public int MarketValue { get; set; }
        public int GearInventoryId { get; set;}

        public List<SelectListItem> GearInventories { get; set; }

        [Display(Name = "Select Tags")]
        public List<int> SelectedTagIds { get; set; }  // New property to hold selected tag IDs

        public List<SelectListItem> AvailableTags { get; set; }  // New property to hold available tags for selection

        public AddGearInventoryViewModel()
        {
            GearInventories = new List<SelectListItem>();
            AvailableTags = new List<SelectListItem>();
            SelectedTagIds = new List<int>();
        }

        public AddGearInventoryViewModel(List<GearInventory> gearInventories, List<Tag> tags)
            : this()
        {
            // foreach (var gearInventory in gearInventories)
            // {
            //     GearInventories.Add(new SelectListItem
            //     {
            //         Value = gearInventory.Id.ToString(),
            //         Text = gearInventory.Title
            //     });
            // }

            foreach (var tag in tags)
            {
                AvailableTags.Add(new SelectListItem
                {
                    Value = tag.Id.ToString(), 
                    Text = tag.Name
                });
            }
        }
    }
}

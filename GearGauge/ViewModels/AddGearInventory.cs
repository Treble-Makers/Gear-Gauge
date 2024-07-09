using System;
using System.ComponentModel.DataAnnotations;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GearGauge.ViewModels
{
    public class AddGearInventoryViewModel
    {
        [Required(ErrorMessage = "Instrument title is required.")]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int MarketValue { get; set; }
        public List<SelectListItem>? GearInventories { get; set; }

        public AddGearInventoryViewModel()
        {
            GearInventories = new List<SelectListItem>();
        }

        public AddGearInventoryViewModel(List<GearInventory> gearInventories)
        {
            GearInventories = new List<SelectListItem>();

            foreach (var gearInventory in gearInventories)
            {
                GearInventories.Add(
                    new SelectListItem
                    {
                        Value = gearInventory.Id.ToString(),
                        Text = gearInventory.Title
                    }
                );
            }
        }
    }
}

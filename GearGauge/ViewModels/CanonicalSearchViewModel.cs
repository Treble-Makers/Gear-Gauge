using System;
using System.ComponentModel.DataAnnotations;
using GearGauge.Models;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace GearGauge.ViewModels;

public class CanonicalSearchViewModel
{
    //public string _id { get; set; }
    // public string? Title { get; set; }

    // public List<string> Finishes { get; set; }
    // public List<string>? CanonicalProductIds { get; set; }
    // public CoreApimessagesImage? Image { get; set; }
    //public string Slug { get; set; }
    // public Brand? Brand { get; set; }
    //public string __typename { get; set; }
    public string? Title { get; set; }

    public List<string>? CanonicalProductIds { get; set; }

	public CoreApimessagesImage? Image { get; set; }    

    public PriceGuideResponse? PriceGuideResponse { get; set; }

    public string? PriceMiddle { get; set; }
    public string? PriceMiddleThirtyDaysAgo { get; set; }

    public VideoDetails? Videos { get; set;}
    
    public class AddGearInventoryViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MarketValue { get; set; }
        public List<int> SelectedTagIds { get; set; }
        public List<CanonicalSearchViewModel> CanonicalSearchItems { get; set; }
        public List<SelectListItem> AvailableTags { get; set; }
        public IFormFile ImageFile { get; set; } // If you are handling file uploads
    }
    // public Video? VideoTitle { get; set; }
    // public Video? VideoId { get; set; }
}

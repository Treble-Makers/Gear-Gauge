using GearGauge.Models;
using GearGauge.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GearGauge.ViewModels;

public class PriceRecommendationViewModel 
{
    public string? Title { get; set; }

    public List<string>? CanonicalProductIds { get; set; }

	public CoreApimessagesImage? Image { get; set; }    

    public PriceGuideResponse? PriceGuideResponse { get; set; }

    public PriceRecommendationsResponse? PriceMiddle { get; set; }
    public PriceRecommendationsResponse? PriceMiddleThirtyDaysAgo { get; set; }

    public VideoDetails? videoDetails { get; set;}
}
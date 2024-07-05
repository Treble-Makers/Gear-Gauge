using GearGauge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GearGauge.ViewModels;

public class PriceRecommendationViewModel 
{
    public string? Title { get; set; }

    public int CanonicalProductId { get; set; }

	public CoreApimessagesImage? Image { get; set; }    

    public PriceGuideResponse? PriceGuideResponse { get; set; }

    public PriceRecommendation? PriceMiddle { get; set; }
}
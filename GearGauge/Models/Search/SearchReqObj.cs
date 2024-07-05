using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.Models;

public class SearchRequestObject
{
    public string? operationName { get; set; }
    public SearchRequestVariables? variables { get; set; }
    public string? query { get; set; }
}

public class SearchRequestVariables
{
    public int? offset { get; set; }
    public int? sellCardLimit { get; set; }
    public string? q { get; set; }
    public string?[] excludedCategoryUuids { get; set; }
    public string? fullTextQueryOperand { get; set; }
    public string? sort { get; set; }
    public bool? fuzzy { get; set; }
    public string? listingsThatShipTo { get; set; }
    public bool? hasExpressSaleBid { get; set; }
    public bool? includePriceRecommendations { get; set; }
}

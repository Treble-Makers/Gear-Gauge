using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;


namespace GearGauge.Models;

public class GearImage
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string? ImageAltDescription { get; set; }
    public string? ImageFileName { get; set; }
    public IFormFile? ImageFile { get; set; }

    public GearImage()
    {
    }

    public override bool Equals(object? obj)
    {
        return obj is GearImage @gearImage &&
            Id == @gearImage.Id;
       
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }


}

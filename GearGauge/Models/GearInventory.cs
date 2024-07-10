﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GearGauge.Models;

public class GearInventory
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MarketValue { get; set; }
    public byte[] Image { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
  

    public ICollection<GearInventory>? GearInventories { get; set; }

    public GearInventory() { }

    public GearInventory(string title, string description)
    {
        Title = title;
        Description = description;
     

    }

    public override string? ToString()
    {
        return Title;
    }

    public override bool Equals(object? obj)
    {
        return obj is GearInventory @gearInventory && Id == @gearInventory.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}

using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;

namespace GearGauge.Models;

public class Favorite
{
    public int Id { get; set; } // changed to int
    public int GearId { get; set;}
    public string UserId { get; set; }
    public Gear Gear { get; set; } 
    public User User { get; set; }
    public GearInventory GearInventory { get; set; } 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
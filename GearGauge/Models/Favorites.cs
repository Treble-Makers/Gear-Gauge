using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using GearGauge.ViewModels;

namespace GearGauge.Models;

public class Favorites 
{
    public int Id { get; set; } 
    public string UserId { get; set; }
   // public int GearInventoryId { get; set; }
    public GearInventory? GearInventory { get; set; }
    public User? User { get; set; }
    //public GearInventory? GearInventories { get; set; }  no longer the version needed
}
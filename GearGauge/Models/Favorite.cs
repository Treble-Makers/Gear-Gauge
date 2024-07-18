using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;

namespace GearGauge.Models;

public class Favorite
{
    public string Id { get; set; }
    public GearInventory GearInventory { get; set; } 
    
    public int UserId { get; set; }
    public User User { get; set; }
    //public Gear Gear { get; set; } 

    public int GearId { get; set;}
  
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}
        


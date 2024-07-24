using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using GearGauge.ViewModels;

namespace GearGauge.Models;

public class Favorites 
{
    public int Id { get; set; } // changed to int
    // public bool IsFavorite { get; set; } = false;
    // public int GearId { get; set;} //Do we have gear Ids? Maybe canonicalSearchViewModel ID
    // public Gear Gear { get; set; } 

    // public CanonicalSearchViewModel? Title { get; set; }

    public string UserId { get; set; } // Changed to string to match IdentityUser.Id
    // public User User { get; set; }
    public GearInventory? GearInventories { get; set; } 
    // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
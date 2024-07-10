using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;

namespace GearGauge.Models;

public class FavoriteItem
{
    public User User { get; set; }
    public int UserId { get; set; }
    public Gear GearId { get; set; } 

    public int Id { get; set; } // was MusicItemId
    public GearInventory GearInventory { get; set; } // does this make sense?
    public string? UserName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

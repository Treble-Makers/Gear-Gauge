using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;

namespace GearGauge.Models;

public class FavoriteItem
{
    public int UserId { get; set; }

    public int MusicItemId { get; set; }
    public GearInventory GearInventory { get; set; } // does this make sense?
   // walk through code
    public string? UserName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

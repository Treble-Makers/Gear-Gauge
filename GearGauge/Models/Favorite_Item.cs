using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;

namespace GearGauge.Models;

public class FavoriteItem
{
    public int UserId { get; set; }

    public int MusicItemId { get; set; }
    public MusicItem MusicItem { get; set; } 
   
    public string? UserName { get; set; }
    //public User User { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

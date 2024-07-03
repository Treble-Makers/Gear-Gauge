using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Web;

namespace GearGauge.Models;

public class FavoriteItem
{
    public int Id { get; set; }
    
    public int UserId { get; set; }

    public string? UserName { get; set; }

}

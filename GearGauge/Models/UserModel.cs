using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.Models;

public class User : IdentityUser
{
    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(200)]
    public string? Address { get; set; }

    [StringLength(500)]
    public string? AboutMe { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public ICollection<Favorites> FavoriteGears { get; set; }

    public User() : base()
    {
        FavoriteGears = new List<Favorites>();
    }
}
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
    public GearInventory gearInventory { get; set; }
    public User()
    {
        
    }
    public User(string userName, string email, string name, string address) : base()
    {
        FavoriteGears = new List<Favorites>();
        UserName = userName;
        Email = email;
        Name = name;
        Address = address;
    }
}


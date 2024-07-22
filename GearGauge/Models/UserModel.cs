using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.Models;

public class User : IdentityUser
{
    public string Id { get; set; } // changed to string to fix favoritecontroller
    public string? Name { get; set; }

    public string? Address { get; set; }  // should this be ContactEmail instead? do we need address?
    //public string? Bio { get; set; } 
    public List<int>? GearId { get; set; } 
    public string? ProfilePictureUrl { get; set; } // TG added this for profile
    public ICollection<Favorites> Favorites { get; set; } = new List<Favorites>(); // Favorite feature

    public User(string userName, string email, string name, string address)
        : base()
    {
        // UserName = userName;
        UserName = userName;
        Email = email;
        Name = name;
        Address = address;
        //Bio = bio;
       
    }

    public User()
    {
    }
}

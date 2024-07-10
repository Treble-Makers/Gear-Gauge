using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.Models;

public class User : IdentityUser
{
    // public int Id { get; set; }
    public string? Name { get; set; }

    public string? Address { get; set; }
    public List<int>? MusicItemIds { get; set; }
    public string? ProfilePictureUrl { get; set; } // TG added this for profile
    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>(); // Favorite feature

    // public User() : base() { }

    public User(string userName, string email, string name, string address)
        : base(userName)
    {
        UserName = userName;
        Email = email;
        Name = name;
        Address = address;
        ProfilePictureUrl = profilePictureUrl;
        // MusicItemIds = musicItemIds;
    }
}

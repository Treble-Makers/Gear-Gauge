using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.Models;

public class User : IdentityUser
{
    // public int Id { get; set; }
    public string? Name { get; set; }

    public string? Address { get; set; }
    // public List<int>? GearId { get; set; }

    public User() : base() { }

    // public User(string email, string name, string address)
    //     : base()
    // {
    //     // UserName = userName;
    //     Email = email;
    //     Name = name;
    //     Address = address;
    //     // MusicItemIds = musicItemIds;
    // }
}

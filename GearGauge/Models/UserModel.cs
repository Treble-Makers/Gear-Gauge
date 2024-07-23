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

// using System;
// using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Identity;

// namespace GearGauge.Models;

// public class User : IdentityUser
// {
//     // Remove the Id property, as it's already defined in IdentityUser
//     public string? Name { get; set; }

//     public string? Address { get; set; }  // should this be ContactEmail instead? do we need address?
//     //public string? Bio { get; set; } 
//     // public List<int>? GearId { get; set; } 
//     public string? ProfilePictureUrl { get; set; } // TG added this for profile
//     public ICollection<Favorites> FavoriteGears { get; set; }  // Favorite feature
    
//     [StringLength(500)]
//     public string? AboutMe { get; set; }

//     public User(string userName, string email, string name, string address)
//         : base()
//     {
//         UserName = userName;
//         Email = email;
//         Name = name;
//         Address = address;
//         //Bio = bio;
//     }

//     public User()
//     {
//     }
// }
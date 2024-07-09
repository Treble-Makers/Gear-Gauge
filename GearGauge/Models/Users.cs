using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GearGauge.Models
{
    public class User : IdentityUser
    {
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<int>? MusicItemIds { get; set; }
        public string? ProfilePictureUrl { get; set; } // Add this line

        public User(string userName, string email, string name, string address, string profilePictureUrl = null)
            : base(userName)
        {
            UserName = userName;
            Email = email;
            Name = name;
            Address = address;
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}

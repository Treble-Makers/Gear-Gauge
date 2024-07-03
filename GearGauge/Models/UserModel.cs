using System;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.Models;

public class User : IdentityUser<Guid>
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    // public int MusicItemId { get; set; }


    public User() : base() { }

    public User(string userName, string firstName, string lastName)
        : base(userName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
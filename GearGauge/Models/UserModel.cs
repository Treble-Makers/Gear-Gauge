using System;
using Microsoft.AspNetCore.Identity;

namespace GearGauge.Models;

public class User : IdentityUser
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }


    public User() : base() { }

    public User(string userName, string email, string firstName, string lastName)
        : base(userName)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
    }
}
using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GearGauge.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Username is required.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "The email field is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "The password field is required.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match!")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }

    [Required]
    public string? Name { get; set; }

    [DataType(DataType.MultilineText)]
    public string? Address { get; set; }
    public List<int>? MusicItemIds { get; set; }
}
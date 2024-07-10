using System.ComponentModel.DataAnnotations;
﻿using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GearGauge.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "The email field is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }


    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}

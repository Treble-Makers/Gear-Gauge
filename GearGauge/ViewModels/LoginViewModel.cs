using System.ComponentModel.DataAnnotations;
﻿using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GearGauge.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Username is required.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "The password field is required.")]
    [DataType(DataType.Password)]

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}

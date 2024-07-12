using System.ComponentModel.DataAnnotations;
﻿using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GearGauge.ViewModels;

   public class ContactUsViewModel
   {
      public int Id { get; set; }
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Please enter a description for your event.")]
    [StringLength(500, ErrorMessage = "Description is too long!")]
    public string? MessageBody { get; set; }
    
    [EmailAddress]
    public string? ContactEmail { get; set; }
   // public User? User { get; set; }
   
   
}
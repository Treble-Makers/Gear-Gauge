using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearGauge.Models;

namespace GearGauge.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }
        
        public string UserDescription { get; set; }
    }
}
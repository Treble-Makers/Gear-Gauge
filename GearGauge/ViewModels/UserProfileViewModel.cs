using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GearGauge.ViewModels
{
    public class UserProfileViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(100)]
        public string? Name { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [Display(Name = "About Me")]
        [StringLength(500)]
        public string? AboutMe { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile? ProfilePicture { get; set; }

        public string? CurrentProfilePictureUrl { get; set; }
        public bool IsEditMode { get; set; }
    }
}
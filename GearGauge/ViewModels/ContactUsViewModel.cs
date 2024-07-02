using System.ComponentModel.DataAnnotations;
namespace GearGauge.ViewModels
{
   public class ContactUsViewModel
   {
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters.")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Please enter a description for your event.")]
    [StringLength(500, ErrorMessage = "Description is too long!")]
    public string? MessageBody { get; set; }
    
    [EmailAddress]
    public string? ContactEmail { get; set; }
   }
}
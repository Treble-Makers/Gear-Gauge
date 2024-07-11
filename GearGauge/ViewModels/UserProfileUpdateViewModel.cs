using System.ComponentModel.DataAnnotations;

public class UserProfileUpdateViewModel
{
    [Required]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    public IFormFile ProfilePicture { get; set; }
}

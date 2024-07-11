using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearGauge.Models;

namespace GearGauge.ViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public string? UserName { get; set; }

        public string? Comment { get; set; } // is using Comment uniform in the other cs?

        [Required]
        public int Id { get; set; }

    }

}

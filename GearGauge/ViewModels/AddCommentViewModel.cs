using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearGauge.Models;

namespace GearGauge.ViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public string? UserName { get; set; }

        public string? Comment { get; set; }

        [Required]
        public int Id { get; set; }

    }

}

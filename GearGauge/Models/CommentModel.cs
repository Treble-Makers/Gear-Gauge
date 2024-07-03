using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearGauge.Models;

namespace GearGauge.ViewModels
{
    public class Comment
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public int Id { get; set; }
        [Required]
        public string? CommentText { get; set; }

    }

}

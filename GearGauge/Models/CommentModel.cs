using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearGauge.Models;

namespace GearGauge.ViewModels
{
    public class Comment
    {
        [Required]
        public string? UserName { get; set; }

        public string? CommentText { get; set; }

        [Required]
        public int MusicItemIds { get; set; }

    }

}

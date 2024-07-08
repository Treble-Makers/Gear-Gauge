using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearGauge.Models;

namespace GearGauge.Models
{
    public class Comment
    {
        // so we need their username, the item's ID, the comment and when it was written... what else?
        [Required]
        public string? UserName { get; set; } 
        [Required]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CommentText { get; set; }
        public MusicItem MusicItem { get; set; }

        public Comment()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }

}

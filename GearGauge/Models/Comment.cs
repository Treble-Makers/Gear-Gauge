using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearGauge.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace GearGauge.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        public ICollection<Comment> Replies { get; set; }
        
    }

}
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
        //public string? UserName { get; set; } // should relate to a specific user -- should this be the email?
        //public int GearId { get; set; }

        // Navigation property for the one-to-one relationship with Gear
        // [ForeignKey("GearId")]
        //public virtual Gear Gear { get; set; }
    }

}

//  [Required]
//         public string? UserName { get; set; } // should relate to a specific user -- should this be the email?
//         [Required]
//         public User User { get; set; }
//         public int Id { get; set; }
//         public int GearId { get; set; }

//         // Navigation property for the one-to-one relationship with Gear
//         // [ForeignKey("GearId")]
//         public virtual Gear Gear { get; set; }
//         public string Content { get; set; }
//         public DateTime CreatedAt { get; set; }

//        //public Gear Gear { get; set; } // attaching comment to a specific music item. Might need to be more specific, ie. attached to a specific music item id

//         public Comment()
//         {
//             CreatedAt = DateTime.UtcNow;
//         }
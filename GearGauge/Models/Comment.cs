// using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using GearGauge.Models;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace GearGauge.Models
// {
//     public class Comment
//     {
//         // so we need their username, the item's ID, the comment and when it was written... what else?
//         [Required]
//         public string? UserName { get; set; } // should relate to a specific user -- should this be the email?
//         [Required]
//         public User User { get; set; }
//         public int Id { get; set; }
//         public int GearId { get; set; }

//         // Navigation property for the one-to-one relationship with Gear
//         [ForeignKey("GearId")]
//         public virtual Gear Gear { get; set; }
//         public string Content { get; set; }
//         public DateTime CreatedAt { get; set; }

//        //public Gear Gear { get; set; } // attaching comment to a specific music item. Might need to be more specific, ie. attached to a specific music item id

//         public Comment()
//         {
//             CreatedAt = DateTime.UtcNow;
//         }
//     }

// }

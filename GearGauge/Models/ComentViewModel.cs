using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearGauge.Models;

namespace GearGauge.ViewModels
{
    public class CommentViewModel
    {
     [Required]
    public string? UserName { get; set; }

    public string? Comment { get; set; }
     [Required]
    public int ItemId { get; set; }

    // a reference to the exact item the comment is for. There can only be one?
   
   // anything else?

    }

}

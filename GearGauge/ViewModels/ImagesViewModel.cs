using System;
using System.ComponentModel;
using GearGauge.Models;


namespace GearGauge.ViewModels;

public class ImagesViewModel
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    [DisplayName("Image Alt Description")]
    public string? ImageAltDescription { get; set; }
    [DisplayName("Image File Name")]
    public string? ImageFileName { get; set; }
    [DisplayName("Upload File")]
    public IFormFile? ImageFile { get; set; }

}



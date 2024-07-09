using System;
using System.ComponentModel.DataAnnotations;
using GearGauge.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace GearGauge.ViewModels;

public class CanonicalSearchViewModel
{
    //public string _id { get; set; }
    public string? Title { get; set; }

    // public List<string> Finishes { get; set; }
    public List<string>? CanonicalProductIds { get; set; }
    public CoreApimessagesImage? Image { get; set; }
    //public string Slug { get; set; }
    // public Brand? Brand { get; set; }
    //public string __typename { get; set; }
}

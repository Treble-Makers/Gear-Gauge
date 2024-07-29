using System;
using System.ComponentModel.DataAnnotations;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace GearGauge.ViewModels;

public class FavoritesListViewModel
{
    public Favorites? Id { get; set; }

    public User? User { get; set; }
    public string? UserId { get; }

}
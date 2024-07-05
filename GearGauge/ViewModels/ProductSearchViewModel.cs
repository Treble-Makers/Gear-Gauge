using System.ComponentModel.DataAnnotations;
﻿using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace GearGauge.ViewModels;

public class ProductSearchViewModel
{
    public string? SearchTerm { get; set; }
}
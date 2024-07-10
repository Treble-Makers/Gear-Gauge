using System.ComponentModel.DataAnnotations;
﻿using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GearGauge.ViewModels
{
    public class YouTubeSearchViewModel
    {
        public string? SearchTerm { get; set; }

        public string? apiConnectionKey { get; set; }
    }
}
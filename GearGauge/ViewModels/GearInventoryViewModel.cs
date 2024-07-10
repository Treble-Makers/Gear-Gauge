using System.ComponentModel.DataAnnotations;
﻿using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GearGauge.ViewModels;

public class GearInventoryViewModel
{
    
    public int GearInventoryId { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public int MarketValue { get; set; }
    
public GearInventoryViewModel (GearInventory theGearInventory) 
{
       GearInventoryId = theGearInventory.Id;
       Title = theGearInventory.Title;
       Description = theGearInventory.Description;
       MarketValue = theGearInventory.MarketValue;

        }
    }
  


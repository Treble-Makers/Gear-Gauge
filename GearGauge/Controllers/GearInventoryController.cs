using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GearGauge.ViewModels;
using GearGauge.Data;

namespace GearGauge.Controllers;

public class GearInventoryController : Controller
{
    private GearGaugeDbContext context;

    public GearInventoryController(GearGaugeDbContext dbContext)
    {
        context = dbContext;
    }

    public IActionResult Index()
    {
        List<GearInventory> gearInventories = context.GearInventory.ToList();

        return View(gearInventories);
    }

    [HttpGet]
    public IActionResult Add()
    {
        AddGearInventoryViewModel addGearInventoryViewModel  = new AddGearInventoryViewModel(context.GearInventories.ToList());
        return View(addGearInventoryViewModel);
    }

    [HttpPost]
    [Route ("/GearInventory/Add")]
    public IActionResult Add(AddGearInventoryViewModel addGearInventoryViewModel)
    {
        if (ModelState.IsValid)
        {
            
            GearInventory newGearInventory = new GearInventory
            {
                Title = addGearInventoryViewModel.Title,
                Description = addGearInventoryViewModel.Description,
                MarketValue = addGearInventoryViewModel.MarketValue,
                
            };

            context.MusicItems.Add(newGearInventory);
            context.SaveChanges();

            return Redirect("/GearInventory");
        }
        return View(addGearInventoryViewModel);
    }
    public IActionResult Delete()
    {
        ViewBag.gearInventories = context.GearInventories.ToList();

        return View();
    }
    [HttpPost]
    public IActionResult Delete(int[] musicItemIds)
    {
        foreach (int musicItemId in musicItemIds)
        {
            MusicItem? theMusicItem = context.MusicItems.Find(musicItemId);
            context.MusicItems.Remove(theMusicItem);
        }
        context.SaveChanges();
        return Redirect("/GearInventory");
    }

    public IActionResult Detail(int id)
    {
        GearInventory theGearInventory = context.GearInventories
            .Single(m => m.Id == id);

        GearInventoryViewModel viewModel = new GearInventoryViewModel(theGearInventory);
        return View(viewModel);
    }
}

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
    private readonly GearGaugeDbContext context;

    public GearInventoryController(GearGaugeDbContext dbContext)
    {
        context = dbContext;
    }

    public IActionResult Index()
    {
        List<GearInventory> gearInventories = context.GearInventories.ToList();

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

            context.GearInventories.Add(newGearInventory);
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
    public IActionResult Delete(int[] Ids)
    {
        foreach (int Id in Ids)
        {
            GearInventory? theGearInventory = context.GearInventories.Find(Id);
            context.GearInventories.Remove(theGearInventory);
        }
        context.SaveChanges();
        return Redirect("/GearInventories");
    }

    public IActionResult Detail(int id)
    {
        GearInventory theGearInventory = context.GearInventories
            .Single(g => g.Id == id);

        GearInventoryViewModel viewModel = new GearInventoryViewModel(theGearInventory);
        return View(viewModel);
    }
}

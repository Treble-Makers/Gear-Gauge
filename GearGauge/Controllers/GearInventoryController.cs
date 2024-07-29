using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GearGauge.Controllers;
using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
        List<GearInventory> gearInventoryList = context.GearInventories.ToList();
        return View(gearInventoryList);
    }

    [HttpGet]
    public IActionResult Add()
    {
        var gearInventories = context.GearInventories.ToList();

        //var tags = context.Tags.ToList();
        GearInventory viewModel = new GearInventory();
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Detail(int Id)
    {
        GearInventory? gearInventory = context.GearInventories.SingleOrDefault(a => a.Id == Id);
        if (gearInventory != null)
        {
            return View("Detail", gearInventory);
        }
        return View("Index");
    }

    public IActionResult Edit(int id)
    {
        GearInventory theGearInventory = context.GearInventories.Find(id);
        if (theGearInventory != null)
        {
            return View(theGearInventory);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("/GearInventory/Add")]
    public IActionResult Add(AddGearInventoryViewModel addGearInventoryViewModel, IFormFile Image)
    {
        if (ModelState.IsValid)
        {
            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    Image.CopyTo(memoryStream);
                    addGearInventoryViewModel.Image = memoryStream.ToArray();
                }
            }

            GearInventory newGearInventory =
                new()
                {
                    Title = addGearInventoryViewModel.Title,
                    Description = addGearInventoryViewModel.Description,
                    MarketValue = addGearInventoryViewModel.MarketValue,
                    Image = addGearInventoryViewModel.Image
                };

            if (addGearInventoryViewModel.SelectedTagIds != null)
            {
                foreach (var tagId in addGearInventoryViewModel.SelectedTagIds)
                {
                    var tag = context.Tags.Find(tagId);
                    if (tag != null)
                    {
                        //newGearInventory.Tags.Add(tagId);
                    }
                }
            }

            context.GearInventories.Add(newGearInventory);

            context.SaveChanges();

            return Redirect("/GearInventory");
        }

        addGearInventoryViewModel.AvailableTags = context
            .Tags.ToList()
            .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name })
            .ToList();

        return View(addGearInventoryViewModel);
    }

    [HttpPost]
    public IActionResult Edit(GearInventory gearInventory, IFormFile Image)
    {
        if (ModelState.IsValid)
        {
            if (Image != null && Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    Image.CopyTo(memoryStream);
                    gearInventory.Image = memoryStream.ToArray();
                }
            }
            else
            {
                var existingGearInventory = context
                    .GearInventories.AsNoTracking()
                    .FirstOrDefault(g => g.Id == gearInventory.Id);
                if (existingGearInventory != null)
                {
                    gearInventory.Image = existingGearInventory.Image;
                }
            }
            context.GearInventories.Update(gearInventory);
            context.SaveChanges();
            
            return Redirect("/Detail");
        }
        return View(gearInventory);
    }

    public IActionResult Delete()
    {
        ViewBag.gearInventories = context.GearInventories.ToList();
        return View();
    }

        [HttpPost("GearInventory/Delete")]
        public IActionResult Delete(GearInventoryViewModel gearInventoryViewModel)
       
        {
                GearInventory theGearInventory = context.GearInventories.Find(gearInventoryViewModel.Id);
                Console.WriteLine("Found");
                if (theGearInventory != null)
                {
                    context.GearInventories.Remove(theGearInventory);
                }
            
            context.SaveChanges();
            return Redirect("/GearInventory");
        }

     public async Task<IActionResult> Details(int id) // added this for my favorites
{
    var gearInventory = await context.GearInventories.FindAsync(id);
    if (gearInventory == null)
    {
        return NotFound();
    }
    return View(gearInventory);
}
    }
}

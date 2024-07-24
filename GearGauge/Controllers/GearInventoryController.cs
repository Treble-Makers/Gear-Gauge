using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using GearGauge.Controllers;

namespace GearGauge.Controllers
{
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

        [HttpPost]
        [Route("/GearInventory/Add")]
        public IActionResult Add(AddGearInventoryViewModel addGearInventoryViewModel, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                      if (Image != null && Image.Length > 0)
                {
                    using(var memoryStream = new MemoryStream())
                    {
                        Image.CopyTo(memoryStream);
                        addGearInventoryViewModel.Image = memoryStream.ToArray();
                    }
                }
             

                GearInventory newGearInventory = new()
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

            addGearInventoryViewModel.AvailableTags = context.Tags.ToList()
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();

            return View(addGearInventoryViewModel);
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

     
    }
}

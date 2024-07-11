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
            List<GearInventory> gearInventories = context.GearInventories.Include(g => g.Tags).ToList();
            return View(gearInventories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var gearInventories = context.GearInventories.ToList();
            var tags = context.Tags.ToList();
            AddGearInventoryViewModel viewModel = new AddGearInventoryViewModel(gearInventories, tags);
            return View(viewModel);
        }

        [HttpPost]
        [Route("/GearInventory/Add")]
        public IActionResult Add(AddGearInventoryViewModel addGearInventoryViewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (addGearInventoryViewModel.ImageFile != null && addGearInventoryViewModel.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine("wwwroot", "images");
                    Directory.CreateDirectory(uploadsFolder);

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + addGearInventoryViewModel.ImageFile.FileName;

                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        addGearInventoryViewModel.ImageFile.CopyTo(fileStream);
                    }

                    uniqueFileName = "/images/" + uniqueFileName;
                }

                GearInventory newGearInventory = new()
                {
                    Title = addGearInventoryViewModel.Title,
                    Description = addGearInventoryViewModel.Description,
                    MarketValue = addGearInventoryViewModel.MarketValue,
                    ImagePath = uniqueFileName
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

        [HttpPost]
        public IActionResult Delete(int[] Ids)
        {
            foreach (int Id in Ids)
            {
                GearInventory theGearInventory = context.GearInventories.Find(Id);
                context.GearInventories.Remove(theGearInventory);
            }
            context.SaveChanges();
            return Redirect("/GearInventory");
        }

        public IActionResult Detail(int id)
        {
            GearInventory theGearInventory = context.GearInventories
                .Include(g => g.Tags)
                .Single(g => g.Id == id);

            return View(theGearInventory);
        }
    }
}

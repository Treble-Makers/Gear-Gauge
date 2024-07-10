using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;  // Add this line

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

                // Check if an image file is uploaded
                if (addGearInventoryViewModel.ImageFile != null && addGearInventoryViewModel.ImageFile.Length > 0)
                {
                    // Define the uploads directory and ensure it exists
                    string uploadsFolder = Path.Combine("images");
                    Directory.CreateDirectory(uploadsFolder);

                    // Generate a unique file name to avoid overwriting existing files
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + addGearInventoryViewModel.ImageFile.FileName;

                    // Combine the uploads path with the unique file name
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the uploaded file to the uploads folder
                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        addGearInventoryViewModel.ImageFile.CopyTo(fileStream);
                    }
                }

                GearInventory newGearInventory = new GearInventory
                {
                    Title = addGearInventoryViewModel.Title,
                    Description = addGearInventoryViewModel.Description,
                    MarketValue = addGearInventoryViewModel.MarketValue,
                    // Store the unique file name instead of the IFormFile
                    Image = System.IO.File.ReadAllBytes(uniqueFileName),
                };

                // Add tags to the new gear inventory
                if (addGearInventoryViewModel.SelectedTagIds != null)
                {
                    foreach (var tagId in addGearInventoryViewModel.SelectedTagIds)
                    {
                        var tag = context.Tags.Find(tagId);
                        if (tag != null)
                        {
                            newGearInventory.Tags.Add(tag);
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

// using System;
// using GearGauge.Models;
// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using GearGauge.ViewModels;
// using GearGauge.Data;
// using Microsoft.AspNetCore.Hosting;
// using System.Net.Http.Headers;
// using Microsoft.EntityFrameworkCore.Metadata.Internal;
// using Microsoft.AspNetCore;
// using System.IO;
// using Microsoft.AspNetCore.Http;

// namespace GearGauge.Controllers;

// public class GearInventoryController : Controller
// {
//     private readonly GearGaugeDbContext context;

//     public GearInventoryController(GearGaugeDbContext dbContext)
//     {
//         context = dbContext;
//     }

//     public IActionResult Index()
//     {
//         List<GearInventory> gearInventories = context.GearInventories.ToList();

//         return View(gearInventories);
//     }

//     [HttpGet]
//     public IActionResult Add()
//     {
//         AddGearInventoryViewModel addGearInventoryViewModel  = new AddGearInventoryViewModel(context.GearInventories.ToList());
//         return View(addGearInventoryViewModel);
//     }
   

//     [HttpPost]
//     [Route ("/GearInventory/Add")]
//     public IActionResult Add(AddGearInventoryViewModel addGearInventoryViewModel)
//     {
//         if (ModelState.IsValid)
//         {
//             string uniqueFileName = null;

//         // Check if an image file is uploaded
//         if (addGearInventoryViewModel.ImageFile != null && addGearInventoryViewModel.ImageFile.Length > 0)
//         {
//             // Define the uploads directory and ensure it exists
//             string uploadsFolder = Path.Combine("images");
//             Directory.CreateDirectory(uploadsFolder);

//             // Generate a unique file name to avoid overwriting existing files
//             uniqueFileName = Guid.NewGuid().ToString() + "_" + addGearInventoryViewModel.ImageFile.FileName;

//             // Combine the uploads path with the unique file name
//             string filePath = Path.Combine(uploadsFolder, uniqueFileName);

//             // Save the uploaded file to the uploads folder
//             using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
//             {
//                 addGearInventoryViewModel.ImageFile.CopyTo(fileStream);
//             }
//         }
        
            
//             GearInventory newGearInventory = new GearInventory
//             {
//                 Title = addGearInventoryViewModel.Title,
//                 Description = addGearInventoryViewModel.Description,
//                 MarketValue = addGearInventoryViewModel.MarketValue,
//                 ImageFile = addGearInventoryViewModel.ImageFile,
                
                
                
//             };
        

//             context.GearInventories.Add(newGearInventory);
//             context.SaveChanges();

//             return Redirect("/GearInventory");
//         }
        
//         return View(addGearInventoryViewModel);
//     }
//     public IActionResult Delete()
//     {
//         ViewBag.gearInventories = context.GearInventories.ToList();

//         return View();
//     }
    
//     [HttpPost]
//     public IActionResult Delete(int[] Ids)
//     {
//         foreach (int Id in Ids)
//         {
//             GearInventory? theGearInventory = context.GearInventories.Find(Id);
//             context.GearInventories.Remove(theGearInventory);
//         }
//         context.SaveChanges();
//         return Redirect("/GearInventories");
//     }

//     public IActionResult Detail(int id)
//     {
//         GearInventory theGearInventory = context.GearInventories
//             .Single(g => g.Id == id);

//         GearInventoryViewModel viewModel = new GearInventoryViewModel(theGearInventory);

//         return View(viewModel);
//     }
// }

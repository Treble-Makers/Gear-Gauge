using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GearGauge.Data;
using GearGauge.Models;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;




namespace GearGauge.Controllers;

public class ImagesController : Controller
{
    private readonly GearGaugeDbContext context;

    [HttpPost]
    public IActionResult Create(Images images)
    {
        if (ModelState.IsValid)
        {
            
            string extension = Path.GetExtension(images.ImageFile.FileName);
            string fileName = images.ImageAltDescription.ToLower().Replace('', '-');
            string dateTime = DateTime.Now.ToString("yymmssfff");
            fileName = fileName + dateTime + extension;
            images.ImageFileName = fileName;
            string userPath = "/images/" + User.FindFirstValue(ClaimTypes.NameIdentifier) + "/";
            bool isExists = System.IO.Directory.Exists(userPath);
            if (!isExists)
            {
                System.IO.Directory.CreateDirectory(userPath);
                string path = Path.Combine(userPath, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await images.ImageFile.CopyToAsync(fileStream);

                }
                context.Add(images);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(images);
            }
        }
    }

}

using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GearGauge.ViewModels;
using Microsoft.EntityFrameworkCore;
using GearGauge.Data;
using Microsoft.AspNetCore.Authorization;

namespace GearGauge.Controllers;

public class MusicItemController : Controller
{
    private GearGaugeDbContext context;

    public MusicItemController(GearGaugeDbContext dbContext)
    {
        context = dbContext;
    }

    public IActionResult Index()
    {
        List<MusicItem> musicItems = context.MusicItems.ToList();

        return View(musicItems);
    }

    [HttpGet]
    public IActionResult Add()
    {
        AddMusicItemViewModel addMusicItemViewModel  = new AddMusicItemViewModel(context.MusicItems.ToList());
        return View(addMusicItemViewModel);
    }

    [HttpPost]
    [Route ("/MusicItem/Add")]
    public IActionResult Add(AddMusicItemViewModel addMusicItemViewModel)
    {
        if (ModelState.IsValid)
        {
            
            MusicItem newMusicItem = new MusicItem
            {
                Title = addMusicItemViewModel.Title,
                Description = addMusicItemViewModel.Description,
                MarketValue = addMusicItemViewModel.MarketValue,
                
            };

            context.MusicItems.Add(newMusicItem);
            context.SaveChanges();

            return Redirect("/MusicItem");
        }
        return View(addMusicItemViewModel);
    }
    
    public IActionResult Delete()
    {
        ViewBag.musicItems = context.MusicItems.ToList();

        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Delete(int[] musicItemIds)
    {
        foreach (int musicItemId in musicItemIds)
        {
            MusicItem? theMusicItem = context.MusicItems.Find(musicItemId);
            context.MusicItems.Remove(theMusicItem);
        }
        context.SaveChanges();
        return Redirect("/MusicItem");
    }

    public IActionResult Detail(int id)
    {
        MusicItem theMusicItem = context.MusicItems
            .Single(m => m.Id == id);

        MusicItemViewModel viewModel = new MusicItemViewModel(theMusicItem);
        return View(viewModel);
    }
}

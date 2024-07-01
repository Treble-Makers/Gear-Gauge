using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GearGauge.ViewModels;

namespace GearGauge.Controllers;

public class MusicItemsController : Controller
{
    private MusicItemDbContext context;

    public MusicItemsController(MusicItemDbContext dbContext)
    {
        context = dbContext;
    }

    public IActionResult Index()
    {
        List<MusicItem> musicItems = context.MusicItems.Include(m => m.Category).ToList();

        return View(musicItems);
    }

    [HttpGet]
    public IActionResult Add()
    {
        AddMusicItemViewModel addMusicItemViewModel  = new AddMusicItemViewModel(context.Categories.ToList());
        return View(addMusicItemViewModel);
    }

    [HttpPost]
    public IActionResult Add(AddMusicItemViewModel addMusicItemViewModel)
    {
        if (ModelState.IsValid)
        {
            MusicItem theCategory = context.Categories.Find(addMusicItemViewModel.CategoryId);
            MusicItem newMusicItem = new MusicItem
            {
                Title = addMusicItemViewModel.Title,
                Description = addMusicItemViewModel.Description,
                
            };

            context.MusicItems.Add(newMusicItem);
            context.SaveChanges();

            return Redirect("/MusicItems");
        }
        return View(addMusicItemViewModel);
    }
    public IActionResult Delete()
    {
        ViewBag.musicItems = context.MusicItems.ToList();

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
        return Redirect("/MusicItems");
    }

    public IActionResult Detail(int id)
    {
        MusicItem theMusicItem = context.MusicItems
            .Include(m => m.Category)
            .Single(m => m.Id == id);

        MusicItemViewModel viewModel = new MusicItemViewModel(theMusicItem);
        return View(viewModel);
    }
}

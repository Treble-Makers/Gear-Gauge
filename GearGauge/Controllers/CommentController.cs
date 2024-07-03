using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GearGauge.ViewModels;
using Microsoft.EntityFrameworkCore;
using GearGauge.Data;

namespace GearGauge.Controllers;

public class CommentController : Controller
{
    private CommentDbContext context;

    public CommentController (CommentDbContext dbContext)
    {
        context = dbContext;
    }

    [HttpGet]
    public IActionResult Add()
    {
        AddCommentViewModel addCommentViewModel  = new AddCommentViewModel(context.Comments.ToList());
        return View(addCommentViewModel);
    }

    [HttpPost]
    [Route ("/Comment/Add")]
    public IActionResult Add(AddCommentViewModel addCommentViewModel)
    {
        if (ModelState.IsValid)
        {
            
            Comment newComment = new Comment
            {
                Title = addCommentViewModel.Title,
                Description = addCommentViewModel.Description,
                
            };

            context.Comments.Add(newComment);
            context.SaveChanges();

            return Redirect("/Comment");
        }
        return View(addCommentViewModel);
    }
    public IActionResult Delete()
    {
        ViewBag.Comments = context.Comments.ToList();

        return View();
    }
    [HttpPost]
    public IActionResult Delete(int[] commentIds)
    {
        foreach (int commentId in commentIds)
        {
            Comment? theComment = context.Comments.Find(commentId);
            if(theComment != null)
            {
                context.Comments.Remove(theComment);
            }
        }
        context.SaveChanges();
        return Redirect("/Comment");
    }

    public IActionResult Detail(int id)
    {
        Comment theComment= context.Comments
            .Single(c=> c.Id == id);

        MusicItemViewModel viewModel = new CommentViewModel(theComment);
        return View(viewModel);
    }
}
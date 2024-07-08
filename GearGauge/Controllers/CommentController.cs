using Microsoft.AspNetCore.Mvc;
using GearGauge.ViewModels;
using GearGauge.Models;
using GearGauge.Data;

namespace GearGauge.Controllers;

public class CommentController : Controller
{
    private CommentDbContext context;

    public CommentController (CommentDbContext dbContext)
    {
        context = dbContext;
    }

    [HttpPost]
        public async Task<IActionResult> Create(int musicItemId, string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
            // {
            //     return BadRequest("Please write a review of this item."); -- commented out so that writing is optional
            // }

            var comment = new Comment
            {
                MusicItemId = musicItemId,
                Content = content
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "MusicItems", new { id = musicItemId });
        }

// the code below was based on other code in our program
//  public IActionResult Index()
//         {
//             List<Comment> comments = context.Comments.ToList();
//             return View(comments);
//         }

//     [HttpGet]
//     public IActionResult Add()
//     {
//         AddCommentViewModel addCommentViewModel  = new AddCommentViewModel(context.Comments.ToList());
//         return View(addCommentViewModel);
//     }

//     [HttpPost]
//     [Route ("/Comment/Add")]
//     public IActionResult Add(AddCommentViewModel addCommentViewModel)
//     {
//         if (ModelState.IsValid)
//         {
            
//             Comment newComment = new Comment
//             {
//                 Title = addCommentViewModel.Title,
//                 Description = addCommentViewModel.Description,
                
//             };

//             context.Comments.Add(newComment);
//             context.SaveChanges();

//             return Redirect("/Comment");
//         }
//         return View(addCommentViewModel);
//     }
//     public IActionResult Delete()
//     {
//         ViewBag.Comments = context.Comments.ToList();

//         return View();
//     }
//     [HttpPost]
//     public IActionResult Delete(int[] commentIds)
//     {
//         foreach (int commentId in commentIds)
//         {
//             Comment? theComment = context.Comments.Find(commentId);
//             if(theComment != null)
//             {
//                 context.Comments.Remove(theComment);
//             }
//         }
//         context.SaveChanges();
//         return Redirect("/Comment");
//     }

//     public IActionResult Detail(int id)
//     {
//         Comment theComment= context.Comments
//             .Single(c=> c.Id == id);

//         AddCommentViewModel viewModel = new AddCommentViewModel(theComment);
//         return View(viewModel);
//     }
}
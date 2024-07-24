using System;
using System.IO;
using System.Threading.Tasks;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GearGauge.Controllers;

[Authorize]
public class UserProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UserProfileController(UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var viewModel = new UserProfileViewModel
        {
            UserName = user.UserName,
            Email = user.Email,
            Name = user.Name,
            Address = user.Address,
            AboutMe = user.AboutMe,
            CurrentProfilePictureUrl = user.ProfilePictureUrl
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var viewModel = new UserProfileViewModel
        {
            UserName = user.UserName,
            Email = user.Email,
            Name = user.Name,
            Address = user.Address,
            AboutMe = user.AboutMe,
            CurrentProfilePictureUrl = user.ProfilePictureUrl
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        user.UserName = model.UserName;
        user.Email = model.Email;
        user.Name = model.Name;
        user.Address = model.Address;
        user.AboutMe = model.AboutMe;

        if (model.ProfilePicture != null)
        {
            var uniqueFileName = GetUniqueFileName(model.ProfilePicture.FileName);
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.ProfilePicture.CopyToAsync(fileStream);
            }

            user.ProfilePictureUrl = "/images/" + uniqueFileName;
        }

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Index));
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        return RedirectToAction(nameof(Index));
    }

    private string GetUniqueFileName(string fileName)
    {
        fileName = Path.GetFileName(fileName);
        return Path.GetFileNameWithoutExtension(fileName)
                + "_"
                + Guid.NewGuid().ToString().Substring(0, 4)
                + Path.GetExtension(fileName);
    }
}

// using System;
// using System.IO;
// using System.Threading.Tasks;
// using GearGauge.Models;
// using GearGauge.ViewModels;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace GearGauge.Controllers;

//     [Authorize]
//     public class UserProfileController : Controller
//     {
//         private readonly UserManager<User> _userManager;
//         private readonly SignInManager<User> _signInManager;
//         private readonly IWebHostEnvironment _webHostEnvironment;

//         public UserProfileController(UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment)
//         {
//             _userManager = userManager;
//             _signInManager = signInManager;
//             _webHostEnvironment = webHostEnvironment;

//         }

//         public async Task<IActionResult> Index()
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return NotFound();
//             }

//             var viewModel = new UserProfileViewModel
//             {
//                 UserName = user.UserName,
//                 Email = user.Email,
//                 Name = user.Name,
//                 Address = user.Address,
//                 AboutMe = user.AboutMe,
//                 CurrentProfilePictureUrl = user.ProfilePictureUrl,
//                 IsEditMode = false
//             };

//             return View(viewModel);
//         }

//         [HttpGet]
//         public async Task<IActionResult> Edit()
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return NotFound();
//             }

//             var viewModel = new UserProfileViewModel
//             {
//                 UserName = user.UserName,
//                 Email = user.Email,
//                 Name = user.Name,
//                 Address = user.Address,
//                 AboutMe = user.AboutMe,
//                 CurrentProfilePictureUrl = user.ProfilePictureUrl,
//                 IsEditMode = true
//             };

//             return View(viewModel);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(UserProfileViewModel model)
//         {
//             if (!ModelState.IsValid)
//             {
//                 model.IsEditMode = true;
//                 return View(model);
//             }

//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return NotFound();
//             }

//             user.UserName = model.UserName;
//             user.Email = model.Email;
//             user.Name = model.Name;
//             user.Address = model.Address;
//             user.AboutMe = model.AboutMe;

//             if (model.ProfilePicture != null)
//             {
//                 var uniqueFileName = GetUniqueFileName(model.ProfilePicture.FileName);
//                 var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
//                 var filePath = Path.Combine(uploadsFolder, uniqueFileName);

//                 using (var fileStream = new FileStream(filePath, FileMode.Create))
//                 {
//                     await model.ProfilePicture.CopyToAsync(fileStream);
//                 }

//                 user.ProfilePictureUrl = "/images/" + uniqueFileName;
//             }

//             var result = await _userManager.UpdateAsync(user);
//             if (result.Succeeded)
//             {
//                 return RedirectToAction(nameof(Index));
//             }

//             foreach (var error in result.Errors)
//             {
//                 ModelState.AddModelError(string.Empty, error.Description);
//             }

//             model.IsEditMode = true;
//             return View(model);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Delete()
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return NotFound();
//             }

//             var result = await _userManager.DeleteAsync(user);
//             if (result.Succeeded)
//             {
//                 await _signInManager.SignOutAsync();
//                 return RedirectToAction("Index", "Home");
//             }

//             return RedirectToAction(nameof(Index));
//         }

//         private string GetUniqueFileName(string fileName)
//         {
//             fileName = Path.GetFileName(fileName);
//             return Path.GetFileNameWithoutExtension(fileName)
//                     + "_"
//                     + Guid.NewGuid().ToString().Substring(0, 4)
//                     + Path.GetExtension(fileName);
//         }

//         public class GearGaugeDbContext
//         {
//         }
//     }

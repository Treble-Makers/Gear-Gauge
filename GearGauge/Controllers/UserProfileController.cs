// using System;
// using System.IO;
// using System.Threading.Tasks;
// using GearGauge.Data;
// using GearGauge.Models;
// using GearGauge.ViewModels;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Hosting;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace GearGauge.Controllers
// {
//     [Authorize]
//     public class UserProfileController : Controller
//     {
//         private readonly UserManager<User> _userManager;
//         private readonly IWebHostEnvironment _webHostEnvironment;
//         private readonly GearGaugeDbContext _context;

//         public UserProfileController(UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, GearGaugeDbContext context)
//         {
//             _userManager = userManager;
//             _webHostEnvironment = webHostEnvironment;
//             _context = context;
//         }

//         public async Task<IActionResult> Index()
//         {
//             var user = await _userManager.GetUserAsync(User);
//             if (user == null)
//             {
//                 return NotFound();
//             }

//             var viewModel = new UserProfileUpdateViewModel
//             {
//                 UserName = user.UserName,
//                 Email = user.Email,
//                 Name = user.Name,
//                 Address = user.Address,
//                 AboutMe = user.AboutMe,
//                 CurrentProfilePictureUrl = user.ProfilePictureUrl
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

//             var viewModel = new UserProfileUpdateViewModel
//             {
//                 UserName = user.UserName,
//                 Email = user.Email,
//                 Name = user.Name,
//                 Address = user.Address,
//                 AboutMe = user.AboutMe,
//                 CurrentProfilePictureUrl = user.ProfilePictureUrl
//             };

//             return View(viewModel);
//         }

//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Update(UserProfileUpdateViewModel model)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return View("Edit", model);
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

//             return View("Edit", model);
//         }

//         private string GetUniqueFileName(string fileName)
//         {
//             fileName = Path.GetFileName(fileName);
//             return Path.GetFileNameWithoutExtension(fileName)
//                     + "_"
//                     + Guid.NewGuid().ToString().Substring(0, 4)
//                     + Path.GetExtension(fileName);
//         }
//     }
// }


using System;
using System.IO;
using System.Threading.Tasks;
using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GearGauge.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserProfileController(UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Edit() //changed from Index to Edit
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
                CurrentProfilePictureUrl = user.ProfilePictureUrl,
                IsEditMode = false
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
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
                var uploadsFolder = Path.Combine(__SignInManager.WebRootPath, "images");
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

            return View("Index", model);
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
                await _userManager.SignOutAsync();
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
}
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using GearGauge.Data;
// using GearGauge.Models;
// using GearGauge.ViewModels;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace GearGauge.Controllers
// {
//     public class UserController : Controller
//     {
//         private GearGaugeDbContext context;
//         private readonly UserManager<User> _userManager;

//         public UserController(GearGaugeDbContext dbContext, UserManager<User> userManager)
//         {
//             context = dbContext;
//             _userManager = userManager;
//         }

//         [HttpGet]
//         public IActionResult Register()
//         {
//             return View(new User());
//         }

//         [HttpPost]
//         public async Task<IActionResult> Register(User user)
//         {
//             if (ModelState.IsValid)
//             {
//                 var createUserResult = await _userManager.CreateAsync(user);
//                 if (createUserResult.Succeeded)
//                 {
//                     return RedirectToAction("ThankYou", "Home");
//                 }
//                 else
//                 {
//                     foreach (var error in createUserResult.Errors)
//                     {
//                         ModelState.AddModelError(string.Empty, error.Description);
//                     }
//                 }
//             }

//             return View(user);
//         }

//         [HttpGet]
//         public IActionResult Login()
//         {
//             return View();
//         }

//         [HttpPost]
//         public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
//         {
//             if (ModelState.IsValid)
//             {
//                 var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
//                 if (user != null)
//                 {
//                     var passwordSignInResult = await _userManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
//                     if (passwordSignInResult.Succeeded)
//                     {
//                         if (returnUrl != null)
//                         {
//                             return LocalRedirect(returnUrl);
//                         }
//                         else
//                         {
//                             return RedirectToAction("Index", "Home");
//                         }
//                     }
//                     else
//                     {
//                         ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//                     }
//                 }
//                 else
//                 {
//                     ModelState.AddModelError(string.Empty, "Invalid login attempt.");
//                 }
//             }

//             return View(loginViewModel);
//         }
//     }
// }
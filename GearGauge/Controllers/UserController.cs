using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GearGauge.Controllers;

public class UserController : Controller
{
    private readonly GearGaugeDbContext context;
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;

}
    public UserController(GearGaugeDbContext context, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(
                loginViewModel.Email,
                loginViewModel.Password,
                loginViewModel.RememberMe,
                false
            );

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View(loginViewModel);
            }
        }
        return View(loginViewModel);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (ModelState.IsValid)
        {
            int Index = registerViewModel.Email.IndexOf("@");
            string userName = registerViewModel.Email.Substring(0, Index);

            var user = new User(
                userName,
                registerViewModel.Email,
                registerViewModel.Name,
            );

            var createUserResult = await userManager.CreateAsync(user, registerViewModel.Password!);

            if (createUserResult.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                return View("ConfirmRegistration");
            }
            foreach (var error in createUserResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
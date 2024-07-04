using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GearGauge.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
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
            //login
            var result = await signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid Login Attempt");
            return View(loginViewModel);
        }
        return View(loginViewModel);
    }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (ModelState.IsValid)
        {
            User user = new User
            {
                Name = registerViewModel.Name,
                Email = registerViewModel.Email,
                Address = registerViewModel.Address,
                MusicItemIds = registerViewModel.MusicItemIds
            };
            var result = await userManager.CreateAsync(user, registerViewModel.Password!);
            
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
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
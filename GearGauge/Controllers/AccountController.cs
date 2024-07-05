using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GearGauge.Controllers;

public class AccountController : Controller
{
    public GearGaugeDbContext context;
    public SignInManager<User> signInManager;
    public UserManager<User> userManager;

    // public AccountController(GearGaugeDbContext dbContext)
    // {
    //     context = dbContext;
    // }
    public AccountController(
        GearGaugeDbContext dbContext,
        SignInManager<User> signInManager,
        UserManager<User> userManager
    )
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        context = dbContext;
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
                registerViewModel.Address
            );

            var createUserResult = await userManager.CreateAsync(user, registerViewModel.Password!);

            if (createUserResult.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                //DbContext stuff here?

                return View("ConfirmRegister");
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

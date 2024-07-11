using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GearGauge.Areas.Identity.Pages.Account;

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
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (ModelState.IsValid)
        {
            //login
            var result = await signInManager.PasswordSignInAsync(
                loginModel.Input.Email,
                loginModel.Input.Password,
                loginModel.Input.RememberMe,
                true
            );

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View(loginModel);
            }
        }
        return View(loginModel);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        if (ModelState.IsValid)
        {
            // int Index = registerViewModel.Email.IndexOf("@");
            // string userName = registerViewModel.Email.Substring(0, Index);

            var user = new User(
                // registerModel.UserName,
                registerModel.Input.Email,
                registerModel.Input.Name,
                registerModel.Input.Address

            );

            var createUserResult = await userManager.CreateAsync(user, registerModel.Input.Password!);

            if (createUserResult.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);

                //DbContext stuff here?

                return View("RegisterConfirmation");
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

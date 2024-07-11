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

    [HttpGet]
    public async Task<IActionResult> UpdateProfile()
    {
        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var model = new UserProfileUpdateViewModel
        {
            UserName = user.UserName,
            Email = user.Email,
            Name = user.Name,
            Address = user.Address
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProfile(UserProfileUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        user.UserName = model.UserName;
        user.Email = model.Email;
        user.Name = model.Name;
        user.Address = model.Address;

        if (model.ProfilePicture != null)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", model.ProfilePicture.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.ProfilePicture.CopyToAsync(stream);
            }

           // user.ProfilePictureUrl = $"/images/{model.ProfilePicture.FileName}";
        }

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        await signInManager.RefreshSignInAsync(user);
        return RedirectToAction(nameof(UpdateProfile));
    }
}

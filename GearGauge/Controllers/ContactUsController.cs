using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GearGauge.Data;
using GearGauge.Models;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace GearGauge.Controllers;
[Authorize]
public class ContactUsController : Controller
{
    private readonly UserManager<User> userManager;
    private static GearGaugeDbContext context;
    public ContactUsController(GearGaugeDbContext dbContext)
    {
        this.userManager = userManager;
        context = dbContext;
    }
    [HttpGet]

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SendMessage(ContactUsViewModel contactUsViewModel)
    {
        if (ModelState.IsValid)
        {
            var contactUs = new ContactUs
            {
            UserName = contactUsViewModel.UserName,
            ContactEmail = contactUsViewModel.ContactEmail,
            MessageBody = contactUsViewModel.MessageBody
            };
           
            context.ContactUs.Add(contactUs);
            context.SaveChanges();
         //  string messageBody = $"Name: {this.ContactUs.UserName}\nEmail: {ContactUs.ContactEmail}\nMessage: {ContactUs.MessageBody}";

          // ViewBag.SuccessMessage = "Thank you for contacting us! We will get back to you soon.";
             //return View("Contact", ContactUs); // Re-render Contact view with success message
           
        }

        // If form data is invalid, re-render Contact view with errors
        return View();
    }

}

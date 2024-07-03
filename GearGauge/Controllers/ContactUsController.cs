using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;


namespace GearGauge.Controllers;

public class ContactUsController : Controller
{
    private ContactUsDbContext context;
    public ContactUsController(ContactUsDbContext dbContext)
    {
        context = dbContext;
    }
    public IActionResult Index()
    {
        List<ContactUs> contactUs = context.ContactUs.ToList();
        return View(contactUs);
    }

    [HttpGet]
    public IActionResult Add()
    {
        ContactUs contactUsViewModel = new ContactUsViewModel();

        return View(contactUsViewModel);
    }

    [HttpPost]
    public IActionResult Add(ContactUs contactUs)
    {
        if (ModelState.IsValid)
        {
            string messageBody = $"Name: {contactUs.UserName}\nEmail: {contactUs.ContactEmail}\nMessage: {contactUs.MessageBody}";

            ViewBag.SuccessMessage = "Thank you for contacting us! We will get back to you soon.";
            return View("Contact", contactUs); // Re-render Contact view with success message
        }

        // If form data is invalid, re-render Contact view with errors
        return View("Contact", contactUs);
    }

}

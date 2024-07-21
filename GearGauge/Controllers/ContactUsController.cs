using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GearGauge.Data;
using Microsoft.AspNetCore.Authorization;
using GearGauge.Models;
using Microsoft.EntityFrameworkCore;


namespace GearGauge.Controllers;
public class ContactUsController : Controller
{
    public GearGaugeDbContext context;
    public ContactUsController(GearGaugeDbContext dbContext)
    {
        context = dbContext;
    }
    [HttpGet]

    public IActionResult Index()
    {
        
        return View();
    }

    [HttpPost]
    public IActionResult Index(ContactUsViewModel contactUsViewModel)
    {
        if (ModelState.IsValid)
        {
            ContactUs newContactUs = new ContactUs
            {
                UserName = contactUsViewModel.UserName,
                ContactEmail = contactUsViewModel.ContactEmail,
                MessageBody = contactUsViewModel.MessageBody
            };
            context.ContactUs.Add(newContactUs);
            try{
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
        }
            return Redirect("/ThankYouContactUs");
            

    
    }
        return View();

    }
}

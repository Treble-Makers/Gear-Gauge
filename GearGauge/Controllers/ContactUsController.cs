using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GearGauge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using GearGauge.Data;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GearGauge.Models;


namespace GearGauge.Controllers;
public class ContactUsController : Controller
{
    private GearGaugeDbContext context;
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
            try
            {
                MailMessage msz = new MailMessage();
                msz.From = new MailAddress(contactUsViewModel.ContactEmail);
                msz.To.Add("ketchersidekatie@gmail.com");
                msz.Body = contactUsViewModel.MessageBody;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("ketchersidekatie@gmail.com", "12345678");
                smtp.EnableSsl = true;
                smtp.Send(msz);

                ContactUs contactUs = new ContactUs
                {
                    UserName = contactUsViewModel.UserName,
                    ContactEmail = contactUsViewModel.ContactEmail,
                    MessageBody = contactUsViewModel.MessageBody

                };
             
            


              context.ContactUs.Add(contactUs);
                
                context.SaveChanges();

            }
            catch{
                ModelState.Clear();
            }
        
         //  string messageBody = $"Name: {this.ContactUs.UserName}\nEmail: {ContactUs.ContactEmail}\nMessage: {ContactUs.MessageBody}";

          // ViewBag.SuccessMessage = "Thank you for contacting us! We will get back to you soon.";
             //return View("Contact", ContactUs); // Re-render Contact view with success message
           
        }

        // If form data is invalid, re-render Contact view with errors
        return View();
    }

}

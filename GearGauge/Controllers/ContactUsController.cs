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
                ContactUs contactUs = new ContactUs
                {
                    UserName = contactUsViewModel.UserName,
                    ContactEmail = contactUsViewModel.ContactEmail,
                    MessageBody = contactUsViewModel.MessageBody

                };
             
            


              context.ContactUs.Add(contactUs);
                
                context.SaveChanges();
                MailMessage msz = new MailMessage();
                msz.From = new MailAddress(contactUsViewModel.ContactEmail);
                msz.To.Add("ketchersidekatie@gmail.com");
                msz.Body = contactUsViewModel.MessageBody;
                SmtpClient smtp = new SmtpClient
                {
                Host = "smtp.gmail.com",
                Port = 587,
                Credentials = new System.Net.NetworkCredential("ketchersidekatie@gmail.com", "12345678"),
                EnableSsl = true
                };
                smtp.Send(msz);


            }
            catch{
                ModelState.Clear();
            }
        
   
           
        }

    
        return View();
    }

}

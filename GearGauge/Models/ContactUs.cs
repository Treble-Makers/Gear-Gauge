using System;

namespace GearGauge.Models;

public class ContactUs
{
    public string? UserName { get; set; }
    public string? ContactEmail { get; set; }
    private  readonly string? GearEmail = "geargauge@hotmail.com";
    

public ContactUs(string userName, string contactEmail)
{
    UserName = userName;
    ContactEmail = contactEmail;

}

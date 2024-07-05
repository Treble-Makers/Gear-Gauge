using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GearGauge.Models;

public class ContactUs
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? ContactEmail { get; set; }
    public string? MessageBody { get; set; }
    private  readonly string? GearEmail = "geargauge@hotmail.com";
    public ICollection<ContactUs>? contacts { get; set; }
    

public ContactUs(string userName, string contactEmail, string messageBody)
{
    UserName = userName;
    ContactEmail = contactEmail;
    MessageBody = messageBody;
}

public ContactUs()
{

}

    public override string ToString()
    {
        return UserName;
        
    }
    public override bool Equals(object? obj)
    {
        return obj is ContactUs @contactUs &&
            Id == @contactUs.Id;
       
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(UserName);
    }

    
}

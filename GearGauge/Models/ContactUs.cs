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
            UserName == @contactUs.UserName;
       
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(UserName);
    }
}

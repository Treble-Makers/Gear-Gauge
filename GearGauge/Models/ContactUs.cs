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
    

public ContactUs()
{

}
 public ContactUs(int Id, string userName, string contactEmail, string messageBody)
 {
     Id = Id;
     UserName = userName;
     ContactEmail = contactEmail;
     MessageBody = messageBody;
 }




    
}

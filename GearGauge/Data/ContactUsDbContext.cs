﻿using System;
using GearGauge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GearGauge.Data;

public class ContactUsDbContext : DbContext
{
    
        public DbSet<ContactUs> UserName { get; set; }

        public DbSet<ContactUs> ContactEmail { get; set; }

        public DbSet<ContactUs> MessageBody

}

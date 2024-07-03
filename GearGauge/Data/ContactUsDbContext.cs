﻿using System;
using GearGauge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GearGauge.Data;

public class ContactUsDbContext : DbContext
{
    
        public DbSet<ContactUs> ContactUs { get; set; }

          public ContactUsDbContext(DbContextOptions<ContactUsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactUs>();
        }
                

}

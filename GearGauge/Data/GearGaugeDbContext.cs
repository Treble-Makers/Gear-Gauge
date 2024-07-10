using System;
using System.Drawing;
using GearGauge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GearGauge.Data;

public class GearGaugeDbContext : IdentityDbContext<User>
{
    
        public DbSet<User> User { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }

        public DbSet<Watchlist> Watchlists { get; set; }

        public DbSet<GearInventory> GearInventories { get; set; }

      

          public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
       // builder.Entity<GearInventory>().HasOne(g => g.ImageId).WithOne(i => i.Im)
            base.OnModelCreating(builder);
        }
            // Must build out to establish one to many, many to many, etc. relationships
        
                

}
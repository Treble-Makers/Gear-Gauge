using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GearGauge.Data;

public class GearGaugeDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    
        public DbSet<User> User { get; set; }

          public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
            // Must build out to establish one to many, many to many, etc. relationships
        
                

}
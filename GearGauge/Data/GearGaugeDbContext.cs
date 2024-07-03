using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace GearGauge.Data;

public class GearGaugeDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    
        public DbSet<User> User { get; set; }

          public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        //   builder.Entity<User>().HasMany(m => m.MusicItem).WithOne(i => i.UserName).HasForeignKey(n => n.IdentityUser.Id);
            base.OnModelCreating(builder);
        }
            // Must build out to establish one to many, many to many, etc. relationships
        
                

}
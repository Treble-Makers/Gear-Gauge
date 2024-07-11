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
        // public DbSet<Gear> Gears { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

      

          public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // was builder
        {
          
       // builder.Entity<GearInventory>().HasOne(g => g.ImageId).WithOne(i => i.Im)
            base.OnModelCreating(modelBuilder);

          modelBuilder.Entity<GearInventory>() // was saved as MusicItem so is it GearId?
             .HasMany(mi => mi.Comments)
             .WithOne(c => c.GearInventory) // was saved as MusicItem so is it GearId?
             .HasForeignKey(c => c.Id); // need to change

       
            modelBuilder.Entity<GearInventory>() // was saved as MusicItem so is it GearId?
              .HasMany(mi => mi.Favorites)
              .WithOne(f => f.GearInventory) /// was saved as MusicItem so is it GearId?
              .HasForeignKey(f => f.GearId); // need to change

              modelBuilder.Entity<Comment>()
              .HasKey(c => c.Id);

              
            modelBuilder.Entity<User>() //I think it's user?
              .HasMany(u => u.Favorites)
              .WithOne(f => f.User)
              .HasForeignKey(f => f.UserId);
        }
        
            // Must build out to establish one to many, many to many, etc. relationships
}
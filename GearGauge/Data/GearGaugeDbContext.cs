using System;
using GearGauge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Build.Framework.Profiler;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace GearGauge.Data;

    public class GearGaugeDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<Gear> Gear { get; set; }
      
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<GearInventory> GearInventories { get; set; }
    

        public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
       // builder.Entity<GearInventory>().HasOne(g => g.ImageId).WithOne(i => i.Im)
            base.OnModelCreating(modelBuilder);
        
            // modelBuilder.Entity<Gear>() 
            //   .HasOne(mi => mi.Comment)
            //   .WithOne(f => f.Gear),
               //.HasForeignKey<Comment>(c => c.GearId);

              modelBuilder.Entity<Comment>()
              .HasKey(c => c.Id);

        modelBuilder.Entity<ContactUs>()
            .HasKey(c => c.Id);

            modelBuilder.Entity<User>() //I think it's user?
              .HasMany(u => u.Favorites)
              .WithOne(f => f.User)
              .HasForeignKey(f => f.Id);
 
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GearInventory>()
                .HasKey(g => g.Id);
                //.WithMany(t => t.GearInventories)
                //.UsingEntity(j => j.ToTable("GearInventoryTags"));
        }
    }
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
        public DbSet<GearInventory> GearInventory { get; set; }

        // public DbSet<Profile> UserProfile { get; set; }

        // public DbSet<Comment> Comments { get; set; }

        // public DbSet<FavoriteItem> Favorites { get; set; }

          public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        //   builder.Entity<User>().HasMany(m => m.MusicItem).WithOne(i => i.UserName).HasForeignKey(n => n.IdentityUser.Id);
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }
        
            // Must build out to establish one to many, many to many, etc. relationships
        
                

}

internal class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(x => x.Name).HasMaxLength(50);
    builder.Property(x => x.GearId).HasMaxLength(10000);
    builder.Property(x => x.Address).HasMaxLength(100);
  }
}
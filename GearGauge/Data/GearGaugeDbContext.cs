using GearGauge.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GearGauge.Data
{
    public class GearGaugeDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<GearInventory> GearInventories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between GearInventory and Tag
            modelBuilder.Entity<GearInventory>()
                .HasMany(gi => gi.Tags)
                .WithMany(t => t.GearInventories)
                .UsingEntity(j => j.ToTable("GearInventoryTags"));
        }
    }
}

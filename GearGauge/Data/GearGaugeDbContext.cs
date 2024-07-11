using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GearGauge.Models;

namespace GearGauge.Data
{
    public class GearGaugeDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<GearInventory> GearInventories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GearInventory>()
                .HasMany(g => g.Tags);
                //.WithMany(t => t.GearInventories)
                //.UsingEntity(j => j.ToTable("GearInventoryTags"));
        }
    }
}

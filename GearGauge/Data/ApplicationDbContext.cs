using Microsoft.EntityFrameworkCore;
using GearGauge.Models;


namespace GearGauge.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Gear> Gears { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Watchlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Watchlists)
                .HasForeignKey(w => w.UserId);

            modelBuilder.Entity<Watchlist>()
                .HasOne(w => w.Gear)
                .WithMany()
                .HasForeignKey(w => w.GearId);

                base.OnModelCreating(modelBuilder);
        }
    }

}

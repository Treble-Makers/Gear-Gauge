using GearGauge.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GearGauge.Data
{
    public class GearGaugeDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<Gear> Gear { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<GearInventory> GearInventories { get; set; }
        public DbSet<GearInventoryTag> GearInventoryTags { get; set; }

        public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define many-to-many relationship between GearInventory and Tag
            modelBuilder.Entity<GearInventoryTag>()
                .HasKey(git => new { git.GearInventoryId, git.TagId });

            modelBuilder.Entity<GearInventoryTag>()
                .HasOne(git => git.GearInventory)
                .WithMany(g => g.Tags)
                .HasForeignKey(git => git.GearInventoryId);

            modelBuilder.Entity<GearInventoryTag>()
                .HasOne(git => git.Tag)
                .WithMany(t => t.GearInventoryTags)
                .HasForeignKey(git => git.TagId);

            modelBuilder.Entity<Comment>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<User>()
                .Property(u => u.AboutMe)
                .HasMaxLength(500);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContactUs>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<GearInventory>()
                .HasKey(g => g.Id);
        }
    }
}

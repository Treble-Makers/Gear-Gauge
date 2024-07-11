using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GearGauge.Models;

namespace GearGauge.Data;

    public class GearGaugeDbContext : IdentityDbContext<User>
    {
        public DbSet<User> User { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<GearInventory> GearInventories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // was builder
        {
       // builder.Entity<GearInventory>().HasOne(g => g.ImageId).WithOne(i => i.Im)
            base.OnModelCreating(modelBuilder);
        
            modelBuilder.Entity<Gear>() // was saved as MusicItem so is it GearId?
              .HasOne(mi => mi.Comment)
              .WithOne(f => f.Gear) /// was saved as MusicItem so is it GearId?
               .HasForeignKey<Comment>(c => c.GearId);

              modelBuilder.Entity<Comment>()
              .HasKey(c => c.Id);

    
            modelBuilder.Entity<User>() //I think it's user?
              .HasMany(u => u.Favorites)
              .WithOne(f => f.User)
              .HasForeignKey(f => f.Id);
 
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GearInventory>()
                .HasMany(g => g.Tags);
                //.WithMany(t => t.GearInventories)
                //.UsingEntity(j => j.ToTable("GearInventoryTags"));
        }
    }
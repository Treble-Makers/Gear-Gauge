using System;
using GearGauge.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;



namespace GearGauge.Data;

public class MusicItemDbContext : DbContext
{
    public DbSet<MusicItem> MusicItems {get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<FavoriteItem> Favorites { get; set; }
    public MusicItemDbContext(DbContextOptions<MusicItemDbContext> options) : base(options)
    {

    }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

      
        modelBuilder.Entity<MusicItem>()
            .HasMany(mi => mi.Comments)
            .WithOne(c => c.MusicItem)
            .HasForeignKey(c => c.MusicItemId);

       
        modelBuilder.Entity<MusicItem>()
            .HasMany(mi => mi.Favorites)
            .WithOne(f => f.MusicItem)
            .HasForeignKey(f => f.MusicItemId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Favorites)
            .WithOne(f => f.User)
            .HasForeignKey(f => f.UserId);
    }
}
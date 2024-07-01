using System;
using GearGauge.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;



namespace GearGauge.Data;

public class MusicItemDbContext : DbContext
{
    public DbSet<MusicItem> MusicItems {get; set; }

    public MusicItemDbContext(DbContextOptions<MusicItemDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MusicItem>();
    }


}

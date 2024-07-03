using GearGauge.Models;



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
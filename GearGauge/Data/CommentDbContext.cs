using GearGauge.ViewModels;

namespace GearGauge.Data;

public class CommentDbContext : DbContext
{
    public DbSet<Comment> Comments {get; set; }

    public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>();
    }


}

public class DbContextOptions<T>
{
}

public class DbContext
{
}

public class DbSet<T>
{
    internal List<Comment> ToList()
    {
        throw new NotImplementedException();
    }
}

public class ModelBuilder
{
}

//these last few classes were error fixes but are they even necessary?
//we need to sort out the DbContext file---> may need to rename MusicItem
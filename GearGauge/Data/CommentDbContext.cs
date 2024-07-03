using System;
using GearGauge.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
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
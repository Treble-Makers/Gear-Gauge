using System;
using GearGauge.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GearGauge.ViewModels;

namespace GearGauge.Data;

public class GearGaugeDbContext : DbContext
{
    public DbSet<Comment> Comments { get; set; }

    public GearGaugeDbContext(DbContextOptions<GearGaugeDbContext> options) : base(options)
    {
    }

}

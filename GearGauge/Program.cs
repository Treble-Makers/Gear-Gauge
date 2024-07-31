using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;

var builder = WebApplication.CreateBuilder(args);
var apiConnectionKey = builder.Configuration["apiConnectionKey"];

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

// Get the connection string from appsettings.json
var connectionString = "server=localhost;user=geargauge;password=geargauge;database=geargauge";
var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));
builder.Services.AddDbContext<GearGaugeDbContext>(dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion));
builder.Services.AddDefaultIdentity<User>
(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    }
).AddEntityFrameworkStores<GearGaugeDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

// Seed the initial tags
SeedTags(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedTags(IServiceProvider services)
{
    using (var scope = services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<GearGaugeDbContext>();
        if (!context.Tags.Any())
        {
            context.Tags.AddRange(
                new Tag { Name = "WTB" },
                new Tag { Name = "WTT" },
                new Tag { Name = "WTS" }
            );
            context.SaveChanges();
        }
    }
}

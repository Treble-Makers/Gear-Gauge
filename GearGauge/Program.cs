using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GearGauge.Data;
using GearGauge.Models;
using GearGauge.ViewModels;

var builder = WebApplication.CreateBuilder(args);
var apiConnectionKey = builder.Configuration["apiConnectionKey"];
// var apiConnectionKey = Configuration.GetSection(apiConnectionKey
// ).Get<YouTubeSearchViewModel>();
// _apiConnectionKey = apiConnectionKeyConfig.ServiceApiKey;



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

// Get the connection string from appsettings.json

// var connectionString = builder.Configuration.GetConnectionString("geargauge");
var connectionString = "server=localhost;user=geargauge;password=geargauge;database=geargauge";
var serverVersion = new MySqlServerVersion(new Version(8, 4, 0));
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
    options.Password.RequireLowercase = false;}
).AddEntityFrameworkStores<GearGaugeDbContext>().AddDefaultTokenProviders();

// builder.Services.AddDbContext<GearGaugeDbContext>(dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion));
// builder.Services.AddIdentity<User, IdentityRole>
// (
//     options =>
//     {
//     options.SignIn.RequireConfirmedAccount = false;
//     options.Password.RequireDigit = false;
//     options.Password.RequiredLength = 8;
//     options.Password.RequireNonAlphanumeric = false;
//     options.Password.RequireUppercase = false;
//     options.Password.RequireLowercase = false;}
// ).AddEntityFrameworkStores<GearGaugeDbContext>().AddDefaultTokenProviders();


// public void ConfigureServices(IServiceCollection services)
// {
//     services.AddDbContext<GearGaugeDbContext>(options => options.UseMySql(Configure.GetConnectionString(connectionString)));

//     services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<GearGaugeDbContext>().AddDefaultTokenProviders();
// }

//--- end of connection syntax


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

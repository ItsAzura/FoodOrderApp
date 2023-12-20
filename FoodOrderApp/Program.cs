using FoodOrderApp.Data;
using FoodOrderApp.Helpers;
using FoodOrderApp.Interfaces.Admin;
using FoodOrderApp.Models;
using FoodOrderApp.Repository.Admin;
using FoodOrderApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddControllersWithViews().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); 
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

//builder.Services.AddMvc(options => options.EnableEndpointRouting = false);


var app = builder.Build();

// Seed Data
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    //Seed.SeedUsersAndRolesAsync(app);
    Seed.SeedData(app);
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.UseMvcWithDefaultRoute();

app.Run();

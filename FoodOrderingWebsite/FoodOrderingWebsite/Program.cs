using FoodOrderingWebsite.Helper;
using FoodOrderingWebsite.Models;
using FoodOrderingWebsite.Repository.Category;
using FoodOrderingWebsite.Repository.Product;
using FoodOrderingWebsite.Repository.User;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FoodWebsiteDbContext>(options =>
                            options.UseSqlServer
                           (builder.Configuration.GetConnectionString("FoodDB")));
//Add Service for DbHelper
builder.Services.AddSingleton<DbHelper>();
//Register Services for Category
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();

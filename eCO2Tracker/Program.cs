using eCO2Tracker;
using eCO2Tracker.Services;
using eCoTwoTracker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
var connectionString = builder.Configuration.GetConnectionString("MyConnection");
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAntiforgery(o => o.HeaderName = "CSRF-TOKEN");
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option =>
    {
        option.LoginPath = "/Home/Index";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    }
);

// Entity Framework Core - Managing Schemas - Migrations
// Add-Migration InitialCreate
// Update-Database
// https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs
builder.Services.AddDbContext<MyDbContext>();

// Understanding Dependency Injection Lifetime
// https://www.c-sharpcorner.com/article/understanding-addtransient-vs-addscoped-vs-addsingleton-in-asp-net-core/
builder.Services.AddScoped<ShopItemService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<User_ShopItemService>();
builder.Services.AddScoped<LifestyleService>();
builder.Services.AddScoped<IActivityService, ActivityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrate;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Veritabanı bağlantısını yapılandır
builder.Services.AddDbContext<UygulamaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Repository'lerin Dependency Injection kaydı
builder.Services.AddScoped<IRandevuDal, EfRandevuRepository>();
builder.Services.AddScoped<IUserAppDal, EfUserAppRepository>();
builder.Services.AddScoped<AppUserManager>();
builder.Services.AddScoped<RandevuManager>();
builder.Services.AddScoped<IRandevuService, RandevuManager>();

// ASP.NET Identity yapılandırması
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<UygulamaDbContext>()
    .AddDefaultTokenProviders();


// MVC ve Controller yapılandırması
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

var app = builder.Build();

// Hata ve HTTPS yönlendirme yapılandırması
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Authentication ve Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Default route
app.MapControllerRoute(
    name: "default",
        pattern: "{controller=Hasta}/{action=Login}/{id?}");

app.Run();
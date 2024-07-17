using Agenda.Models;
using Agenda.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text.Json;
using System.Text.Json.Serialization;
using Agenda.Interfaces;
using Agenda.Services;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseNpgsql(Env.GetString("DbConnection")));

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Session/Index"; 
        option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        option.SlidingExpiration = true;
        option.Cookie.HttpOnly = true;
        option.Cookie.Name = "Agenda-Auth";
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddSingleton<ICookieService,CookieService>();

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
    pattern: "{controller=Session}/{action=Index}/{id?}");

app.Run();

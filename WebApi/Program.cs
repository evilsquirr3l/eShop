using System.Reflection;
using AutoMapper;
using Business;
using Data;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllersWithViews()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.Load("Business")));

builder.Services.AddDbContextPool<EShopDbContext>(dbContextOptionsBuilder =>
    dbContextOptionsBuilder
        .UseLazyLoadingProxies()
        .UseNpgsql(builder.Configuration.GetConnectionString("eShop")));

var mapperConfig = new MapperConfiguration(c => c.AddProfile(new AutomapperProfile()));
builder.Services.AddSingleton(mapperConfig.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");


app.Run();
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Business;
using Business.Automapper;
using Business.Interfaces;
using Business.Services;
using Data;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

AddServices();

void AddServices()
{
    builder.Services
        .AddControllers()
        .AddFluentValidation(fv =>
        {
            fv.RegisterValidatorsFromAssembly(Assembly.Load("Business"));
            fv.DisableDataAnnotationsValidation = true;
        });

    builder.Services.AddDbContextPool<EShopDbContext>(dbContextOptionsBuilder =>
        dbContextOptionsBuilder
            .UseLazyLoadingProxies()
            .UseNpgsql(builder.Configuration.GetConnectionString("eShop")));

    builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    builder.Services.AddTransient<IProductService, ProductService>();
    builder.Services.AddTransient<ICategoryService, CategoryService>();
    builder.Services.AddAutoMapper(typeof(AutomapperProfile).Assembly);

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo {Title = "eShop", Description = "Web api for eShop frontend"});
    });
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "eShop API");
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.Run();

//For creating web application factory in integration tests
[ExcludeFromCodeCoverage]
public partial class Program { }
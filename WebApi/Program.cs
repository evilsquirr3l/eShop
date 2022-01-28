using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Business;
using Business.Automapper;
using Business.Interfaces;
using Business.Services;
using Data;
using Data.Entities;
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

    builder.Services.AddIdentity<User, UserRole>()
        .AddEntityFrameworkStores<EShopDbContext>();
    
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

//Create db and apply migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<EShopDbContext>();
    if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
    {
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        var migrations = pendingMigrations.ToList();
        if (migrations.Any())
        {
            Console.WriteLine($"You have {migrations.Count} pending migrations to apply.");
            Console.WriteLine("Applying pending migrations now");
            await context.Database.MigrateAsync();
        }

        var lastAppliedMigration = (await context.Database.GetAppliedMigrationsAsync()).Last();

        Console.WriteLine($"You're on schema version: {lastAppliedMigration}");
    }
}

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

//For creating web application factory in integration tests
[ExcludeFromCodeCoverage]
public partial class Program { }
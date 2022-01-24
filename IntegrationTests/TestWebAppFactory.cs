using System.Linq;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public class TestWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault<ServiceDescriptor>(d => d.ServiceType == typeof(DbContextOptions<EShopDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);
            services.AddDbContextPool<EShopDbContext>(options => { options.UseInMemoryDatabase("InmemoryDb"); });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<EShopDbContext>();
            SeedData(dbContext);
            dbContext.Database.EnsureCreated();
        });
    }

    private static void SeedData(EShopDbContext dbContext)
    {
        dbContext.Categories.Add(new Category {Name = "category1", Description = "category1 description"});
        dbContext.Categories.Add(new Category {Name = "category2", Description = "category2 description"});
        dbContext.Products.Add(new Product {Name = "product1", Description = "product1 description", CategoryId = 1});
        dbContext.Products.Add(new Product {Name = "product2", Description = "product2 description", CategoryId = 2});

        dbContext.SaveChanges();
    }
}
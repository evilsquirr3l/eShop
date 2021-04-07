using System;
using System.Linq;
using Data.Entities;
using Data.Implementation;
using eShop.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.IntegrationTests
{
    internal class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                RemoveLibraryDbContextRegistration(services);

                var serviceProvider = GetInMemoryServiceProvider();

                services.AddDbContextPool<ShopDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.Empty.ToString());
                    options.UseInternalServiceProvider(serviceProvider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ShopDbContext>();

                SeedData(context);
            });
        }

        private void SeedData(ShopDbContext context)
        {
            context.Carts.AddAsync(new Cart { Id = 1, TotalPrice = 1488});
            context.Carts.AddAsync(new Cart { Id = 2, TotalPrice = 1337});

            context.SaveChanges();
        }

        private static ServiceProvider GetInMemoryServiceProvider()
        {
            return new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
        }

        private static void RemoveLibraryDbContextRegistration(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ShopDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }
    }
}
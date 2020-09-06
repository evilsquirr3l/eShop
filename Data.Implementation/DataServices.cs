using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Implementation
{
    public static class DataServices
    {
        //TODO: write extension method to iservicecollection and register dbcontext/identitycore
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShopDbContext>(options =>
                options.UseSqlServer(connectionString));
            return services;
        }
        //test commit comment
    }
}
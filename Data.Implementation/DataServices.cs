using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Implementation
{
    public static class DataServices
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShopDbContext>(options =>
                options.UseNpgsql(connectionString));
            
            return services;
        }
    }
}
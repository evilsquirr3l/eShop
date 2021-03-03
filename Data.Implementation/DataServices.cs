using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Data.Implementation
{
    public static class DataServices
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();
            builder.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            builder.Username = configuration["UserID"];
            builder.Password = configuration["Password"];
            services.AddDbContext<ShopDbContext>(opt => opt.UseNpgsql(builder.ConnectionString));
            
            return services;
        }
    }
}
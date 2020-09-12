using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Implementation
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection BindMapper(this IServiceCollection services)
        {
            services.AddTransient<IMapper>();
            return services;
        }
    }

}
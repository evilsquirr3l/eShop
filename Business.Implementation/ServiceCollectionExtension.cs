using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Implementation
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfile()));
            var mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            return services;
        }
    }

}
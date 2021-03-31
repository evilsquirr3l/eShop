using AutoMapper;
using Business.Abstraction;
using Business.Implementation.Services;
using Business.Implementation.Validations;
using Business.Models;
using FluentValidation;
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

            services.AddTransient(typeof(IServiceHelper<>), typeof(ServiceHelper<>));

            services.AddTransient<ICrudInterface<CartDto>, CartService>();
            services.AddTransient<ICrudInterface<CategoryDto>, CategoryService>();
            services.AddTransient<ICrudInterface<ProductDto>, ProductService>();

            services.AddTransient<AbstractValidator<CartDto>, CartValidation>();
            services.AddTransient<AbstractValidator<CategoryDto>, CategoryValidation>();
            services.AddTransient<AbstractValidator<ProductDto>, ProductValidation>();
            
            return services;
        }
    }
}
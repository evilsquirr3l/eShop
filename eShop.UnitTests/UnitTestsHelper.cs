using System;
using System.Collections.Generic;
using AutoMapper;
using Business.Implementation;
using Data.Entities;
using Data.Implementation;
using Microsoft.EntityFrameworkCore;

namespace eShop.UnitTests
{
    internal static class UnitTestsHelper
    {
        public static DbContextOptions<ShopDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using var context = new ShopDbContext(options);
            SeedData(context);
            return options;
        }
        
        private static void SeedData(ShopDbContext context)
        {
            context.Carts.Add(new Cart {Id = 1, Products = new List<Product>(), TotalPrice = 0});
            context.Carts.Add(new Cart {Id = 2, Products = new List<Product>(), TotalPrice = 1});
            context.Categories.Add(new Category { Id = 1, Products = new List<Product>(), Name="Pa" });
            context.Categories.Add(new Category { Id = 2, Products = new List<Product>(), Name = "Pich" });
            context.Products.Add(new Product { Id = 1,  Name = "Vi" });
            context.Products.Add(new Product { Id = 2,  Name = "Ka" });
            context.SaveChanges();
        }
        
        public static Mapper CreateMapperProfile()
        {
            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }
    }
}
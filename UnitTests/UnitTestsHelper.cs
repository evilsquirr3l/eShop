using System;
using AutoMapper;
using Business.Automapper;
using Business.Interfaces;
using Data;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

internal static class UnitTestsHelper
{
    public static DbContextOptions<EShopDbContext> UseInmemoryDatabase()
    {
        var options = new DbContextOptionsBuilder<EShopDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return options;
    }

    public static Mapper CreateAutomapper(IDateTimeProvider dateTimeProvider)
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(AutomapperProfile).Assembly);
        });

        return new Mapper(configuration);
    }
}
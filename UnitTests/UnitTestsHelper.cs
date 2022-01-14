using System;
using Data;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

internal static class UnitTestsHelper
{
    public static DbContextOptions<EShopDbContext> GetUnitTestDbOptions()
    {
        var options = new DbContextOptionsBuilder<EShopDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return options;
    }
}
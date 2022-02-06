using System;
using AutoMapper;
using Business.Automapper;
using Business.Interfaces;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests;

internal static class UnitTestsHelper
{
    public static EShopDbContext UseInmemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<EShopDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new EShopDbContext(options);
    }

    public static Mapper CreateAutomapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(AutomapperProfile).Assembly);
        });

        return new Mapper(configuration);
    }
    
    public static Mock<IDateTimeProvider> DateTimeProviderMock(DateTime dateTime)
    {
        var dateTimeProvider = new Mock<IDateTimeProvider>();
        dateTimeProvider.Setup(x => x.GetCurrentTime()).Returns(dateTime);

        return dateTimeProvider;
    }
    
    public static (Mock<UserManager<User>> userManagerMock, Mock<SignInManager<User>> signInManagerMock) GetMockedIdentityManagers()
    {
        var userManagerMock = new Mock<UserManager<User>>(
            /* IUserStore<TUser> store */Mock.Of<IUserStore<User>>(),
            /* IOptions<IdentityOptions> optionsAccessor */null,
            /* IPasswordHasher<TUser> passwordHasher */null,
            /* IEnumerable<IUserValidator<TUser>> userValidators */null,
            /* IEnumerable<IPasswordValidator<TUser>> passwordValidators */null,
            /* ILookupNormalizer keyNormalizer */null,
            /* IdentityErrorDescriber errors */null,
            /* IServiceProvider services */null,
            /* ILogger<UserManager<TUser>> logger */null);

        var signInManagerMock = new Mock<SignInManager<User>>(
            userManagerMock.Object,
            /* IHttpContextAccessor contextAccessor */Mock.Of<IHttpContextAccessor>(),
            /* IUserClaimsPrincipalFactory<TUser> claimsFactory */Mock.Of<IUserClaimsPrincipalFactory<User>>(),
            /* IOptions<IdentityOptions> optionsAccessor */null,
            /* ILogger<SignInManager<TUser>> logger */null,
            /* IAuthenticationSchemeProvider schemes */null,
            /* IUserConfirmation<TUser> confirmation */null);

        return (userManagerMock, signInManagerMock);
    }
}
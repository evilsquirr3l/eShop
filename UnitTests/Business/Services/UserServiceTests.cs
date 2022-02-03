using System;
using AutoMapper;
using Business.Interfaces;
using Business.Records;
using Business.Services;
using Data.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace UnitTests.Business.Services;

[TestFixture]
public class UserServiceTests
{
    private IUserService _userService;
    private Mock<UserManager<User>> _userManager;
    private Mock<SignInManager<User>> _signInManager;
    private Mock<IJwtAuthService> _jwtAuthService;
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        (_userManager, _signInManager) = UnitTestsHelper.GetMockSignInManager();
        _jwtAuthService = new Mock<IJwtAuthService>();
        var mapper = UnitTestsHelper.CreateAutomapper();

        _userService = new UserService(_userManager.Object, _signInManager.Object, _jwtAuthService.Object, _mapper);
    }

    [Test]
    public void LoginAsync_UserIsNotFound_ThrowsArgumentException()
    {
        var loginRecord = new LoginRecord() {Email = "any@mail.com", Password = "qwerty"};
        _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null!);

        var action = async () => await _userService.LoginAsync(loginRecord);

        action.Should().ThrowExactlyAsync<ArgumentException>().WithMessage("User with this email is not found!");
    }
}
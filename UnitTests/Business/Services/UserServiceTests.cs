using System;
using System.Threading.Tasks;
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
        (_userManager, _signInManager) = UnitTestsHelper.GetMockedIdentityManagers();
        _jwtAuthService = new Mock<IJwtAuthService>();
        _mapper = UnitTestsHelper.CreateAutomapper();

        _userService = new UserService(_userManager.Object, _signInManager.Object, _jwtAuthService.Object, _mapper);
    }

    [Test]
    public async Task LoginAsync_UserIsNotFound_ThrowsArgumentException()
    {
        var loginRecord = new LoginRecord { Email = "any@mail.com", Password = "qwerty" };
        _userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(() => null!);

        var action = async () => await _userService.LoginAsync(loginRecord);

        await action.Should().ThrowExactlyAsync<ArgumentException>().WithMessage("User with this email is not found!");
    }

    [TestCase("any@mail.com", "qwerty")]
    public async Task LoginAsync_WhenPasswordIsIncorrect_ThrowsArgumentException(string email, string password)
    {
        var loginRecord = new LoginRecord { Email = email, Password = password };
        _userManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(new User());
        _signInManager.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<User>(), password, false))
            .ReturnsAsync(SignInResult.Failed);

        var action = async () => await _userService.LoginAsync(loginRecord);

        await action.Should().ThrowExactlyAsync<ArgumentException>().WithMessage("Login failed.");
    }
    
    [TestCase("any@mail.com", "qwerty", "jwtToken")]
    public async Task LoginAsync_WhenPasswordIsCorrect_ReturnsUserRecordWithToken(string email, string password, string jwtToken)
    {
        var loginRecord = new LoginRecord { Email = email, Password = password };
        _userManager.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(new User { Email = email} );
        _signInManager.Setup(x => x.CheckPasswordSignInAsync(It.IsAny<User>(), password, false))
            .ReturnsAsync(SignInResult.Success);
        _jwtAuthService.Setup(x => x.CreateToken(It.IsAny<User>())).Returns(jwtToken);

        var result = await _userService.LoginAsync(loginRecord);

        result.Email.Should().Be(email);
        result.Token.Should().Be(jwtToken);
    }
}
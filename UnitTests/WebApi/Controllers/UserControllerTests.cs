using System;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Records;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;

namespace UnitTests.WebApi.Controllers;

[TestFixture]
public class UserControllerTests
{
    private UserController _userController;
    private Mock<IUserService> _userService;
    
    [SetUp]
    public void SetUp()
    {
        _userService = new Mock<IUserService>();
        _userController = new UserController(_userService.Object);
    }

    [TestCase("valid@mail.com", "qwerty")]
    public async Task LoginAsync_WithValidaData_ReturnsUserRecord(string email, string password)
    {
        var loginRecord = new LoginRecord { Email = email, Password = password };
        
        var result = await _userController.LoginAsync(loginRecord);
        
        result.Should().BeOfType<ActionResult<UserRecord>>();
    }
    
    [Test]
    public async Task LoginAsync_WhenUserServiceThrowsException_Returns401()
    {
        var exceptionMessage = "TestMessage";
        _userService.Setup(x => x.LoginAsync(It.IsAny<LoginRecord>())).ThrowsAsync(new ArgumentException(exceptionMessage));
        
        var result = await _userController.LoginAsync(new LoginRecord());

        result.Result.Should().BeOfType<UnauthorizedObjectResult>();
    }
}
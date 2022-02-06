using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Business;
using Business.Interfaces;
using Business.Services;
using Data.Entities;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;

namespace UnitTests.Business.Services;

[TestFixture]
public class JwtAuthServiceTests
{
    private IJwtAuthService _jwtAuthService;
    private Mock<IOptions<JwtSettings>> _options;
    private const string SecurityKey = "requires a key size of at least '128' bits";

    [SetUp]
    public void SetUp()
    {
        _options = new Mock<IOptions<JwtSettings>>();
        var jwtSettings = new JwtSettings { TokenKey = SecurityKey, Lifetime = TimeSpan.FromMinutes(10)};
        _options.Setup(x => x.Value).Returns(jwtSettings);
        
        _jwtAuthService = new JwtAuthService(_options.Object);
    }

    [Test]
    public void CreateToken_ForUser_ReturnsValidToken()
    {
        var user = new User { UserName = "test" };

        var token = _jwtAuthService.CreateToken(user);

        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey)),
            ValidateLifetime = true
        }, out SecurityToken validatedToken);
        validatedToken.Should().BeOfType<JwtSecurityToken>();
    }
}
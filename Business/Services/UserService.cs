using Business.Interfaces;
using Business.Records;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtAuthService _jwtAuthService;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtAuthService jwtAuthService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtAuthService = jwtAuthService;
    }

    public async Task<UserRecord> LoginAsync(LoginRecord loginRecord)
    {
        var user = await _userManager.FindByEmailAsync(loginRecord.Email);

        if (user is null)
        {
            throw new ArgumentException("User with this email is not found!");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginRecord.Password, false);

        if (result.Succeeded)
        {
            return new UserRecord()
            {
                Token = _jwtAuthService.CreateToken(user),
                Username = user.UserName
            };
        }

        throw new ArgumentException("Login failed.");
    }
}
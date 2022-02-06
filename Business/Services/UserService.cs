using AutoMapper;
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
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtAuthService jwtAuthService, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtAuthService = jwtAuthService;
        _mapper = mapper;
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
            var userRecord = _mapper.Map<UserRecord>(user);
            userRecord.Token = _jwtAuthService.CreateToken(user);

            return userRecord;
        }

        throw new ArgumentException("Login failed.");
    }
}
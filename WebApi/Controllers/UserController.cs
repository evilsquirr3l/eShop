using Business.Interfaces;
using Business.Records;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
[Consumes("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserRecord>> LoginAsync(LoginRecord login)
    {
        try
        {
            var userRecord = await _userService.LoginAsync(login);

            return Ok(userRecord);
        }
        catch (ArgumentException e)
        {
            return Unauthorized(e.Message);
        }
    }
}
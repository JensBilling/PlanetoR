using Microsoft.AspNetCore.Mvc;
using PlanetoR.Models;
using PlanetoR.Utility;

namespace PlanetoR.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    public static User user = new User();

    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUser(UserDto requestUserDto)
    {
        AuthHelper.CreatePasswordHash(requestUserDto.Password, out var passwordHash, out var passwordSalt);

        user.Username = requestUserDto.Username;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        return Ok("User created");
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginUser(UserDto requestUserDto)
    {
        if (user.Username != requestUserDto.Username)
        {
            return BadRequest("User not found!");
        }

        if (!AuthHelper.VerifyPasswordHash(requestUserDto.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Wrong password!");
        }

        string jwt = AuthHelper.CreateToken(user);

        return jwt;
    }
}
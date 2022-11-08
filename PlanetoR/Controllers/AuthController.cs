using Microsoft.AspNetCore.Mvc;
using PlanetoR.Data;
using PlanetoR.Models;
using PlanetoR.Utility;

namespace PlanetoR.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;

    public AuthController(IConfiguration configuration, AppDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUser(UserDto requestUserDto)
    {
        // Check if email and username is available
        var foundUser = _context.users.FirstOrDefault(u => u.Username == requestUserDto.Username);
        if (foundUser != null)
        {
            return BadRequest("Username is already taken");
        }

        foundUser = _context.users.FirstOrDefault(u => u.Email == requestUserDto.Email);
        if (foundUser != null)
        {
            return BadRequest("Email is already taken");
        }

        AuthHelper.CreatePasswordHash(requestUserDto.Password, out var passwordHash, out var passwordSalt);
        var user = new User();
        var apiKeyGuid = Guid.NewGuid().ToString();
        apiKeyGuid = apiKeyGuid.Replace("-", "");
        apiKeyGuid = apiKeyGuid.ToLower();

        user.Username = requestUserDto.Username;
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.Email = requestUserDto.Email;
        user.ApiKey = apiKeyGuid;

        _context.users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User created");
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginUser(UserDto requestUserDto)
    {
        var foundUser = _context.users.FirstOrDefault(u => u.Username == requestUserDto.Username);

        if (foundUser == null)
        {
            foundUser = _context.users.FirstOrDefault(u => u.Email == requestUserDto.Email);
            if (foundUser == null)
            {
                return BadRequest("User not found!");
            }
        }

        if (!AuthHelper.VerifyPasswordHash(requestUserDto.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
        {
            return BadRequest("Wrong password!");
        }

        string jwt = "";
        if (foundUser.isAdmin)
        {
            jwt = AuthHelper.CreateAdminToken(foundUser);
        }
        else
        {
            jwt = AuthHelper.CreateUserToken(foundUser);
        }

        return jwt;
    }

    [HttpPut("update-password")]
    public async Task<ActionResult<String>> UpdatePassword(UserDto requestUserDto)
    {
        var foundUser = _context.users.FirstOrDefault(u => u.Username == requestUserDto.Username);

        if (foundUser == null)
        {
            foundUser = _context.users.FirstOrDefault(u => u.Email == requestUserDto.Email);
            if (foundUser == null)
            {
                return BadRequest("User not found!");
            }
        }
        
        if (!AuthHelper.VerifyPasswordHash(requestUserDto.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
        {
            return BadRequest("Wrong password!");
        }
        
        AuthHelper.CreatePasswordHash(requestUserDto.NewPassword, out var passwordHash, out var passwordSalt);

        foundUser.PasswordHash = passwordHash;
        foundUser.PasswordSalt = passwordSalt;

        await _context.SaveChangesAsync();
        
        return Ok("Password for " + requestUserDto.Username + " updated");
    }
}
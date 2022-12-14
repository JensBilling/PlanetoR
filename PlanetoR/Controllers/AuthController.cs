using Microsoft.AspNetCore.Mvc;
using PlanetoR.Data;
using PlanetoR.Models;
using PlanetoR.Utility;

namespace PlanetoR.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterUser(UserDto requestUserDto)
    {
        // Check if email and username is available
        var foundUser = _context.Users.FirstOrDefault(u => u.Username == requestUserDto.Username);
        if (foundUser != null)
        {
            return BadRequest("Username is already taken");
        }

        foundUser = _context.Users.FirstOrDefault(u => u.Email == requestUserDto.Email);
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

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        MailHelper.MailSender(requestUserDto.Email, "Welcome to PlanetoR, " + requestUserDto.Username + "! Your account has been created.",
            "Welcome to PlanetoR!");

        return Ok("User created");
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginUser(UserDto requestUserDto)
    {
        var foundUser = _context.Users.FirstOrDefault(u => u.Username == requestUserDto.Username);

        if (foundUser == null)
        {
            foundUser = _context.Users.FirstOrDefault(u => u.Email == requestUserDto.Email);
            if (foundUser == null)
            {
                return BadRequest("User not found!");
            }
        }

        if (!AuthHelper.VerifyPasswordHash(requestUserDto.Password, foundUser.PasswordHash, foundUser.PasswordSalt))
        {
            return BadRequest("Wrong password!");
        }

        var jwt = "";

        jwt = foundUser.isAdmin ? AuthHelper.CreateAdminToken(foundUser) : AuthHelper.CreateUserToken(foundUser);
        
        return jwt;
    }

    [HttpPut("update-password")]
    public async Task<ActionResult<String>> UpdatePassword(UserDto requestUserDto)
    {
        var foundUser = _context.Users.FirstOrDefault(u => u.Username == requestUserDto.Username);

        if (foundUser == null)
        {
            foundUser = _context.Users.FirstOrDefault(u => u.Email == requestUserDto.Email);
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
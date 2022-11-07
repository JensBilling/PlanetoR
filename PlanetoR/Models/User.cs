using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PlanetoR.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string Email { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
}
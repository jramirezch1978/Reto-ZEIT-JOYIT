namespace CleanArchitecture.Domain.Models;

public class LoginRequest
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }
} 
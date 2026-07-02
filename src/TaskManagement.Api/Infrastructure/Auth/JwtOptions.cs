namespace TaskManagement.Api.Infrastructure.Auth;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = "TaskManagement.Api";
    public string Audience { get; set; } = "TaskManagement.Api.Client";
    public string Key { get; set; } = "ThisIsADevelopmentKeyWithAtLeast32Chars";
    public int ExpiryMinutes { get; set; } = 60;
    public List<UserCredential> Users { get; set; } =
    [
        new UserCredential { Username = "admin", Password = "admin123", Role = "Admin" },
        new UserCredential { Username = "user", Password = "user123", Role = "User" }
    ];
}

public class UserCredential
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
}

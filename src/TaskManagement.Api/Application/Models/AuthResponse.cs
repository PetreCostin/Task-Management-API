namespace TaskManagement.Api.Application.Models;

public sealed class AuthResponse
{
    public required string Token { get; init; }
    public required DateTime ExpiresAtUtc { get; init; }
}

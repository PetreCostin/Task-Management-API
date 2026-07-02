using Microsoft.Extensions.Options;

namespace TaskManagement.Api.Infrastructure.Auth;

public interface IUserStore
{
    UserCredential? Validate(string username, string password);
}

public sealed class InMemoryUserStore(IOptions<JwtOptions> jwtOptions) : IUserStore
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public UserCredential? Validate(string username, string password)
    {
        return _jwtOptions.Users.FirstOrDefault(user =>
            string.Equals(user.Username, username, StringComparison.OrdinalIgnoreCase) &&
            user.Password == password);
    }
}

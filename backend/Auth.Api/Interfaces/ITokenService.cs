using Auth.Api.Models;

namespace Auth.Api.Interfaces;

public interface ITokenService
{
    Task<string> CreateTokenAsync(User user);
}

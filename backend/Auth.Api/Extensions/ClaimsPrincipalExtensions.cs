using System.Security.Claims;

namespace Auth.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new Exception("User does not have a user id.");

        return Guid.Parse(userId);
    }
}

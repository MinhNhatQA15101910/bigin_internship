using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Models;

public class Role : IdentityRole<Guid>
{
    public ICollection<UserRole> UserRoles { get; set; } = [];
}

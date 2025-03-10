using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Models;

public class User : IdentityUser<Guid>
{
    public required string FullName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<UserRole> UserRoles { get; set; } = [];
}

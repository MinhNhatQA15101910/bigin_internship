namespace API.Models;

public class User
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Role> Roles { get; set; } = [];
}

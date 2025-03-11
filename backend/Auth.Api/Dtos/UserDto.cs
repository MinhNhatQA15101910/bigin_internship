namespace Auth.Api.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string FullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<string>? Roles { get; set; }
    public string? Token { get; set; }
}

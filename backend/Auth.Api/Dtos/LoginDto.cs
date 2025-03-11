using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Dtos;

public class LoginDto
{
    [EmailAddress]
    public required string Email { get; set; }

    [StringLength(50, MinimumLength = 8)]
    public required string Password { get; set; }
}

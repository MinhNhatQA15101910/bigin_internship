using System.ComponentModel.DataAnnotations;

namespace Auth.Api.Dtos;

public class ChangePasswordDto
{
    [Required]
    [StringLength(50, MinimumLength = 8)]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required]
    [StringLength(50, MinimumLength = 8)]
    public string NewPassword { get; set; } = string.Empty;
}

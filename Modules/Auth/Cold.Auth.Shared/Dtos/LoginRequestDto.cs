using System.ComponentModel.DataAnnotations;

namespace Cold.Auth.Shared.Dtos;

public class LoginRequestDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}
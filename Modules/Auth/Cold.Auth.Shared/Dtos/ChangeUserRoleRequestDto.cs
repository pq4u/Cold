using System.ComponentModel.DataAnnotations;

namespace Cold.Auth.Shared.Dtos;

public class ChangeUserRoleRequestDto
{
    [Required]
    public string Role { get; set; }
}
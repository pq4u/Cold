namespace Cold.Auth.Shared.Dtos;

public class AuthResponseDto
{
    public required string UserId { get; set; }
    public required string Token { get; set; }
    public required DateTime ExpiresAt { get; set; }
}
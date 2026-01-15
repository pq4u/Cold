namespace Cold.Auth.Shared.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public IList<string> Roles { get; set; }
}
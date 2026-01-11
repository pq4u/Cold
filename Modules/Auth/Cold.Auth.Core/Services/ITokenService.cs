using System.Security.Claims;
using Cold.Auth.Core.Entities;

namespace Cold.Auth.Core.Services;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
}
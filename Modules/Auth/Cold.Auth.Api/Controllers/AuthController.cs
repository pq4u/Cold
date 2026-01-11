using Cold.Auth.Core.Entities;
using Cold.Auth.Core.Services;
using Cold.Auth.Shared.Dtos;
using Cold.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cold.Auth.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterRequestDto request)
    {
        var validRoles = new[] { "Supplier", "Employee", "Admin" };
        if (!validRoles.Contains(request.Role))
        {
            return BadRequest($"Invalid role. Must be one of: {string.Join(", ", validRoles)}");
        }

        if (!await _roleManager.RoleExistsAsync(request.Role))
        {
            return BadRequest($"Role '{request.Role}' does not exist.");
        }
        
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, request.Role);

        return Ok(new { Message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ArgumentException("Invalid email or password");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.GenerateToken(user, roles);

        return Ok(new AuthResponseDto
        {
            UserId = user.Id.ToString(),
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        });
    }
}
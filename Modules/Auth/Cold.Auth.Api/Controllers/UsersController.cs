using Cold.Auth.Core.Entities;
using Cold.Auth.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cold.Auth.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
    {
        var users = _userManager.Users.ToList();
        var userDtos = new List<UserDto>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userDtos.Add(new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles
            });
        }
        return Ok(userDtos);
    }

    [HttpGet("role/{role}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByRole(string role)
    {
        return await GetUsersByRoleInternal(role);
    }

    [HttpGet("suppliers")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetSuppliers()
    {
        return await GetUsersByRoleInternal("Supplier");
    }

    [HttpGet("employees")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetEmployees()
    {
        return await GetUsersByRoleInternal("Employee");
    }

    [HttpGet("admins")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetAdmins()
    {
        return await GetUsersByRoleInternal("Admin");
    }

    [HttpPut("{userId}/role")]
    public async Task<ActionResult> ChangeRole(Guid userId, ChangeUserRoleRequestDto request)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return NotFound("User not found");
        }

        if (!await _roleManager.RoleExistsAsync(request.Role))
        {
            return BadRequest($"Role '{request.Role}' does not exist.");
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            return BadRequest(removeResult.Errors);
        }

        var addResult = await _userManager.AddToRoleAsync(user, request.Role);
        if (!addResult.Succeeded)
        {
            return BadRequest(addResult.Errors);
        }

        return Ok(new { Message = "User role changed successfully" });
    }

    private async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByRoleInternal(string role)
    {
        if (!await _roleManager.RoleExistsAsync(role))
        {
            return NotFound($"Role '{role}' not found.");
        }

        var users = await _userManager.GetUsersInRoleAsync(role);
        
        var userDtos = new List<UserDto>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userDtos.Add(new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles
            });
        }

        return Ok(userDtos);
    }
}
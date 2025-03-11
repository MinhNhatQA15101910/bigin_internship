using Auth.Api.Dtos;
using Auth.Api.Extensions;
using Auth.Api.Helpers;
using Auth.Api.Interfaces;
using Auth.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    UserManager<User> userManager,
    IMapper mapper,
    IUserRepository userRepository
) : ControllerBase
{
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await userManager.Users
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == User.GetUserId());

        if (user == null) return BadRequest("Could not find user");

        return mapper.Map<UserDto>(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] UserParams userParams)
    {
        userParams.CurrentEmail = User.GetEmail();
        var users = await userRepository.GetUsersAsync(userParams);

        Response.AddPaginationHeader(users);

        return Ok(users);
    }

    [HttpPatch("change-password")]
    [Authorize]
    public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var userId = User.GetUserId();

        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null) return NotFound("Could not find user");

        var checkPasswordResult = await userManager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword);
        if (!checkPasswordResult) return Unauthorized("Current password is incorrect");

        user.UpdatedAt = DateTime.UtcNow;
        var changePasswordResult = await userManager.ChangePasswordAsync(
            user, changePasswordDto.CurrentPassword, 
            changePasswordDto.NewPassword
        );
        if (!changePasswordResult.Succeeded) return BadRequest(changePasswordResult.Errors);

        return NoContent();
    }
}

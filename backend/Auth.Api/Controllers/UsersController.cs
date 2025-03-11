using Auth.Api.Dtos;
using Auth.Api.Extensions;
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
    IMapper mapper
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
}

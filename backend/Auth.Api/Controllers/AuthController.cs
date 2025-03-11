using Auth.Api.Dtos;
using Auth.Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    UserManager<User> userManager,
    IMapper mapper
) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var existingUser = await userManager.Users
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .SingleOrDefaultAsync(x => x.NormalizedEmail == loginDto.Email.ToUpper());
        if (existingUser == null)
        {
            return Unauthorized("User with this email does not exist.");
        }

        var result = await userManager.CheckPasswordAsync(existingUser, loginDto.Password);
        if (!result) return Unauthorized("Invalid password");

        var userDto = mapper.Map<UserDto>(existingUser);

        return userDto;
    }
}

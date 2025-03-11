using Auth.Api.Dtos;
using Auth.Api.Extensions;
using Auth.Api.Interfaces;
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
    IMapper mapper,
    ITokenService tokenService
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

    [HttpPost("signup")]
    public async Task<ActionResult<UserDto>> ValidateSignup(RegisterDto registerDto)
    {
        // Check if email already exists
        if (await UserExists(registerDto.Email))
        {
            return BadRequest("Email already exists.");
        }

        // Check if password is valid
        var result = await userManager.PasswordValidators.First().ValidateAsync(
            userManager,
            null!,
            registerDto.Password
        );
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }


        var user = mapper.Map<User>(registerDto);
        user.UserName = user.UserName!.ConvertToAsciiLowercase();

        await userManager.CreateAsync(user, registerDto.Password);
        await userManager.AddToRoleAsync(user, "User");

        var userDto = mapper.Map<UserDto>(user);

        userDto.Token = await tokenService.CreateTokenAsync(user);
        return Ok(userDto);
    }

    private async Task<bool> UserExists(string email)
    {
        return await userManager.Users.AnyAsync(x => x.NormalizedEmail == email.ToUpper());
    }
}

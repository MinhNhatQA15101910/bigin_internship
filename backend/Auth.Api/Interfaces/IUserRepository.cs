using Auth.Api.Dtos;
using Auth.Api.Helpers;

namespace Auth.Api.Interfaces;

public interface IUserRepository
{
    Task<PagedList<UserDto>> GetUsersAsync(UserParams userParams);
}

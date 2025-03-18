using Domain.Entities;
using SharedKernel;
using SharedKernel.DTOs;
using SharedKernel.Params;

namespace Domain.Repositories.Sqlite;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<PagedList<UserDto>> GetUsersAsync(UserParams userParams);
}

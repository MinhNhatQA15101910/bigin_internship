using Auth.Api.Data;
using Auth.Api.Dtos;
using Auth.Api.Helpers;
using Auth.Api.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Auth.Api.Repositories;

public class UserRepository(
    DataContext context,
    IMapper mapper
) : IUserRepository
{
    public async Task<PagedList<UserDto>> GetUsersAsync(UserParams userParams)
    {
        var query = context.Users.AsQueryable();

        // Remove current user
        query = query.Where(u => u.NormalizedEmail != userParams.CurrentEmail!.ToUpper());

        // Filter by full name
        if (userParams.FullName != null)
        {
            query = query.Where(u => u.FullName.ToLower().Contains(userParams.FullName.ToLower()));
        }

        // Filter by email
        if (userParams.Email != null)
        {
            query = query.Where(u => u.NormalizedEmail == userParams.Email.ToUpper());
        }

        // Order
        query = userParams.OrderBy switch
        {
            "email" => userParams.SortBy == "asc"
                        ? query.OrderBy(u => u.Email)
                        : query.OrderByDescending(u => u.Email),
            "updatedAt" => userParams.SortBy == "asc"
                        ? query.OrderBy(u => u.UpdatedAt)
                        : query.OrderByDescending(u => u.UpdatedAt),
            _ => query.OrderBy(u => u.Email)
        };

        return await PagedList<UserDto>.CreateAsync(
            query.ProjectTo<UserDto>(mapper.ConfigurationProvider),
            userParams.PageNumber,
            userParams.PageSize
        );
    }
}

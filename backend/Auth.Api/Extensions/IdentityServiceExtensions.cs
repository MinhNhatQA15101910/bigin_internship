using Auth.Api.Data;
using Auth.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentityCore<User>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
        })
            .AddRoles<Role>()
            .AddRoleManager<RoleManager<Role>>()
            .AddEntityFrameworkStores<DataContext>();

        return services;
    }
}

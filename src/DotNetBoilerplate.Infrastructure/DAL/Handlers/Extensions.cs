using DotNetBoilerplate.Application.Users.Responses;
using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers;

internal static class Extensions
{
    public static UserDetailsResponse AsUserDetailsDto(this UserReadModel user)
    {
        return new UserDetailsResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role
        };
    }

    private static UserResponse AsDto(this UserReadModel user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username
        };
    }
}
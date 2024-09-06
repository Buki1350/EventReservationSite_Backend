using DotNetBoilerplate.Application.Users;
using DotNetBoilerplate.Application.Users.Responses;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.Users;

internal sealed class GetUserHandler(
    DotNetBoilerplateReadDbContext dbContext,
    IContext context
)
    : IQueryHandler<GetUserQuery, UserDetailsResponse?>
{
    public async Task<UserDetailsResponse> HandleAsync(GetUserQuery query)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == query.UserId);

        return user is null
            ? null
            : new UserDetailsResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
    }
}
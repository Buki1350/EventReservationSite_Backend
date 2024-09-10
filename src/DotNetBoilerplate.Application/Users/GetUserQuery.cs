using DotNetBoilerplate.Application.Users.Responses;
using DotNetBoilerplate.Shared.Abstractions.Queries;

namespace DotNetBoilerplate.Application.Users;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserDetailsResponse?>
{
}
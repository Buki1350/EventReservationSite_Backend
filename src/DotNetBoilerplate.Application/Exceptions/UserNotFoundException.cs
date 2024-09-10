using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

public sealed class UserNotFoundException : CustomException
{
    public UserNotFoundException(UserId userId) : base($"User {userId} not found")
    {
    }
}
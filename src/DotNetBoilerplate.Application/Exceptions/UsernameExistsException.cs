using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

public sealed class UsernameExistsException : CustomException
{
    public UsernameExistsException() : base("Username already in use.")
    {
    }
}
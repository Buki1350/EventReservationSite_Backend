using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Application.Exceptions;

public sealed class EmailExistsException : CustomException
{
    public EmailExistsException() : base("Email already in use.")
    {
    }
}
using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.Url;

public sealed class InvalidUrlException : CustomException
{
    public InvalidUrlException(string value) : base($"{value} is not a valid redirect url")
    {
    }
}
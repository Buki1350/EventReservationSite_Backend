using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.BaseName;

public class InvalidNameException : CustomException
{
    public InvalidNameException() : base("Invalid name")
    {
    }
}
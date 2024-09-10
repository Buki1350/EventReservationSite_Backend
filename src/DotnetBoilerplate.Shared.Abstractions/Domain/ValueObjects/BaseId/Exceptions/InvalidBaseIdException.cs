using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.BaseId.Exceptions;

public class InvalidBaseIdException : CustomException
{
    public InvalidBaseIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
    {
    }
}
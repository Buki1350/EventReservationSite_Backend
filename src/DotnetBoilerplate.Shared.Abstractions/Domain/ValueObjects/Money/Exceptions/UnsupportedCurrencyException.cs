using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.Money.Exceptions;

public class UnsupportedCurrencyException : CustomException
{
    public UnsupportedCurrencyException(string message) : base(message)
    {
    }
}
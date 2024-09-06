using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.Money.Exceptions;

public class InvalidCurrencyException : CustomException
{
    public InvalidCurrencyException(string currency) : base($"Currency: '{currency}' is invalid.")
    {
    }
}
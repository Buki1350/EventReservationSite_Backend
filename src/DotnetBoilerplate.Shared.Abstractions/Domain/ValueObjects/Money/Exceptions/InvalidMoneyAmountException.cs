using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.Money.Exceptions;

public class InvalidMoneyAmountException : CustomException
{
    public InvalidMoneyAmountException(decimal amount) : base($"Money amount {amount}")
    {
    }
}
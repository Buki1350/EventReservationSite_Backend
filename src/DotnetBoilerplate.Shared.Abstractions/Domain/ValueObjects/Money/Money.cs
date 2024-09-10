using DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.Money.Exceptions;

namespace DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.Money;

public record Money
{
    private static readonly HashSet<string> AllowedCurrencies = new() { "PLN", "EUR", "GBP" };

    private Money()
    {
    }

    private Money(int amount, string currency)
    {
        if (amount is < 0 or > 1000000)
            throw new InvalidMoneyAmountException(amount);

        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new InvalidCurrencyException(currency);

        currency = currency.ToUpperInvariant();
        if (!AllowedCurrencies.Contains(currency))
            throw new UnsupportedCurrencyException(currency);

        Amount = amount;
        Currency = currency;
    }

    public int Amount { get; init; }
    public string Currency { get; init; }

    public static Money Create(int amount, string currency)
    {
        return new Money(amount, currency);
    }

    public Money Copy()
    {
        return new Money
        {
            Amount = Amount,
            Currency = Currency
        };
    }
}
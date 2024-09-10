using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Users;

internal sealed class InvalidAccountTypeException(string value) : CustomException($"{value} is not a valid account type.");

public record AccountType
{
    public AccountType(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30) throw new InvalidAccountTypeException(value);

        if (!AvailableAccountTypes.Contains(value)) throw new InvalidAccountTypeException(value);

        Value = value;
    }

    public static IEnumerable<string> AvailableAccountTypes { get; } = new[] { "free", "basic", "extended" };

    public string Value { get; }

    public static AccountType Free()
    {
        return new AccountType("free");
    }

    public static AccountType Basic()
    {
        return new AccountType("basic");
    }

    public static AccountType Extended()
    {
        return new AccountType("extended");
    }

    public static implicit operator AccountType(string value)
    {
        return new AccountType(value);
    }

    public static implicit operator string(AccountType value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}
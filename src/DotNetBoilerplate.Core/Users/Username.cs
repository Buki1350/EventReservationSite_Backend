using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Users;

public class InvalidUsernameException(string value) : CustomException($"{value} is not a valid username.");

public sealed record Username
{
    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 30 or < 3) throw new InvalidUsernameException(value);

        Value = value;
    }

    public string Value { get; }

    public static implicit operator Username(string value)
    {
        return new Username(value);
    }

    public static implicit operator string(Username value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}
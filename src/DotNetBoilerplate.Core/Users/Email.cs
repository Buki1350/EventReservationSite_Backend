using System.Text.RegularExpressions;
using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Users;

public sealed class InvalidEmailException(string emailValue) : CustomException($"{emailValue} is not a valid email.");

public sealed record Email
{
    public Email(string value)
    {
        if (string.IsNullOrEmpty(value) || !IsValid(value)) throw new InvalidEmailException(value);
        Value = value;
    }

    public string Value { get; }

    private static bool IsValid(string emailToValidate)
    {
        return Regex.IsMatch(emailToValidate,
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }

    public static implicit operator string(Email email)
    {
        return email.Value;
    }

    public static implicit operator Email(string email)
    {
        return new Email(email);
    }

    public override string ToString()
    {
        return Value;
    }
}
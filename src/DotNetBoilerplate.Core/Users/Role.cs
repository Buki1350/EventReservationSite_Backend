using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Users;

public class InvalidRoleException(string role) : CustomException($"Role: '{role}' is invalid.")
{
    public string Role { get; } = role;
}

public record Role
{
    public Role(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 30) throw new InvalidRoleException(value);

        if (!AvailableRoles.Contains(value)) throw new InvalidRoleException(value);

        Value = value;
    }

    private static IEnumerable<string> AvailableRoles { get; } = new[] { "admin", "user" };

    public string Value { get; }

    public static Role Admin()
    {
        return new Role("admin");
    }

    public static Role User()
    {
        return new Role("user");
    }

    public static implicit operator Role(string value)
    {
        return new Role(value);
    }

    public static implicit operator string(Role value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return Value;
    }
}
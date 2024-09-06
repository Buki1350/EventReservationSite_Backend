namespace DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.Url;

public sealed record Url
{
    public Url(string value)
    {
        var isValidUrl = Uri.TryCreate(value, UriKind.Absolute, out var result);
        if (!isValidUrl) throw new InvalidUrlException(value);

        Value = value;
    }

    public string Value { get; }

    public static implicit operator string(Url value)
    {
        return value.Value;
    }

    public static implicit operator Url(string value)
    {
        return new Url(value);
    }
}
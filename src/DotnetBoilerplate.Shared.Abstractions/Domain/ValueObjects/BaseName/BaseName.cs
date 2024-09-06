namespace DotNetBoilerplate.Shared.Abstractions.Domain.ValueObjects.BaseName;

public class BaseName
{
    protected BaseName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length is > 30 or < 3) throw new InvalidNameException();

        Value = value;
    }

    public string Value { get; }

    public override string ToString()
    {
        return Value;
    }
}